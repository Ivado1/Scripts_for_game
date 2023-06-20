using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{    
    public GameObject mainMenuBackground;
    public GameObject optionMenu;
    public Text allStarsGame;
    private int sumAllStars;
    public AudioSource Sound;
    
    private void Start()
    {
        //GameDistribution.Instance.ShowAd(); //GD initial ad
        CountAllStars();
        //Sound = GetComponent<AudioSource>();
    }
    public void OpenOptionMenu()
    {
        mainMenuBackground.SetActive(false);
        optionMenu.SetActive(true);
        Sound.Play();
    } 
    public void CloseOptionMenu()
    {
        mainMenuBackground.SetActive(true);
        optionMenu.SetActive(false);
        Sound.Play();
    }
    public void Exit()
    {
        Application.Quit();
    }
    private void CountAllStars()
    {
        for (int i = 1; i < 46; i++)
        {
            allStarsGame.text = sumAllStars + "/135".ToString();

            if (PlayerPrefs.HasKey("Stars" + i))
            {
                  
                sumAllStars += PlayerPrefs.GetInt("Stars" + i);
                allStarsGame.text = sumAllStars + "/135".ToString();
                //Debug.Log(sumAllStars + " vsego stars");
            }
        }
    }
}
