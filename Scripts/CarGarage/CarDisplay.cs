using UnityEngine;
using UnityEngine.UI;

public class CarDisplay : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] private Text carName;
    [SerializeField] private Text carDescription;
    [SerializeField] private Text carPrice;

    [Header("Stats")]
    [SerializeField] private Image speed;
    [SerializeField] private Image speedBuyEffect;
    [SerializeField] private Image brake;
    [SerializeField] private Image brakeBuyEffect;
    [SerializeField] private Image handling;
    [SerializeField] private Image handlingBuyEffect;
    [SerializeField] private Image mass;
    [SerializeField] private Image massBuyEffect;

    [Header("Car Model")]
    [SerializeField] private GameObject carModel;

    public Gradient gradient;
    public Gradient gradientInvers;

    private float speedBuyUpgradeNumber;
    private float brakeBuyUpgradeNumber;
    private float handlingBuyUpgradeNumber;
    private float massBuyUpgradeNumber;

    private void Awake()
    {
        //carModel = GameObject.Find("CarContainer");
    }

    public void EnterSpeedEffectButton()
    {
        speedBuyEffect.enabled = true;
        speedBuyEffect.fillAmount = speedBuyUpgradeNumber / 400;
        //Debug.Log("COLOR True Button");
    }
    public void EnterBrakeEffectButton()
    {
        brakeBuyEffect.enabled = true;
        brakeBuyEffect.fillAmount = brakeBuyUpgradeNumber / 400;
        //Debug.Log("COLOR True Button");
    }
    public void EnterHandlingEffectButton()
    {
        handlingBuyEffect.enabled = true;
        handlingBuyEffect.fillAmount = handlingBuyUpgradeNumber / 40;
        //Debug.Log("COLOR True Button");
    }
    public void EnterMassEffectButton()
    {
        massBuyEffect.enabled = true;
        massBuyEffect.fillAmount = massBuyUpgradeNumber / 1200;
        mass.enabled = false;
        //Debug.Log("COLOR True Button");
    }
    public void ExitShowEffectButton()
    {
        speedBuyEffect.enabled = false;
        brakeBuyEffect.enabled = false;
        handlingBuyEffect.enabled = false;
        massBuyEffect.enabled = false;
        mass.enabled = true;
        //Debug.Log("COLOR false Button");
    }
    public void UpdateCar(Car _newCar)
    {
        carName.text = _newCar.carName;
        carDescription.text = _newCar.carDescription;
        carPrice.text = _newCar.carPrice.ToString();   //" $"

        //if (gradient != null)
        if (speedBuyEffect != null)
        {
            speedBuyEffect.enabled = false;
            speedBuyUpgradeNumber = 1.15f * _newCar.Torque;

            brakeBuyEffect.enabled = false;
            brakeBuyUpgradeNumber = 1.3f * _newCar.Brake;

            handlingBuyEffect.enabled = false;
            handlingBuyUpgradeNumber = 2 + _newCar.Steer;

            massBuyEffect.enabled = false;
            massBuyUpgradeNumber = _newCar.Mass - 100;
            mass.enabled = true;

            speed.fillAmount = (float)_newCar.Torque / 400;
            speed.color = gradient.Evaluate(speed.fillAmount);            

            brake.fillAmount = (float)_newCar.Brake / 400;
            brake.color = gradient.Evaluate(brake.fillAmount);

            handling.fillAmount = (float)_newCar.Steer / 40;
            handling.color = gradient.Evaluate(handling.fillAmount);

            mass.fillAmount = (float)_newCar.Mass / 1200;
            mass.color = gradientInvers.Evaluate(mass.fillAmount);
        }
        
        OldCarMove.motorTorque = _newCar.Torque;
        OldCarMove.motorBrake = _newCar.Brake;
        OldCarMove.maxSteer = _newCar.Steer;
        OldCarMove.massCar = _newCar.Mass;

        Debug.Log(OldCarMove.motorTorque + "Was HP");
        Debug.Log(OldCarMove.motorBrake + "Was Brake");
        Debug.Log(OldCarMove.maxSteer + "Was Angle");
        Debug.Log(OldCarMove.massCar + "Was Mass");

        for (int i = 1; i <= 4; i++)
        {
            ScriptableObjectChanger.upgradeCar[ScriptableObjectChanger.currentCarIndex, i] = PlayerPrefs.GetInt("Upgrade" + ScriptableObjectChanger.currentCarIndex + i);

            if (ScriptableObjectChanger.upgradeCar[ScriptableObjectChanger.currentCarIndex, i] > 0)  //Esli uzhe kupil ,pribav har-ki
            {
                switch (i)
                {
                    case 1:
                        //OldCarMove.motorTorque = 200 + _newCar.Torque;
                        OldCarMove.motorTorque =  1.15f * _newCar.Torque;
                        speed.fillAmount = speedBuyUpgradeNumber / 400; 
                        Debug.Log(OldCarMove.motorTorque + "Upgrade HP");
                        break;
                    case 2:
                        OldCarMove.motorBrake = 1.3f * _newCar.Brake;
                        brake.fillAmount = brakeBuyUpgradeNumber / 400;
                        Debug.Log(OldCarMove.motorBrake + "Upgrade Brake");
                        break;
                    case 3:
                        OldCarMove.maxSteer = 2 + _newCar.Steer;
                        handling.fillAmount = handlingBuyUpgradeNumber / 40;
                        Debug.Log(OldCarMove.maxSteer + "Upgrade Handling");
                        break;
                    case 4:
                        OldCarMove.massCar = _newCar.Mass - 100;
                        mass.fillAmount = massBuyUpgradeNumber / 1200;
                        Debug.Log(OldCarMove.massCar + "Upgrade Mass");
                        break;
                }
            }
        }

        ScriptableObjectChanger.costCar = _newCar.carPrice;

        if (carModel.transform.childCount > 0)
            Destroy(carModel.transform.GetChild(0).gameObject);

        Instantiate(_newCar.carModel, carModel.transform.position, carModel.transform.rotation, carModel.transform);
    }
    
}