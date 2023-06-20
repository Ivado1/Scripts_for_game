using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    AudioSource Sound;

    void Start()
    {
        Sound=GetComponent<AudioSource>();    
    }   
    public void PlauSound()
    {
        Sound.Play();
    }    
}
