using UnityEngine;
using System.Collections;

public class PlayerDons : MonoBehaviour
{
    public PlayerDom player;

    public string nome;

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

    void PressButtonA(string player)
    {
        if(PlayerPrefs.GetInt(nome + player + "ButtonA") == 0)
        {
            PlayerPrefs.SetInt("CalloftheWild", 1);
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
