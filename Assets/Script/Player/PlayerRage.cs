using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRage : MonoBehaviour
{
    public GameObject[] obj;

    GameObject temp;

    public int tipo;

    public GameObject[] lua;

    float rageMaxTemp;

    void Update()
    {
        if (obj.Length <= 0)
        {
            Procura();
        }
        else
        {
            rageMaxTemp = temp.GetComponent<PlayerStats>().playerStatus.rageMax;
        }
        if ((rageMaxTemp / 6) * 1 > temp.GetComponent<PlayerStats>().rage)
        {
            print("dshfsdhsjdsjfposkfoijso");
            lua[1].SetActive(true);
        }
        else
        {
            lua[1].GetComponent<Animator>().SetBool("Normal", true);
        }

        if ((rageMaxTemp / 6) * 2 > temp.GetComponent<PlayerStats>().rage && (rageMaxTemp / 6) * 1 < temp.GetComponent<PlayerStats>().rage)
        {
            lua[2].SetActive(true);
        }
        else
        {
            lua[2].GetComponent<Animator>().SetBool("Normal", true);
        }

        if ((rageMaxTemp / 6) * 3 > temp.GetComponent<PlayerStats>().rage && (rageMaxTemp / 6) * 2 < temp.GetComponent<PlayerStats>().rage)
        {
            lua[3].SetActive(true);
        }
        else
        {
            lua[3].GetComponent<Animator>().SetBool("Normal", true);
        }

        if ((rageMaxTemp / 6) * 4 > temp.GetComponent<PlayerStats>().rage && (rageMaxTemp / 6) * 3 < temp.GetComponent<PlayerStats>().rage)
        {
            lua[4].SetActive(true);
        }
        else
        {
            lua[4].GetComponent<Animator>().SetBool("Normal", true);
        }

        if ((rageMaxTemp / 6) * 5 > temp.GetComponent<PlayerStats>().rage && (rageMaxTemp / 6) * 4 < temp.GetComponent<PlayerStats>().rage)
        {
            lua[5].SetActive(true);
        }
        else
        {
            lua[5].GetComponent<Animator>().SetBool("Normal", true);
        }
    }

    void Procura()
    {
        obj = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < obj.Length; i++)
        {
            switch (tipo)
            {
                case 1:
                    if (obj[i].GetComponent<PlayerStats>().player == Player.Player1)
                    {
                        temp = obj[i];
                    }
                    break;
                case 2:
                    if (obj[i].GetComponent<PlayerStats>().player == Player.Player2)
                    {
                        temp = obj[i];
                    }
                    break;
            }
        }
    }
}