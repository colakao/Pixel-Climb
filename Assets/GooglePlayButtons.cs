using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GooglePlayButtons : MonoBehaviour
{
    public void OnAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void OnLeaderboards()
    {
        Social.ShowLeaderboardUI();
    }
}
