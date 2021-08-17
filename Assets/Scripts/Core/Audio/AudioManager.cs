// Tutorial for this can be found on Reinassance Coders' Youtube chanel.

using System.Collections;
using UnityEngine;
using Core.Save;
using UnityEngine.Audio;

namespace Core
{
    namespace Audio
    {
        public class AudioManager : MonoBehaviour
        {
            public static AudioManager Instance { get; set; }

            // mixer and volume
            public AudioMixer mixer;
            [HideInInspector] public int masterVolume;
            [HideInInspector] public int musicVolume;
            [HideInInspector] public int soundVolume;

            private string _masterMixer = "MasterVolume";
            private string _musicMixer = "SoundtrackVolume";
            private string _soundMixer = "SFXVolume";

            // sound hashtables
            public bool debug;
            public AudioTrack[] tracks;

            private Hashtable m_AudioTable; // relationship between audio types (key) and audio tracks (value)
            private Hashtable m_JobTable;   // relationship between audio types (key) and jobs (value) (Coroutine, IEnumerator)

            [System.Serializable]
            public class AudioObject
            {
                public AudioTypeName type;
                public AudioClip clip;
            }

            [System.Serializable]
            public class AudioTrack
            {
                public AudioSource source;
                public AudioObject[] audio;
            }

            private class AudioJob
            {
                public AudioAction action;
                public AudioTypeName type;
                public bool fade;
                public float delay;

                public AudioJob(AudioAction _action, AudioTypeName _type, bool _fade, float _delay)
                {
                    action = _action;
                    type = _type;
                    fade = _fade;
                    delay = _delay;
                }
            }

            private enum AudioAction
            {
                START,
                STOP,
                RESTART
            }

            #region Unity Functions
            private void Awake()
            {
                if (Instance == null)
                    Configure();
                else
                    Destroy(this);
            }

            private void Start()
            {
                SetMixerVolume();
                StartMusic();
            }

            private void OnDisable()
            {
                Dispose();
            }
            #endregion

            #region Public Functions
            public void PlayAudio(AudioTypeName _type, bool _fade=false, float _delay=0.0f)
            {
                AddJob(new AudioJob(AudioAction.START, _type, _fade, _delay));
            }
            public void StopAudio(AudioTypeName _type, bool _fade=false, float _delay=0.0f)
            {
                AddJob(new AudioJob(AudioAction.STOP, _type, _fade, _delay));
            }
            public void RestartAudio(AudioTypeName _type, bool _fade=false, float _delay=0.0f)
            {
                AddJob(new AudioJob(AudioAction.RESTART, _type, _fade, _delay));
            }
            #endregion

            #region Private Functions
            private void Configure()
            {
                Instance = this;
                m_AudioTable = new Hashtable();
                m_JobTable = new Hashtable();              
                GenerateAudioTable();
                LoadConfiguration();
            }

            // default values at 5 because that's what the sliders default to.

            private void Dispose()
            {
                foreach(DictionaryEntry _entry in m_JobTable)
                {
                    IEnumerator _job = (IEnumerator)_entry.Value;
                }
            }

            private void GenerateAudioTable()
            {
                foreach (AudioTrack _track in tracks)
                {
                    foreach (AudioObject _obj in _track.audio)
                    {
                        // do not duplicate keys
                        if (m_AudioTable.ContainsKey(_obj.type))
                        {
                            LogWarning("You are trying to register audio [" + _obj.type + "] that has already been registered.");
                        }
                        else
                        {
                            m_AudioTable.Add(_obj.type, _track);
                            Log("Registering audio [" + _obj.type + "].");
                        }
                    }
                }
            }

            private IEnumerator RunAudioJob(AudioJob _job)
            {
                yield return new WaitForSecondsRealtime(_job.delay);

                AudioTrack _track = (AudioTrack)m_AudioTable[_job.type];
                _track.source.clip = GetAudioClipFromAudioTrack(_job.type, _track);

                switch (_job.action)
                {
                    case AudioAction.START:
                        if (!_track.source.isPlaying)
                        _track.source.Play();
                        break;

                    case AudioAction.STOP:
                        if (!_job.fade)
                        {
                            _track.source.Stop();
                        }
                        break;

                    case AudioAction.RESTART:
                        _track.source.Stop();
                        _track.source.Play();
                        break;
                }

                if (_job.fade)
                {
                    float _initial = _job.action == AudioAction.START || _job.action == AudioAction.RESTART ? 0.0f : 1.0f;
                    float _target = _initial == 0 ? 1 : 0;
                    float _duration = 1.0f;
                    float _timer = 0.0f;

                    while (_timer <= _duration)
                    {
                        _track.source.volume = Mathf.Lerp(_initial, _target, _timer / _duration);
                        _timer += Time.unscaledDeltaTime;
                        yield return null;
                    }

                    if (_job.action == AudioAction.STOP)
                        _track.source.Stop();
                }

                m_JobTable.Remove(_job.type);
                Log("Job count: " + m_JobTable.Count);

                yield return null;
            }

            private void AddJob(AudioJob _job)
            {
                // remove conflicting jobs
                RemoveConflictingJobs(_job.type);

                // start job
                IEnumerator _jobRunner = RunAudioJob(_job);
                m_JobTable.Add(_job.type, _jobRunner);
                StartCoroutine(_jobRunner);
                Log("Starting job on [" + _job.type + "] with operation: " + _job.action);
            }

            private void RemoveJob(AudioTypeName _type)
            {
                if (!m_JobTable.ContainsKey(_type))
                {
                    LogWarning("YOu are trying to stop a job [" + _type + "] that is not running.");
                    return;
                }

                IEnumerator _runningJob = (IEnumerator)m_JobTable[_type];
                StopCoroutine(_runningJob);
                m_JobTable.Remove(_type);
            }

            private void RemoveConflictingJobs(AudioTypeName _type)
            {
                if (m_JobTable.ContainsKey(_type))
                {
                    RemoveJob(_type);
                }

                AudioTypeName _conflictAudio = AudioTypeName.None;
                foreach (DictionaryEntry _entry in m_JobTable)
                {
                    AudioTypeName _audioType = (AudioTypeName)_entry.Key;
                    AudioTrack _audioTrackInUse = (AudioTrack)m_AudioTable[_audioType];
                    AudioTrack _audioTrackNeeded = (AudioTrack)m_AudioTable[_type];
                    if (_audioTrackNeeded.source == _audioTrackInUse.source)
                    {
                        // conflict
                        _conflictAudio = _audioType;
                    }
                }
                if (_conflictAudio != AudioTypeName.None)
                {
                    RemoveJob(_conflictAudio);
                }
            }

            public AudioClip GetAudioClipFromAudioTrack(AudioTypeName _type, AudioTrack _track)
            {
                foreach(AudioObject _obj in _track.audio)
                {
                    if (_obj.type == _type)
                        return _obj.clip;
                }
                return null;
            }

            private void Log(string _msg)
            {
                if (!debug) return;
                Debug.Log("[Audio Controller]: " + _msg);
            }

            private void LogWarning(string _msg)
            {
                if (!debug) return;
                Debug.LogWarning("[Audio Controller]: " + _msg);
            }
            #endregion

            #region SaveFunctions
            public void SaveConfiguration()
            {
                SaveSystem.SaveConfiguration(this);
            }

            public void LoadConfiguration()
            {
                if (SaveSystem.LoadConfiguration() != null)
                {
                    ConfigurationData data = SaveSystem.LoadConfiguration();
                    masterVolume = data.masterVolume;
                    musicVolume = data.musicVolume;
                    soundVolume = data.soundVolume;
                }
                else
                {
                    masterVolume = 12;
                    musicVolume = 12;
                    soundVolume = 12;
                  
                    SaveConfiguration();
                    Debug.LogWarning("A new save file has been created.");
                }
            }

            private void SetMixerVolume()
            {
                mixer.SetFloat(_masterMixer, SliderValueNormalizer.Normalize(masterVolume));
                mixer.SetFloat(_musicMixer, SliderValueNormalizer.Normalize(musicVolume));
                mixer.SetFloat(_soundMixer, SliderValueNormalizer.Normalize(soundVolume));
            }

            public void MuteForAd(bool mute = false)
            {
                if (mute)
                    mixer.SetFloat(_masterMixer, SliderValueNormalizer.Normalize(0));
                else
                    mixer.SetFloat(_masterMixer, SliderValueNormalizer.Normalize(masterVolume));

                StartMusic();
            }

            public void StartMusic()
            {
                if (musicVolume == 0 || masterVolume == 0)
                    StopAudio(AudioTypeName.ST_01);
                else
                    PlayAudio(AudioTypeName.ST_01);
            }
            #endregion
        }
    }
}