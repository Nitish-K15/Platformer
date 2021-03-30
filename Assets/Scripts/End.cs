using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public AudioClip[] clips = new AudioClip[2];
    private AudioSource source;
    public bool finish;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (finish == false)                                        //Check to see if its Win screen or GameOver screen
        {                                                           //Play audio accordingly
            source.clip = clips[0];
            source.Play();
        }
        else
        {
            source.clip = clips[1];
            source.Play();
        }
    }
    private void Update()
    {
        Player.count = 0;
        if (Input.GetKey(KeyCode.Return))
        {
            if (finish)                                                        //From Win screen to Main Menu 
                SceneManager.LoadScene("Main Menu");
            else
                SceneManager.LoadScene("Game");                                
        }
        if (Input.GetKey(KeyCode.M) && finish == false)
            SceneManager.LoadScene("Main Menu");                              //From GameOver screen to Main Menu
    }
}
