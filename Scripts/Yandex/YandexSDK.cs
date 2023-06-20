using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexSDK : MonoBehaviour{

    //public static YandexSDK instance;
    [DllImport("__Internal")]
    private static extern void ShowFullscreenAd();
    /// <summary>
    /// Returns an int value which is sent to index.html
    /// </summary>
    /// <param name="placement"></param>
    /// <returns></returns>
    [DllImport("__Internal")]
    private static extern int ShowRewardedAd(string placement);
    [DllImport("__Internal")]
    private static extern void GerReward();
    [DllImport("__Internal")]
    private static extern void AuthenticateUser();
    [DllImport("__Internal")]
    private static extern void InitPurchases();
    [DllImport("__Internal")]
    private static extern void Purchase(string id);

    public UserData user;
    //public event Action onUserDataReceived;
    //public event Action Go;
    //public event Action Pause;

    public event Action onInterstitialShown;
    public event Action<string> onInterstitialFailed;
    /// <summary>
    /// Пользователь открыл рекламу
    /// </summary>
    public event Action<int> onRewardedAdOpened;
    /// <summary>
    /// Пользователь должен получить награду за просмотр рекламы
    /// </summary>
    public event Action<string> onRewardedAdReward;
    /// <summary>
    /// Пользователь закрыл рекламу
    /// </summary>
    public event Action<int> onRewardedAdClosed;
    /// <summary>
    /// Вызов/просмотр рекламы повлёк за собой ошибку
    /// </summary>
    public event Action<string> onRewardedAdError;
    /// <summary>

    public event Action onClose;

    public Queue<int> rewardedAdPlacementsAsInt = new Queue<int>();
    public Queue<string> rewardedAdsPlacements = new Queue<string>();

    private void Awake()
    {
        //if (instance == null)
        //{
            //instance = this;
            
        //}
        //else
        //{
            //Destroy(gameObject);
        //}
    }
 
    public void GamePause()
    {
        FocusSoundController.tvBreak = 1;
        AudioListener.pause = true;
        Time.timeScale = 0f;
        Debug.Log("GamePause-SDK");       
    }
    public void GameOn()
    {
        FocusSoundController.tvBreak = 0;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        Debug.Log("GameGoOn-SDK");        
    }
   
    /// <summary>
    /// Call this to show interstitial ad. Don't call frequently. There is a 3 minute delay after each show.
    /// </summary>
    public void ShowInterstitial()
    {
        //FocusSoundController.tvBreak = 1;
        //ShowFullscreenAd();                                            //Later ON
        //GamePause();
        //Debug.Log("SDK-InterAD_Begin");
    }

    /// <summary>
    /// Call this to show rewarded ad
    /// </summary>
    /// <param name="placement"></param>
    public void ShowRewarded(string placement)
    {
        //Debug.Log("SDK-Was-Start-Rewarded");
        //rewardedAdPlacementsAsInt.Enqueue(ShowRewardedAd(placement));   //Later ON
        //rewardedAdsPlacements.Enqueue(placement);                       //Later ON
    }

    /// <summary>
    /// Call this to receive user data
    /// </summary>


    /// <summary>
    /// Callback from index.html
    /// </summary>
    public void OnInterstitialShown()
    {
        onInterstitialShown();           
        GameOn();
        Debug.Log("SDK-AD-Was-Past");
    }


    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="error"></param>
    public void OnInterstitialError(string error)
    {
        onInterstitialFailed(error);
        GameOn();
        Debug.Log("SDK-Was-Past-failed");
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="placement"></param>
    public void OnRewardedOpen(int placement)
    {
        onRewardedAdOpened(placement);
        Debug.Log("SDK-Was-opened");
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="placement"></param>
    public void OnRewarded(int placement)
    {
        if (placement == rewardedAdPlacementsAsInt.Dequeue())
        {
            onRewardedAdReward.Invoke(rewardedAdsPlacements.Dequeue());
            Debug.Log("SDK-Reward");
        }
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="placement"></param>
    public void OnRewardedClose(int placement)
    {
        onRewardedAdClosed(placement);
        Debug.Log("SDK-Reward-close");
    }

    /// <summary>
    /// Callback from index.html
    /// </summary>
    /// <param name="placement"></param>
    public void OnRewardedError(string placement)
    {
        onRewardedAdError(placement);
        rewardedAdsPlacements.Clear();
        rewardedAdPlacementsAsInt.Clear();
        Debug.Log("Reward-error");
    }
    /// <summary>
    /// Browser tab has been closed
    /// </summary>
    /// <param name="error"></param>
    public void OnClose()
    {
        onClose.Invoke();
        Debug.Log("close");
    }
}

public struct UserData
{
    public string id;
    public string name;
    public string avatarUrlSmall;
    public string avatarUrlMedium;
    public string avatarUrlLarge;
}

