using UnityEngine;

public class MoveButtonsMobile : MonoBehaviour
{
    private GameObject carScriptsMovement;

    void Start()
    {
        carScriptsMovement = GameObject.Find("CarContainer").transform.GetChild(0).gameObject;
        //carRightMovement = GameObject.Find("Canvas").transform.GetChild(0).transform.Find("Right").gameObject;
    }
    public void LeftButton()
    {
       // Debug.Log("MoveLeft");
        carScriptsMovement.GetComponent<OldCarMove>().ButtonMoveLeft();
    }
    public void RightButton()
    {
        carScriptsMovement.GetComponent<OldCarMove>().ButtonMoveRight();
    }
    public void Forward()
    {
        carScriptsMovement.GetComponent<OldCarMove>().ButtonThrottle();
    }
    public void Brake()
    {
        carScriptsMovement.GetComponent<OldCarMove>().ButtonBrake();
    }
    public void ReleaseButtonGo()
    {
        carScriptsMovement.GetComponent<OldCarMove>().ReleaseMoveButton();
    }
    public void ReleaseButtonSteer()
    {
        carScriptsMovement.GetComponent<OldCarMove>().ReleaseSteerButton();
    }
}
