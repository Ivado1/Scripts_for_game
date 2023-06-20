using UnityEngine;

public class AnimHit : MonoBehaviour
{
    private Animator anim;
    private AudioSource Sound;

    void Start()
    {
        anim = GetComponent<Animator>();
        Sound = GetComponent<AudioSource>();
        
        Sound.pitch = 1;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1.8f)
        {
            anim.Play("cactusTouch", 0, 0f);
            Sound.pitch = Random.Range(0.6f,1.4f);
            Sound.Play();
            //Debug.Log(Sound.pitch);
            //Debug.Log(collision.relativeVelocity.magnitude);
            //anim.SetTrigger("cube");
        }
        Sound.pitch = 5f;
        Sound.Play();
        Sound.Play();        
    }
}
