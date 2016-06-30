using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRage : MonoBehaviour
{
    public GameObject[] obj;

    GameObject temp;

    public int tipo;

    public Slider sli;

    void Update()
    {
        if (obj.Length <= 0)
        {
            Procura();
        }
        else
        {
            sli.value = temp.GetComponent<PlayerStats>().rage;
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
        sli.maxValue = temp.GetComponent<PlayerStats>().playerStatus.rageMax;
    }
}