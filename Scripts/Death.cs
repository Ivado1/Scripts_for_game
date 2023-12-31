﻿using UnityEngine;
using UnityEngine.SceneManagement;
public class Death : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Debug.Log("Player touch water");
        }
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Debug.Log("Restart");
    }
}