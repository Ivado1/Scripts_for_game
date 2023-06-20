using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Finish : MonoBehaviour
{
    public static Finish S;            
    public int nextSceneLoad;
    public GameObject StarsManagerObj;
    public GameObject Icons;
    public GameObject FinishMenu;
    [SerializeField] private TMPro.TextMeshProUGUI MoneyFinish;
    [SerializeField] private int moneyLevel;
    public Image starFinishImages;
    public Sprite OneStarImage, TwoStarImage, ThreeStarImage;
    public AudioSource sondFinish;
    public GameObject moneyPrefabEffect;
    
    DateTime StartAdTime;
    private float timeForNextAd; //time to next ad in sec
    private int remainingDurationAd; //timePassed

    void Start()
    {
        InterstitialAd.S.LoadAd();
        if (SceneManager.GetActiveScene().buildIndex >= 1 && SceneManager.GetActiveScene().buildIndex <= 7) moneyLevel = 75;
        if (SceneManager.GetActiveScene().buildIndex >= 8 && SceneManager.GetActiveScene().buildIndex <= 15) moneyLevel = 120;
        if (SceneManager.GetActiveScene().buildIndex >= 16 && SceneManager.GetActiveScene().buildIndex <= 23) moneyLevel = 75;
        if (SceneManager.GetActiveScene().buildIndex >= 24 && SceneManager.GetActiveScene().buildIndex <= 30) moneyLevel = 120;
        if (SceneManager.GetActiveScene().buildIndex >= 31 && SceneManager.GetActiveScene().buildIndex <= 37) moneyLevel = 75;
        if (SceneManager.GetActiveScene().buildIndex >= 38 && SceneManager.GetActiveScene().buildIndex <= 45) moneyLevel = 120;

        timeForNextAd = 150; //2.5 min
        if (PlayerPrefs.HasKey("StartAdTimeSaves")) StartAdTime = Convert.ToDateTime(PlayerPrefs.GetString("StartAdTimeSaves"));
        else
        {
            StartAdTime = System.DateTime.UtcNow;
            PlayerPrefs.SetString("StartAdTimeSaves", StartAdTime.ToString());
            remainingDurationAd = 0;
        }
        remainingDurationAd = (int)(System.DateTime.UtcNow - StartAdTime).TotalSeconds;
        if (remainingDurationAd > timeForNextAd) TimeToShowAD();
        Debug.Log(StartAdTime + " STARTED");
        Debug.Log(remainingDurationAd + "sec Was past, needs= "+ timeForNextAd);
    }
    public void TimeToShowAD()
    {        
        PlayerPrefs.DeleteKey("StartAdTimeSaves");
        remainingDurationAd = 0;
        Debug.Log("ShowAD");
        InterstitialAd.S.ShowAd();   //AD Unity SHOW        
        
    }
    public void OnTriggerEnter(Collider col)
    {        
        var go = Instantiate(moneyPrefabEffect, starFinishImages.transform.position + new Vector3(-740f, -50f, 0f), Quaternion.identity, GameObject.Find("Canvas").transform);
        go.GetComponent<Text>().text = "+" + moneyLevel + "$".ToString();
        Destroy(go, 1f);

        //if (col.gameObject.tag=="Player")
        if (col.gameObject.CompareTag("Player"))   //more optimyze
        {

            sondFinish.Play();
            Icons.SetActive(false);
            if (SceneManager.GetActiveScene().buildIndex <= 15)
            {
                nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextSceneLoad > PlayerPrefs.GetInt("levelAtCoast")) PlayerPrefs.SetInt("levelAtCoast", nextSceneLoad);
            }
            if (SceneManager.GetActiveScene().buildIndex >= 16 && SceneManager.GetActiveScene().buildIndex <= 30)
            {
                nextSceneLoad = SceneManager.GetActiveScene().buildIndex -14;    // - current build index
                if (nextSceneLoad > PlayerPrefs.GetInt("levelAtDesert")) PlayerPrefs.SetInt("levelAtDesert", nextSceneLoad);
            }
            if (SceneManager.GetActiveScene().buildIndex >= 30 && SceneManager.GetActiveScene().buildIndex <= 45)
            {
                nextSceneLoad = SceneManager.GetActiveScene().buildIndex - 29;    // - current build index
                if (nextSceneLoad > PlayerPrefs.GetInt("levelAtSnow")) PlayerPrefs.SetInt("levelAtSnow", nextSceneLoad);
            }

            if (PlayerPrefs.HasKey("Money")) ScriptableObjectChanger.money = PlayerPrefs.GetInt("Money");            
            PlayerPrefs.SetInt("Money", ScriptableObjectChanger.money += moneyLevel);            
            
            FinishMenu.SetActive(true);
            StarsManagerObj.GetComponent<StarsManager>().FinishLevel(); //Save collected stars
                                                                      
            ReadStars();
            MoneyFinish.text = moneyLevel.ToString();
            Invoke("PauseSoundFinish", 1.1f);            
        }       
    }  
    void PauseSoundFinish()
    {
        AudioListener.pause = true;
        Time.timeScale = 0f;
        Debug.Log("PauseAudio");
    }
    
    private void ReadStars()
    {
        if (PlayerPrefs.HasKey("Stars" + SceneManager.GetActiveScene().buildIndex))
        {
            LevelSelection.levelStars[SceneManager.GetActiveScene().buildIndex] = PlayerPrefs.GetInt("Stars" + SceneManager.GetActiveScene().buildIndex);
            if (LevelSelection.levelStars[SceneManager.GetActiveScene().buildIndex] == 1) starFinishImages.sprite = OneStarImage;
            if (LevelSelection.levelStars[SceneManager.GetActiveScene().buildIndex] == 2) starFinishImages.sprite = TwoStarImage;
            if (LevelSelection.levelStars[SceneManager.GetActiveScene().buildIndex] == 3) starFinishImages.sprite = ThreeStarImage;
            Debug.Log(SceneManager.GetActiveScene().buildIndex+" level has "+LevelSelection.levelStars[SceneManager.GetActiveScene().buildIndex]+" Star");
        }
    }
    public void RewardDoble(string placement)
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            ScriptableObjectChanger.money = PlayerPrefs.GetInt("Money");
        }
        PlayerPrefs.SetInt("Money", ScriptableObjectChanger.money += moneyLevel);  // Fix Level Reward 
        if (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().buildIndex <= 15) SceneManager.LoadScene("LevelCoast");
        if (SceneManager.GetActiveScene().buildIndex > 15 && SceneManager.GetActiveScene().buildIndex <= 30) SceneManager.LoadScene("LevelDesert");
        if (SceneManager.GetActiveScene().buildIndex > 30 && SceneManager.GetActiveScene().buildIndex <= 45) SceneManager.LoadScene("LevelSnow");

    }
}