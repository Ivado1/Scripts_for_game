using UnityEngine;

public class SoundScene : MonoBehaviour
{
    public AudioSource Sound;
    void Start()
    {
        if (Sound != null) Sound.Play();
    }
}
