using UnityEngine;

public class PodiumMove : MonoBehaviour
{
    Vector3 rot = new Vector3(0, 0.5f, 0);
    
    void FixedUpdate()
    {
        transform.Rotate(rot);
    }
}
