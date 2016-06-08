using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string play, retorno;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            SceneManager.LoadScene(play);
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            SceneManager.LoadScene(retorno);
        }
    }
}
