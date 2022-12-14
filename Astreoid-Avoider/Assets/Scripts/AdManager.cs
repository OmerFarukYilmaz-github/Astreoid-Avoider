using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] bool isTestMode;
    public static AdManager instance;
    private GameOver gameOver;

#if UNITY_ANDROID
    string gameId="";
#elif Unity_IOS
    string gameId="";
#endif

    public void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Advertisement.AddListener(this);
            // Unutma
            Advertisement.Initialize("gameId will be here",isTestMode);
        }
    }
    public void ShowAd(GameOver gameOver)
    {
        this.gameOver = gameOver;
        Advertisement.Show("Ad_Id");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Ads Error");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                gameOver.Continue();
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("Ad Skipped");
                break;
            case ShowResult.Failed:
                Debug.LogWarning("Ad Failed");
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ad Ready");
    }



}
