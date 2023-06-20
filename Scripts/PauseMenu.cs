using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuBackground;
    public GameObject MoveButtonsLeft;
    public GameObject MoveButtonsRight;
    public GameObject IconsGame;
    
    private void Start()
    {
        if (Application.isMobilePlatform)  // toucheble devices
        {
            QualitySettings.antiAliasing = 0;
            MoveButtonsLeft.SetActive(true);
            MoveButtonsRight.SetActive(true);
        }
        else
        {
            QualitySettings.antiAliasing = 2;
            MoveButtonsLeft.SetActive(false);
            MoveButtonsRight.SetActive(false);
        }

    }
    public void IconPause()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        PauseMenuBackground.SetActive(true);
        MoveButtonsLeft.SetActive(false);
        MoveButtonsRight.SetActive(false);
        IconsGame.SetActive(false);
    }
    public void ReturnButton()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        PauseMenuBackground.SetActive(false);
        //MoveButtons.SetActive(true);  Mobile Game must be ON
        IconsGame.SetActive(true);

        if (Application.isMobilePlatform)  // toucheble devices
        {
            MoveButtonsLeft.SetActive(true);
        }
        else
        {
            MoveButtonsRight.SetActive(false);
        }
    }
    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

}
