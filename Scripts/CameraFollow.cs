using UnityEngine;
//
public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public Vector3 offset;
    public Vector3 eulerRotation;
    public float damper;

    //private float shakeMagnitude = 0.05f, shakeTime = 0.5f; //Stronger shake
    private float shakeMagnitude = 0.02f, shakeTime = 0.2f;
    public Camera mainCamera;

    static public int shakeNumber;

    void Start()
    {
        Target = GameObject.Find("CarContainer").transform.GetChild(0); /// working camera follow the car 
        transform.eulerAngles = eulerRotation;
        ShakeCamera();
    }
    void Update()
    {
        if (Target == null)
            return;
        transform.position = Vector3.Lerp(transform.position, Target.position + offset, damper * Time.deltaTime);
        
        if (shakeNumber == 1)
        {
            ShakeCamera();
            //Debug.Log("shake true");
        }
    }

    public void ShakeCamera()
    {
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }
    void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        Vector3 cameraIntermediatePostion = mainCamera.transform.position;
        cameraIntermediatePostion.x += cameraShakingOffsetX;
        cameraIntermediatePostion.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermediatePostion;
    }
    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        shakeNumber = 0;

    }
}