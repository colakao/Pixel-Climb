using UnityEngine;
using Player;
using Core.Menu.MenuSystem;
using Core.Save;

using UnityEngine.SocialPlatforms;
//using GooglePlayGames.BasicApi;
//using GooglePlayGames;
using System;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [HideInInspector] public int score;
        [HideInInspector] public int highScore;
        [HideInInspector] public bool tutorialSeen;
        [HideInInspector] public bool revivedWithAd;

        public GameObject sawBlades;

        public bool isConnectedToGooglePlayServices;

        private void Awake()
        {
            Initialize();
            //PlayGamesPlatform.DebugLogEnabled = true;
            //PlayGamesPlatform.Activate();
        }

        //private void Start()
        //{
        //    SignInToGooglePlayServices();
        //}

        //private void SignInToGooglePlayServices()
        //{
        //    PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
        //    {
        //        switch (result)
        //        {
        //            case SignInStatus.Success:
        //                isConnectedToGooglePlayServices = true;
        //                break;
        //            default:
        //                isConnectedToGooglePlayServices = false;
        //                break;
        //        }
        //    });
        //}

        private void Initialize()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        #region ScoreLogic
        public void IncreaseScore(int scoreToIncrease)
        {
            score += scoreToIncrease;
            UpdateScore();
        }

        private void UpdateScore(int scoreToAdd = 0)
        {
            UpdateScoreUI.UpdateScore(score);
            //SendAchievementProgress(score);
        }

        //private void SendAchievementProgress(int _score)
        //{
        //    switch (_score)
        //    {
        //        case 10:
        //            Social.ReportProgress(GPGSIds.achievement_double_digits, 100f, null);
        //            break;
        //        case 25:
        //            Social.ReportProgress(GPGSIds.achievement_getting_the_hang_of_it, 100f, null);
        //            break;
        //        case 50:
        //            Social.ReportProgress(GPGSIds.achievement_patience_makes_perfect, 100f, null);
        //            break;
        //        case 100:
        //            Social.ReportProgress(GPGSIds.achievement_climb_graduate, 100f, null);
        //            break;
        //        case 258:
        //            Social.ReportProgress(GPGSIds.achievement_better_than_the_developer, 100f, null);
        //            break;
        //        case 999:
        //            Social.ReportProgress(GPGSIds.achievement_gate_of_expertise, 100f, null);
        //            break;
        //        default:
        //            break;
        //    }
        //}
        #endregion

        #region GameStates
        public void GameOver()
        {
            //CloudOnceServices.Instance.SumbitScoreToLeaderboard(highScore);
            if (score > highScore)
            {
                highScore = score;
                if (isConnectedToGooglePlayServices)
                {
                    Debug.Log("Reporting score...");
                    Social.ReportScore(highScore, GPGSIds.leaderboard_highscore, (success) =>
                    {
                        if (!success) Debug.LogError("Unable to post highscore");
                    });
                }
                else
                {
                    Debug.Log("Not signed in.. unable to report score");
                }
            }
            Instance.SavePlayer();
            Paused();
            MenuManager.Instance.OnGameOver();
        }

        public void ReloadNormal()
        {
            //Saw Blade Logic
            sawBlades.transform.position = sawBlades.GetComponent<MoveUp>().startingPosition;
            Difficulty.Instance.SetTime(0);

            revivedWithAd = false;
            Instance.score = 0;
            Instance.LoadPlayer();
            Unpaused();

            PlatformSpawner.Instance.RemoveAllPlatforms();
            PlayerController.Instance.Respawn(Vector3.zero);
        }

        public void ReloadReward()
        {
            //Saw Blade Logic
            sawBlades.transform.position = sawBlades.GetComponent<MoveUp>().startingPosition;
            Difficulty.Instance.SetTime(0);

            revivedWithAd = true;
            Instance.LoadPlayer();
            Unpaused();
            PlayerController.Instance.Respawn(PlatformSpawner.Instance.GetTopPlatform());
        }

        public void Paused()
        {
            Time.timeScale = 0f;
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }

        public void Unpaused()
        {
            Time.timeScale = 1f;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
        #endregion

        public void SavePlayer()
        {
            SaveSystem.SavePlayer(this);
        }

        public void LoadPlayer()
        {
            if (SaveSystem.LoadPlayer() != null)
            {
                PlayerData data = SaveSystem.LoadPlayer();
                highScore = data.highScore;
                tutorialSeen = data.tutorialSeen;
            }
            else
            {
                highScore = 0;
                tutorialSeen = false;
                SaveSystem.SavePlayer(this);
                Debug.LogWarning("A new save file has been created.");
            }
        }
    }

}