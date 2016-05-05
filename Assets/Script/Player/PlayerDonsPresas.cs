using UnityEngine;
using System.Collections;

public class PlayerDonsPresas : MonoBehaviour
{
    public float[] cost;

    public PlayerController controller;

    public PlayerStatus playerStatus;

    public PlayerDomPresas player;

    public GameObject granite;

    public Transform posGranite;

    public string nome;

    void Update()
    {
        switch (player)
        {
            case PlayerDomPresas.Player1:
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
            case PlayerDomPresas.Player2:
                if (Input.GetKey(KeyCode.Joystick1Button5))
                {
                    if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                    {
                        PressButtonA("P2");
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                    {
                        PressButtonB("P2");
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                    {
                        PressButtonX("P2");
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                    {
                        PressButtonY("P2");
                    }
                }
                break;
        }
    }

    void LunarArmor()
    {
        playerStatus.lunar = true;
        controller.lunar = true;
        StartCoroutine("Lunar");
    }

    IEnumerator Lunar()
    {
        yield return new WaitForSeconds(3);
        playerStatus.lunar = false;
        controller.lunar = false;
        StopCoroutine("Lunar");
    }

    void WallofGranite()
    {
        print(posGranite.position);
        Instantiate(granite, posGranite.position, transform.rotation);
    }

    void PressButtonA(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonA") == 0 && controller.gnose >= cost[0])
        {
            LunarArmor();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonA") == 1 && controller.gnose >= cost[1])
        {
            WallofGranite();
            controller.gnose -= cost[1];
        }
    }

    void PressButtonB(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonB") == 0 && controller.gnose >= cost[0])
        {
            LunarArmor();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonB") == 1 && controller.gnose >= cost[1])
        {
            WallofGranite();
            controller.gnose -= cost[1];
        }
    }

    void PressButtonX(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonX") == 0 && controller.gnose >= cost[0])
        {
            LunarArmor();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonX") == 1 && controller.gnose >= cost[1])
        {
            WallofGranite();
            controller.gnose -= cost[1];
        }
    }

    void PressButtonY(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonY") == 0 && controller.gnose >= cost[0])
        {
            LunarArmor();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonY") == 1 && controller.gnose >= cost[1])
        {
            WallofGranite();
            controller.gnose -= cost[1];
        }
    }
}

public enum PlayerDomPresas
{
    Player1,
    Player2,
    Player3,
    Player4
}