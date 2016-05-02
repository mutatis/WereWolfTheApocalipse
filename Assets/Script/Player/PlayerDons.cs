using UnityEngine;
using System.Collections;

public class PlayerDons : MonoBehaviour
{
    public PlayerDom player;

    public string nome;

    GameObject[] obj;

    void Update()
    {
        switch(player)
        {
            case PlayerDom.Player1:
                if (Input.GetKey(KeyCode.Joystick1Button5))
                {
                    if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                    {
                        PressButtonA("P1");
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                    {
                        PressButtonB("P1");
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                    {
                        PressButtonX("P1");
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                    {
                        PressButtonY("P1");
                    }
                }
                break;
        }
    }

    void CalloftheWyld()
    {
        obj = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < obj.Length; i ++)
        {
            if(obj[i].GetComponent<PlayerDons>().nome != nome)
            {
                obj[i].GetComponent<PlayerStatus>().call = true;
                obj[i].GetComponent<PlayerController>().call = true;
            }
        }
        StartCoroutine("Call");
    }

    IEnumerator Call()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i].GetComponent<PlayerDons>().nome != nome)
            {
                obj[i].GetComponent<PlayerStatus>().call = false;
                obj[i].GetComponent<PlayerController>().call = false;
            }
        }
    }

    void PressButtonA(string player)
    {
        if(PlayerPrefs.GetInt(nome + player + "ButtonA") == 0)
        {
            CalloftheWyld();
        }
    }

    void PressButtonB(string player)
    {

    }

    void PressButtonX(string player)
    {

    }

    void PressButtonY(string player)
    {

    }
}

public enum PlayerDom
{
    Player1,
    Player2,
    Player3,
    Player4
}
