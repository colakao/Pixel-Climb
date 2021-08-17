using UnityEngine;

namespace Core
{
    namespace Audio
    {
        public class TestAudio : MonoBehaviour
        {
            public AudioManager audioManager;
            #region Unity Functions
#if UNITY_EDITOR
            private void Update()
            {
                if (Input.GetKeyUp(KeyCode.T))
                {
                    audioManager.PlayAudio(AudioTypeName.ST_01, true);
                }
                if (Input.GetKeyUp(KeyCode.G))
                {
                    audioManager.StopAudio(AudioTypeName.ST_01, true);
                }
                if (Input.GetKeyUp(KeyCode.B))
                {
                    audioManager.RestartAudio(AudioTypeName.ST_01);
                }

                if (Input.GetKeyUp(KeyCode.Y))
                {
                    audioManager.PlayAudio(AudioTypeName.SFX_01);
                }
                if (Input.GetKeyUp(KeyCode.H))
                {
                    audioManager.StopAudio(AudioTypeName.SFX_01);
                }
                if (Input.GetKeyUp(KeyCode.N))
                {
                    audioManager.RestartAudio(AudioTypeName.SFX_01);
                }
            }
            #endif
            #endregion
        }
    }

}
