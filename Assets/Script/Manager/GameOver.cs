using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            SceneManager.LoadScene("Jogo");
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
