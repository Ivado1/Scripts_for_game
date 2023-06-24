using UnityEngine;
//Right mouse button -> Create (for template car with standart parameters)
[CreateAssetMenu(fileName = "New Car", menuName = "Scriptable Objects/Car")]
public class Car : ScriptableObject
{
    [Header("Description")]
    //public int carIndex;
    public string carName;
    public string carDescription;
    //public Color nameColor;

    [Header("Stats")]
    public int carPrice;
    public float Torque;
    public float Brake;
    public float Steer;
    public float Mass;

    [Header("3D model")]
    public GameObject carModel;
    
}