using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ManagerPlayer : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Players", 0);
    }

    public void Um()
    {
        PlayerPrefs.SetInt("Players", 1);
        SceneManager.LoadScene("SelecaoPersonagem");
    }

    public void Dois()
    {
        PlayerPrefs.SetInt("Players", 2);
        SceneManager.LoadScene("SelecaoPersonagem");
    }
}
