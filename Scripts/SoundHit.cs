using UnityEngine;

public class SoundHit : MonoBehaviour    
{
    private AudioSource Sound;
    void Start()
    {
        Sound = GetComponent<AudioSource>();
        Sound.pitch = 1;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1f)
        {
            Sound.pitch = Random.Range(0.6f, 1.4f);
            Sound.Play();
            //Debug.Log(Sound.pitch + " Rock" + collision.relativeVelocity.magnitude);
            //Debug.Log(collision.relativeVelocity.magnitude);            
        }
    }
}
