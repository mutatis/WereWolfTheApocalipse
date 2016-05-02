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
                if(Input.GetKey(KeyCode.Joystick1Button0))
                {

                }
                break;
        }
    }

    void PressButtonA(string player)
    {

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
