using UnityEngine;
//
public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public Vector3 offset; // Move camera (X,Y,Z)(Up,Down,Left,Right) put needs parameters
    public Vector3 eulerRotation; // Rotate camera
    public float damper; // Follow effect(0-slow, high- faster)

    //private float shakeMagnitude = 0.05f, shakeTime = 0.5f; //Stronger shake
    private float shakeMagnitude = 0.02f, shakeTime = 0.2f;
    public Camera mainCamera;

    static public int shakeNumber;

    void Start()
    {
        Target = GameObject.Find("CarContainer").transform.GetChild(0); //Camera find player in scene(Hierarchy)  
        transform.eulerAngles = eulerRotation;
        ShakeCamera();
    }
    void Update()
    {
        if (Target == null)
            return;
        transform.position = Vector3.Lerp(transform.position, Target.position + offset, damper * Time.deltaTime);//Camera follow the car 
        //transform.eulerAngles = eulerRotation; // On if need to add updating rotation  
        if (shakeNumber == 1)
        {
            ShakeCamera();
            //Debug.Log("shake true");
        }
    }

    public void ShakeCamera()
    {
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