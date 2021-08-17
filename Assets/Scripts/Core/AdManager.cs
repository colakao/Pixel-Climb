using UnityEngine;
using UnityEngine.Advertisements;
using Core;
using Core.Audio;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public static AdManager Instance { get; private set; }

    private string rewardedVideoAd = "rewardedVideo";
    public bool isTestAd;

#if UNITY_ANDROID
    private string playStoreID = "4085919";

    void InitizalizeAd()
    {
        Advertisement.Initialize(playStoreID, isTestAd);
    }
#endif

#if UNITY_IOS
    private string appStoreID = "4085918";

        void InitizalizeAd()
    {
        Advertisement.Initialize(appStoreID, isTestAd);
    }
#endif

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        Advertisement.AddListener(this);
        InitizalizeAd();
    }

    public void PlayRewarded()
    {
        if (!Advertisement.IsReady(rewardedVideoAd)) return;
        Advertisement.Show(rewardedVideoAd);
    }

    public void OnUnityAdsReady(string placementId)
    {
        //meh
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Ad failed to show.");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        AudioManager.Instance.MuteForAd(true);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        AudioManager.Instance.MuteForAd(false);
        switch (showResult)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Finished:
                if (placementId == rewardedVideoAd)
                GameManager.Instance.ReloadReward();
                break;
        }
    }
}
