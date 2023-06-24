using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject StarEffect; 

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(StarEffect, transform.position, Quaternion.identity);            
            //gameObject.SetActive(false);
        }
    }
}
