using UnityEngine;
using System.Collections;

public class PlatformAttach : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerCont;

    private Transform _Transform;
    public float distance;
    public float lapTime;

    private void Start()
    {
        Player= GameObject.Find("CarContainer").transform.GetChild(0).transform.GetChild(2).gameObject;
        PlayerCont = GameObject.Find("CarContainer");

        StartCoroutine(MoveUp());
        _Transform = gameObject.transform;
    }
    void FixedUpdate()
    {
        _Transform = gameObject.transform;
        _Transform.Translate(distance * Time.deltaTime, 0f, 0f);
        //_Transform.Translate(-3f * Time.deltaTime, visota * Time.deltaTime, 0f);
    }
    IEnumerator MoveUp()
    {
        distance *= 1;
        yield return new WaitForSeconds(lapTime);
        StartCoroutine(MoveDown());
    }
    IEnumerator MoveDown()
    {
        distance *= -1;
        yield return new WaitForSeconds(lapTime);
        StartCoroutine(MoveUp());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            PlayerCont.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            PlayerCont.transform.parent = null;
            //Player.transform.parent = null;
        }
    }    
}
