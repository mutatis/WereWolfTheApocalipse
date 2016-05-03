using UnityEngine;
using System.Collections;

public class ManagerStartGame : MonoBehaviour
{
    public GameObject andarilho, presas;

    void Start()
    {
        if(PlayerPrefs.GetInt("Escolha") == 1)
        {
            Instantiate(andarilho, new Vector3(0, 0, 0), transform.rotation);
        }
        else if(PlayerPrefs.GetInt("Escolha") == 2)
        {
            Instantiate(presas, new Vector3(0, 0, 0), transform.rotation);
        }
    }
}
