using UnityEngine;
using UnityEngine.SceneManagement;

public class StarsManager : MonoBehaviour
{
    public GameObject[] starsGameObject;
    public int[,] stars = new int[45,4];  //[number level, quantity stars]
    public static int[] starNumber;

    void Awake()
    {
        starNumber = new int[starsGameObject.Length];
        int SceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if(PlayerPrefs.HasKey("Stars" + SceneIndex))
        {
            LevelSelection.levelStars[SceneIndex] = PlayerPrefs.GetInt("Stars" + SceneIndex);
        }
        else
        {
            LevelSelection.levelStars[SceneIndex] = 0;
            //Debug.Log("First Time Play, Haven`t Collected stars");
        }

        for (int i = 0; i < starsGameObject.Length; i++)        //stars  [Number scene 1, number Star 1] = 0;
        {
            if (PlayerPrefs.HasKey("LevelStar" + SceneIndex + i))
            {
                stars[SceneIndex, i] = PlayerPrefs.GetInt("LevelStar" + SceneIndex + i);

                if (stars[SceneIndex, i] > 0)
                {
                    starsGameObject[i].SetActive(false);
                    //Debug.Log(starsGameObject[i] + " In Scene " + SceneIndex + " unable, Was collected ");
                }
            }
        }
    }
    public void FinishLevel()
    {
        int SceneIndex = SceneManager.GetActiveScene().buildIndex;

        for (int i = 0; i < starsGameObject.Length; i++)
        {
            if (starNumber[i] > 0)
            {
                stars[SceneIndex, i] = 1;
                PlayerPrefs.SetInt("LevelStar"+ SceneIndex + i, stars[SceneIndex, i]);
                starsGameObject[i].SetActive(false);
                //Debug.Log("In Scene " + SceneIndex + " StarNumber " + i + " Was Collected");
            }
        } 
        PlayerPrefs.SetInt("Stars"+ SceneIndex, LevelSelection.levelStars[SceneIndex]);
    }
}
