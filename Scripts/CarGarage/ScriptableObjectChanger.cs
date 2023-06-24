using UnityEngine;
using UnityEngine.UI;
//Manage all game cars on level(Upgrade, Car Prefabs) with "Car Display()";
public class ScriptableObjectChanger : MonoBehaviour
{
    public static int money;
    public static int costCar;
    public static int costUpgrade;

    public static int[,] upgradeCar;   //1-speed;2-Brake;3-Handling;4-Mass;    01234=5
    public static int [] boughtCar;
    public GameObject BuyButton;
    public GameObject MenuButtonLeft;
    public GameObject PlayButtonRight;
    public GameObject SelectCarButton;
    public GameObject speedUpgradeButton;
    public GameObject brakeUpgradeButton;
    public GameObject handlingUpgradeButton;
    public GameObject massUpgradeButton;
    public GameObject floatingMoneyPrefab;
    [SerializeField] private Text moneyPlayer;

    public Text UpgradePrice1;
    public Text UpgradePrice2;
    public Text UpgradePrice3;
    public Text UpgradePrice4;    

    [SerializeField] private ScriptableObject[] scriptableObjects;

    [Header("Display Scripts")]
    [SerializeField] private CarDisplay carDisplay;
    public static int currentCarIndex;

    public AudioSource SpendMoney;
    public AudioSource UpgradeSound;

    private void Awake()
    {        
        if (PlayButtonRight != null)
        {
            if (PlayerPrefs.HasKey("firstTimeEnter")) PlayButtonRight.SetActive(true);
            else PlayButtonRight.SetActive(false);
        }
        if (MenuButtonLeft != null)
        {
            if (PlayerPrefs.HasKey("firstTimeEnter")) MenuButtonLeft.SetActive(true);
            else MenuButtonLeft.SetActive(false);
        }
        //carDisplay = GameObject.Find("CarDisplay").transform.GetComponent<CarDisplay>();
        boughtCar = new int[scriptableObjects.Length];
        upgradeCar = new int[scriptableObjects.Length, 5];
        if (PlayerPrefs.HasKey("Money")) money = PlayerPrefs.GetInt("Money");        /////Proverka deneg
        else money = 1500;
        if (moneyPlayer != null) moneyPlayer.text = money.ToString();

        if (PlayerPrefs.HasKey("carChoose"))
        {
            currentCarIndex = PlayerPrefs.GetInt("carChoose");
            //Debug.Log("currentCarIndex "+currentCarIndex);
        }
        ChangeCar(0);
    }
    private void FixedUpdate()
    {
        if (moneyPlayer != null) moneyPlayer.text = money.ToString();
    }

    public void ChangeCar(int _index)
    {
        currentCarIndex += _index;
        if (currentCarIndex < 0) currentCarIndex = scriptableObjects.Length - 1;
        if (currentCarIndex > scriptableObjects.Length - 1) currentCarIndex = 0;        
        if (carDisplay != null) carDisplay.UpdateCar((Car)scriptableObjects[currentCarIndex]);

        if (PlayerPrefs.HasKey("carGarage" + currentCarIndex))
        {
            boughtCar[currentCarIndex] = PlayerPrefs.GetInt("carGarage" + currentCarIndex);

            if(BuyButton != null)// For avoid mistake on other levels
            {
                costUpgrade = costCar / 10;
                UpgradePrice1.text = (costUpgrade + 20).ToString();
                UpgradePrice2.text = costUpgrade.ToString();
                UpgradePrice3.text = (costUpgrade + 5).ToString();
                UpgradePrice4.text = (costUpgrade + 10).ToString();

                speedUpgradeButton.SetActive(true);
                brakeUpgradeButton.SetActive(true);
                handlingUpgradeButton.SetActive(true);
                massUpgradeButton.SetActive(true);

                BuyButton.SetActive(false);
                SelectCarButton.SetActive(true);

                for (int i = 1; i <= 4; i++) 
                {
                    if (PlayerPrefs.HasKey("Upgrade" + currentCarIndex + i ))
                    {                        
                        upgradeCar[currentCarIndex, i] = PlayerPrefs.GetInt("Upgrade" + currentCarIndex + i);
                        Debug.Log( "Upgrade № "+ i + " on car № " + currentCarIndex);
                    }
                    
                    if (upgradeCar[currentCarIndex,i] > 0)
                    {
                        switch (i)
                        {
                            case 1:
                                Debug.Log( "Upgrade bought № "+ i );
                                speedUpgradeButton.SetActive(false);
                                break;
                            case 2:
                                Debug.Log("Upgrade bought № " + i);
                                brakeUpgradeButton.SetActive(false);
                                break;
                            case 3:
                                Debug.Log("Upgrade bought № " + i);
                                handlingUpgradeButton.SetActive(false);
                                break;
                            case 4:
                                Debug.Log("Upgrade bought № " + i);
                                massUpgradeButton.SetActive(false);
                                break;
                        }
                    }
                }
            }
        }
        else
        {
            if (BuyButton != null)        
            {
                speedUpgradeButton.SetActive(false);
                brakeUpgradeButton.SetActive(false);
                handlingUpgradeButton.SetActive(false);
                massUpgradeButton.SetActive(false);
                BuyButton.SetActive(true);
                SelectCarButton.SetActive(false);
            }
        }
    }
    public void BuyCar()
    {
        if (money >= costCar)
        {
            FocusSoundController.tvBreak = 0;

            var go = Instantiate(floatingMoneyPrefab, moneyPlayer.transform.position + new Vector3(0f, -130f, 0f), Quaternion.identity, transform);
            go.GetComponent<Text>().text = "-" + costCar+"$".ToString();
            Destroy(go, 1f);
            SpendMoney.Play();

            PlayerPrefs.SetInt("firstTimeEnter", 1); //To check 1 bought in game
            PlayerPrefs.SetInt("Money", money -= costCar);
            PlayerPrefs.SetInt("carGarage" + currentCarIndex, boughtCar[currentCarIndex] = 1);
            PlayerPrefs.SetInt("carChoose", currentCarIndex);
            BuyButton.SetActive(false);
            SelectCarButton.SetActive(true);
            PlayButtonRight.SetActive(true);//for 1st playgame return buttons
            MenuButtonLeft.SetActive(true);//for 1st playgame return buttons

            speedUpgradeButton.SetActive(true);
            brakeUpgradeButton.SetActive(true);
            handlingUpgradeButton.SetActive(true);
            massUpgradeButton.SetActive(true);
            costUpgrade = costCar / 10;
            UpgradePrice1.text = (costUpgrade + 20).ToString();
            UpgradePrice2.text = costUpgrade.ToString();
            UpgradePrice3.text = (costUpgrade + 5).ToString();
            UpgradePrice4.text = (costUpgrade + 10).ToString();
            moneyPlayer.text = money.ToString();

            //carDisplay.UpdateCar((Car)scriptableObjects[currentCarIndex]);            
        }
        else
        {
            // PourSound.Play(); //kva kva Not enough money
        }
    }
    public void BuyUpgradeSpeed()
    {
        costUpgrade = (costCar / 10) + 20;
        if (money >= costUpgrade)
        {
            var go = Instantiate(floatingMoneyPrefab, moneyPlayer.transform.position + new Vector3(0f, -130f, 0f), Quaternion.identity, transform);
            go.GetComponent<Text>().text = "-" + costUpgrade + "$".ToString();
            Destroy(go, 1f);
            SpendMoney.Play();

            speedUpgradeButton.SetActive(false);
            PlayerPrefs.SetInt("Money", money -= costUpgrade);
            PlayerPrefs.SetInt("Upgrade" + currentCarIndex + 1, upgradeCar[currentCarIndex,1] = 1);   // Save Detal` +nomercar+nomer detal, ustanovlen on\off
            moneyPlayer.text = money.ToString();
            Debug.Log("Bought Speed Upgrade on car # "+currentCarIndex);
            carDisplay.UpdateCar((Car)scriptableObjects[currentCarIndex]);
            UpgradeSound.Play();
        }
    }
    public void BuyUpgradeBrake()
    {
        costUpgrade = costCar / 10;
        if (money >= costUpgrade)
        {
            var go = Instantiate(floatingMoneyPrefab, moneyPlayer.transform.position + new Vector3(0f, -130f, 0f), Quaternion.identity, transform);
            go.GetComponent<Text>().text = "-" + costUpgrade + "$".ToString();
            Destroy(go, 1f);
            SpendMoney.Play();

            brakeUpgradeButton.SetActive(false);
            PlayerPrefs.SetInt("Money", money -= costUpgrade);
            PlayerPrefs.SetInt("Upgrade" + currentCarIndex + 2, upgradeCar[currentCarIndex, 2] = 1);   // Save Detal` +nomercar+nomer detal, ustanovlen on\off
            moneyPlayer.text = money.ToString();
            Debug.Log("Bought Brake Upgrade on car # " + currentCarIndex);
            carDisplay.UpdateCar((Car)scriptableObjects[currentCarIndex]);
            UpgradeSound.Play();
        }
    }
    public void BuyUpgradeHandling()
    {
        costUpgrade = (costCar / 10) + 5;
        if (money >= costUpgrade)
        {
            var go = Instantiate(floatingMoneyPrefab, moneyPlayer.transform.position + new Vector3(0f, -130f, 0f), Quaternion.identity, transform);
            go.GetComponent<Text>().text = "-" + costUpgrade + "$".ToString();
            Destroy(go, 1f);
            SpendMoney.Play();

            handlingUpgradeButton.SetActive(false);
            PlayerPrefs.SetInt("Money", money -= costUpgrade);
            PlayerPrefs.SetInt("Upgrade" + currentCarIndex + 3, upgradeCar[currentCarIndex, 3] = 1);   // Save Detal` +nomercar+nomer detal, ustanovlen on\off
            moneyPlayer.text = money.ToString();
            Debug.Log("Bought Handling Upgrade on car # " + currentCarIndex);
            carDisplay.UpdateCar((Car)scriptableObjects[currentCarIndex]);
            UpgradeSound.Play();
        }
    }
    public void BuyUpgradeMass()
    {
        costUpgrade = (costCar / 10) + 10;
        if (money >= costUpgrade)
        {
            var go = Instantiate(floatingMoneyPrefab, moneyPlayer.transform.position + new Vector3(0f, -130f, 0f), Quaternion.identity, transform);
            go.GetComponent<Text>().text = "-" + costUpgrade + "$".ToString();
            Destroy(go, 1f);
            SpendMoney.Play();

            massUpgradeButton.SetActive(false);
            PlayerPrefs.SetInt("Money", money -= costUpgrade);
            PlayerPrefs.SetInt("Upgrade" + currentCarIndex + 4, upgradeCar[currentCarIndex, 4] = 1);   // Save Detal` +nomercar+nomer detal, ustanovlen on\off
            moneyPlayer.text = money.ToString();
            Debug.Log("Bought Mass Upgrade on car # " + currentCarIndex);
            carDisplay.UpdateCar((Car)scriptableObjects[currentCarIndex]);
            UpgradeSound.Play();
        }
    }
    public void SelectCar()
    {
        PlayerPrefs.SetInt("carChoose", currentCarIndex);
        UpgradeSound.Play();
        SelectCarButton.SetActive(false);
    }
}