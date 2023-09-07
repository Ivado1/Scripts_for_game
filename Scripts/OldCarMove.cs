using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
//Player`s car 
public class OldCarMove : MonoBehaviour
{
    public Transform centerOfMass;

    public WheelCollider wheelColliderFrontLeft;
    public WheelCollider wheelColliderFrontRight;
    public WheelCollider wheelColliderBackLeft;
    public WheelCollider wheelColliderBackRight;

    public Transform wheelFrontLeft;
    public Transform wheelFrontRight;
    public Transform wheelBackLeft;
    public Transform wheelBackRight;
 
    private Rigidbody _rigidbody;
    public static float motorTorque;
    public float motorCurrentTorque;
    public static float motorBrake;
    public static float maxSteer;
    public static float massCar;

    //public GameObject MoveCanvas;

    public GameObject ExhausteSmokeEffect;
    [SerializeField] private Transform ExhausteSmokePositionL;
    [SerializeField] private Transform ExhausteSmokePositionR;

    public AudioSource soundEngine;
    public AudioSource starAudioSource;
    
    public bool PCPlayFrom;
    public bool Shakering;
    private int ShakeringNumber;

    void Start()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;

        //Debug.Log(motorTorque + " HP");
        //Debug.Log(motorBrake + " Brake");
        //Debug.Log(maxSteer + " Angel");
        //Debug.Log(massCar + " KG");
        
        int SceneIndex = SceneManager.GetActiveScene().buildIndex;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.mass = massCar;
        _rigidbody.centerOfMass = centerOfMass.localPosition;

        //MoveCanvas = GameObject.Find("Canvas").transform.GetChild(0).gameObject;

        if (SceneManager.GetActiveScene().buildIndex != 0) soundEngine.Play();
        
        if (Application.isMobilePlatform) PCPlayFrom = false;
        else PCPlayFrom = true;
    }

    void FixedUpdate()
    {
        if (PCPlayFrom)
        {
            wheelColliderBackLeft.motorTorque = wheelColliderBackRight.motorTorque = wheelColliderFrontLeft.motorTorque = wheelColliderFrontRight.motorTorque = Input.GetAxis("Vertical") * motorTorque; 
            //PC MOVE ---------  // wheelColliderBackRight.motorTorque = Input.GetAxis("Vertical") * motorTorque; //R 2wd pc 
            wheelColliderFrontRight.steerAngle = wheelColliderFrontLeft.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
        }
        motorCurrentTorque = _rigidbody.velocity.magnitude/15;
        if (soundEngine.pitch < 1.2f) soundEngine.pitch = 0.5f + motorCurrentTorque;
        else
        { 
            soundEngine.pitch = 1.2f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 4f)
        {
            ShakeringNumber = 1;
            CameraFollow.shakeNumber = 1;
            //Debug.Log("HIT");
        }
    }
    public void ButtonMoveLeft() => wheelColliderFrontRight.steerAngle = wheelColliderFrontLeft.steerAngle = -maxSteer; // -1* inversion
    public void ButtonMoveRight() => wheelColliderFrontRight.steerAngle = wheelColliderFrontLeft.steerAngle = maxSteer;
    public void ButtonBrake() => wheelColliderBackLeft.motorTorque = wheelColliderBackRight.motorTorque = wheelColliderFrontLeft.motorTorque = wheelColliderFrontRight.motorTorque = -motorBrake; //-1* inversion
    public void ButtonThrottle() => wheelColliderBackLeft.motorTorque = wheelColliderBackRight.motorTorque = wheelColliderFrontLeft.motorTorque = wheelColliderFrontRight.motorTorque = motorTorque;

    public void ReleaseSteerButton() => wheelColliderFrontRight.steerAngle = wheelColliderFrontLeft.steerAngle = 0;
    //public void ReleaseMoveButton() => wheelColliderBackLeft.motorTorque = wheelColliderBackRight.motorTorque = 0;  // 2 Rwd
    public void ReleaseMoveButton() => wheelColliderBackLeft.motorTorque = wheelColliderBackRight.motorTorque = wheelColliderFrontLeft.motorTorque = wheelColliderFrontRight.motorTorque = 0;  

    void Update()
    {
        var pos = Vector3.zero;
        var rot = Quaternion.identity;

        wheelColliderFrontLeft.GetWorldPose(out pos, out rot);
        wheelFrontLeft.position = pos;
        wheelFrontLeft.rotation = rot;

        wheelColliderFrontRight.GetWorldPose(out pos, out rot);
        wheelFrontRight.position = pos;
        wheelFrontRight.rotation = rot * Quaternion.Euler(0, 180, 0);

        wheelColliderBackLeft.GetWorldPose(out pos, out rot);
        wheelBackLeft.position = pos;
        wheelBackLeft.rotation = rot;

        wheelColliderBackRight.GetWorldPose(out pos, out rot);
        wheelBackRight.position = pos;
        wheelBackRight.rotation = rot * Quaternion.Euler(0, 180, 0);
        
        if (ShakeringNumber==1) ShakeringNumber = 0; // For fast shake camera(crash effect)
    }
    private void OnTriggerEnter(Collider other)
    {
        int SceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (other.CompareTag("Star1"))
        {
            LevelSelection.levelStars[SceneIndex] += 1;
            StarsManager.starNumber[1] = 1;
            other.gameObject.SetActive(false);            
            starAudioSource.Play();
            
            //Debug.Log("star 1 collected");
        }
        if (other.CompareTag("Star2"))
        {
            LevelSelection.levelStars[SceneIndex] += 1;
            StarsManager.starNumber[2] = 1;
            other.gameObject.SetActive(false);
            starAudioSource.Play();
        }
        if (other.CompareTag("Star3"))
        {
            LevelSelection.levelStars[SceneIndex] += 1;
            StarsManager.starNumber[3] = 1;
            other.gameObject.SetActive(false);
            starAudioSource.Play();
        }
    }    
}
