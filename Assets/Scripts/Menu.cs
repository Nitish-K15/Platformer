using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    private void Update()
    {
        Player.count = 0;
        if (Input.GetKey(KeyCode.Return))
            SceneManager.LoadScene("Game");
        if (Input.GetKey(KeyCode.M))
            SceneManager.LoadScene("Main Menu");
    }
}
