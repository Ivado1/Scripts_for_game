using UnityEngine;
using UnityEngine.SceneManagement;

public class Go : MonoBehaviour
{
    //AudioSource Sound;

    void Start()
    {
        //if (Sound!= null) Sound = GetComponent<AudioSource>();
    } 
    public void GoLevel(int number)
    {
        //if (Sound != null) Sound.Play();
        SceneManager.LoadScene(number);   
    }
    public void GoLevelName(string name)
    {
        AudioListener.pause = false;
        //if (Sound != null) Sound.Play();
        SceneManager.LoadScene(name);
        //Sound.TransitionTo(1f);
    }    
    public void ShowAD()
    {
       // GameDistribution.Instance.ShowAd();  ///for GD
    }
    public void LoadNextLevel()
    {
        AudioListener.pause = false;
        //if (Sound != null) Sound.Play();
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().buildIndex == 15) SceneManager.LoadScene("LevelCoast");
        if (SceneManager.GetActiveScene().buildIndex == 30) SceneManager.LoadScene("LevelDesert");
        if (SceneManager.GetActiveScene().buildIndex == 45) SceneManager.LoadScene("LevelSnow");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }


}
