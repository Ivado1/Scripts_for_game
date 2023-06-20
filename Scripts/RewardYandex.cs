using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardYandex : MonoBehaviour
{
    public YandexSDK sdk;
    private int moneyFinishReward;

    void Start()
    {        
        sdk.onRewardedAdReward += RewardBonus;
        sdk.onRewardedAdReward += RewardDouble;
        if (PlayerPrefs.HasKey("Money"))
        {
            ScriptableObjectChanger.money = PlayerPrefs.GetInt("Money");
        }
        if (SceneManager.GetActiveScene().buildIndex >= 1 && SceneManager.GetActiveScene().buildIndex <= 7) moneyFinishReward = 75;
        if (SceneManager.GetActiveScene().buildIndex >= 8 && SceneManager.GetActiveScene().buildIndex <= 15) moneyFinishReward = 120;
        if (SceneManager.GetActiveScene().buildIndex >= 16 && SceneManager.GetActiveScene().buildIndex <= 23) moneyFinishReward = 75;
        if (SceneManager.GetActiveScene().buildIndex >= 24 && SceneManager.GetActiveScene().buildIndex <= 30) moneyFinishReward = 120;
        if (SceneManager.GetActiveScene().buildIndex >= 31 && SceneManager.GetActiveScene().buildIndex <= 37) moneyFinishReward = 75;
        if (SceneManager.GetActiveScene().buildIndex >= 38 && SceneManager.GetActiveScene().buildIndex <= 45) moneyFinishReward = 120;

        //Invoke("WorkButton", 1f);
    }
    private void WorkButton()
    {
        AudioListener.pause = true;
        this.gameObject.SetActive(true);
    }
    public void RewardBonus(string placement)
    {
        AudioListener.pause = true;
              
        Invoke("WorkButton", 2f);
        Invoke("GiveMoney", 5f);
        
        this.gameObject.SetActive(false);
        //SceneManager.LoadScene("Garage");
    }
    private void GiveMoney()
    {
        AudioListener.pause = true;
        if (PlayerPrefs.HasKey("Money"))
        {
            ScriptableObjectChanger.money = PlayerPrefs.GetInt("Money");
        }
        PlayerPrefs.SetInt("Money", ScriptableObjectChanger.money += 100);  // Fix Reward 
    }

    public void RewardDouble(string placement)
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            ScriptableObjectChanger.money = PlayerPrefs.GetInt("Money");
        }
        PlayerPrefs.SetInt("Money", ScriptableObjectChanger.money += moneyFinishReward);  // Reward Double by Finish Button
        if (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().buildIndex <= 15) SceneManager.LoadScene("LevelCoast");
        if (SceneManager.GetActiveScene().buildIndex > 15 && SceneManager.GetActiveScene().buildIndex <= 30) SceneManager.LoadScene("LevelDesert");
        if (SceneManager.GetActiveScene().buildIndex > 30 && SceneManager.GetActiveScene().buildIndex <= 45) SceneManager.LoadScene("LevelSnow");
        //SceneManager.LoadScene(0);
    }


}
