using UnityEngine;
using UnityEngine.SceneManagement;
// For transition scene by button
public class Go : MonoBehaviour
{
    public void GoLevel(int number)
    {
        SceneManager.LoadScene(number);   
    }
    public void GoLevelName(string name)
    {
        AudioListener.pause = false;
        SceneManager.LoadScene(name);
    }    
    public void LoadNextLevel()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().buildIndex == 15) SceneManager.LoadScene("LevelCoast");
        if (SceneManager.GetActiveScene().buildIndex == 30) SceneManager.LoadScene("LevelDesert");
        if (SceneManager.GetActiveScene().buildIndex == 45) SceneManager.LoadScene("LevelSnow");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }


}
