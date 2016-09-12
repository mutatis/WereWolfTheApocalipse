using UnityEngine;
using System.Collections;

public class ManagerStartGame : MonoBehaviour
{
    public GameObject andarilho, presas;

    GameObject temp;

    void Start()
    {
        if (PlayerPrefs.GetInt("Players") <= 1)
        {
            if (PlayerPrefs.GetInt("Escolha") == 1)
            {
                Instantiate(andarilho, new Vector3(-25, 2, 0), transform.rotation);
            }
            else if (PlayerPrefs.GetInt("Escolha") == 2)
            {
				Instantiate(presas, new Vector3(-25, 2, 0), transform.rotation);
            }
        }
        else
        {
            Instantiate(andarilho, new Vector3(0, 0, 0), transform.rotation);
            temp = Instantiate(presas, new Vector3(-5, 0, 0), transform.rotation) as GameObject;
            temp.GetComponent<PlayerStats>().player = Player.Player2;
            temp.GetComponent<PlayerDonsPresas>().player = PlayerDomPresas.Player2;
        }
    }
}