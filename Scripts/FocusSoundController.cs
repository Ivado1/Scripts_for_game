using UnityEngine;

public class FocusSoundController : MonoBehaviour
{
    static public int tvBreak;

    void OnApplicationFocus(bool lostFocus)  //true-out game / false-in game
    {
        if (tvBreak == 0)
        {
            Silence(!lostFocus);
            //Debug.Log("!!!!!");
        }
        else
        {
            lostFocus=true;
            //Debug.Log("lostFocus=true");
        }
    }

    private void Silence(bool silence)
    {
        // Or / And
        AudioListener.volume = silence ? 0 : 1;        
    }
}
