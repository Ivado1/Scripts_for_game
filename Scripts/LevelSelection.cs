using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public static int[] levelStars;
    public Button[] lvlButtons ;
    public Image[] starImage;
    public Sprite OneStarImage, TwoStarImage, ThreeStarImage;
    public Text starBiomScore;
    private int sumStars;

    void Awake()
    {
        levelStars = new int[SceneManager.sceneCountInBuildSettings];

        if (SceneManager.GetActiveScene().name == ("LevelCoast"))
        {
            int LevelAt = PlayerPrefs.GetInt("levelAtCoast", 1);
            for (int i = 0; i < lvlButtons.Length; i++)
            {
                if (i + 1 > LevelAt)
                    lvlButtons[i].interactable = false;
            }
        }
        if (SceneManager.GetActiveScene().name == ("LevelDesert"))
        {
            int LevelAt = PlayerPrefs.GetInt("levelAtDesert", 1);
            for (int i = 0; i < lvlButtons.Length; i++)
            {
                if (i + 1 > LevelAt)
                    lvlButtons[i].interactable = false;
            }
        }
        if (SceneManager.GetActiveScene().name == ("LevelSnow"))
        {
            int LevelAt = PlayerPrefs.GetInt("levelAtSnow", 1);
            for (int i = 0; i < lvlButtons.Length; i++)
            {
                if (i + 1 > LevelAt)
                    lvlButtons[i].interactable = false;
            }
        }
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == ("LevelCoast"))
        {
            Main();  //1 -15 level
            Debug.Log("Current Scene Coast");
        }
        if (SceneManager.GetActiveScene().name == ("LevelDesert"))
        {
            Main2();  //16 - 30 level
            Debug.Log("Current Scene Desert");
        }
        if (SceneManager.GetActiveScene().name == ("LevelSnow"))
        {
            Main3();  //31 - 45 level
            Debug.Log("Current Scene Snow");
        }

    }
    public void loadLevelINDEX(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
        Time.timeScale = 1f;
    }

    private void Main()
    {
        for (int i = 1; i <= 15; i++)
        {
            starBiomScore.text = sumStars + "/45".ToString();

            if (PlayerPrefs.HasKey("Stars" + i))
            {                
                levelStars[i] = PlayerPrefs.GetInt("Stars" + i);

                if (levelStars[i] == 1) starImage[i].sprite = OneStarImage;
                if (levelStars[i] == 2) starImage[i].sprite = TwoStarImage;
                if (levelStars[i] == 3) starImage[i].sprite = ThreeStarImage;

                sumStars += levelStars[i];
                starBiomScore.text = sumStars + "/45".ToString();
                Debug.Log(sumStars + "FOR vsego stars ");

            }
        }
    }
    private void Main2()
    {
        starBiomScore.text = sumStars + "/45".ToString();

        for (int i = 16; i <= 30; i++)
        {
            if (PlayerPrefs.HasKey("Stars" + i))
            {
                levelStars[i] = PlayerPrefs.GetInt("Stars" + i);

                if (levelStars[i] == 1) starImage[i].sprite = OneStarImage;
                if (levelStars[i] == 2) starImage[i].sprite = TwoStarImage;
                if (levelStars[i] == 3) starImage[i].sprite = ThreeStarImage;

                sumStars += levelStars[i];
                starBiomScore.text = sumStars + "/45".ToString();
                Debug.Log(sumStars + "FOR vsego stars ");
            }
        }
    }
    private void Main3()
    {
        starBiomScore.text = sumStars + "/45".ToString();

        for (int i = 31; i <= 45; i++)
        {
            if (PlayerPrefs.HasKey("Stars" + i))
            {
                levelStars[i] = PlayerPrefs.GetInt("Stars" + i);

                if (levelStars[i] == 1) starImage[i].sprite = OneStarImage;
                if (levelStars[i] == 2) starImage[i].sprite = TwoStarImage;
                if (levelStars[i] == 3) starImage[i].sprite = ThreeStarImage;

                sumStars += levelStars[i];
                starBiomScore.text = sumStars + "/45".ToString();
                Debug.Log(sumStars + "FOR vsego stars ");
            }
        }
    }
}
