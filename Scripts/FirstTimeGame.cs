using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstTimeGame : MonoBehaviour
{
    public bool firstTimeEnter;

    void Start()
    {
        if (PlayerPrefs.HasKey("firstTimeEnter"))
        {
            firstTimeEnter = true;
        }
        else
        {
            
            //PlayerPrefs.SetInt("firstTimeEnter", 1);
            SceneManager.LoadScene("Garage");
        }
    }
    
}
