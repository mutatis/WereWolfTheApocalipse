using UnityEngine;
using System.Collections;

public class PlayerDonsAndarilho : MonoBehaviour
{
    public float[] cost;

    public PlayerController controller;

    public PlayerDom player;

    public GameObject invoque, eletrecute, pack;

    public Transform posInvoque;

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
                    else if (Input.GetKeyDown(KeyCode.Joystick1Button4))
                    {
                        PressButtonL1("P1");
                    }
                }
                break;
            case PlayerDom.Player2:
                if (Input.GetKey(KeyCode.Joystick2Button5))
                {
                    if (Input.GetKeyDown(KeyCode.Joystick2Button0))
                    {
                        PressButtonA("P2");
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick2Button1))
                    {
                        PressButtonB("P2");
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick2Button2))
                    {
                        PressButtonX("P2");
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick2Button3))
                    {
                        PressButtonY("P2");
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick2Button4))
                    {
                        PressButtonL1("P2");
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
            if(obj[i].GetComponent<PlayerController>().nome != nome)
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
            if (obj[i].GetComponent<PlayerController>().nome != nome)
            {
                obj[i].GetComponent<PlayerStatus>().call = false;
                obj[i].GetComponent<PlayerController>().call = false;
            }
        }
        StopCoroutine("Call");
    }

    void FabricoftheMind()
    {
        GameObject temp;
        temp = Instantiate(invoque, posInvoque.position, transform.rotation) as GameObject;
        temp.GetComponent<Pacman>().obj = gameObject;
    }

    void Eletrecute()
    {
        GameObject tempObj;
        tempObj = Instantiate(eletrecute, transform.position, transform.rotation) as GameObject;
        if(gameObject.transform.localScale.x < 0)
        {
            tempObj.transform.localScale = new Vector3(tempObj.transform.localScale.x * -1, tempObj.transform.localScale.y, tempObj.transform.localScale.z);
        }
    }

    void Pack1()
    {
        pack.SetActive(true);
    }

    void PressButtonL1(string player)
    {
        Pack1();
    }

    void PressButtonA(string player)
    {
        if(PlayerPrefs.GetInt(nome + player + "ButtonA") == 0 && controller.gnose >= cost[0])
        {
            CalloftheWyld();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonA") == 1 && controller.gnose >= cost[1])
        {
            FabricoftheMind();
            controller.gnose -= cost[1];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonA") == 2 && controller.gnose >= cost[2])
        {
            Eletrecute();
            controller.gnose -= cost[2];
        }
    }

    void PressButtonB(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonB") == 0 && controller.gnose >= cost[0])
        {
            CalloftheWyld();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonB") == 1 && controller.gnose >= cost[1])
        {
            FabricoftheMind();
            controller.gnose -= cost[1];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonB") == 2 && controller.gnose >= cost[2])
        {
            Eletrecute();
            controller.gnose -= cost[2];
        }
    }

    void PressButtonX(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonX") == 0 && controller.gnose >= cost[0])
        {
            CalloftheWyld();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonX") == 1 && controller.gnose >= cost[1])
        {
            FabricoftheMind();
            controller.gnose -= cost[1];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonX") == 2 && controller.gnose >= cost[2])
        {
            Eletrecute();
            controller.gnose -= cost[2];
        }
    }

    void PressButtonY(string player)
    {
        if (PlayerPrefs.GetInt(nome + player + "ButtonY") == 0 && controller.gnose >= cost[0])
        {
            CalloftheWyld();
            controller.gnose -= cost[0];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonY") == 1 && controller.gnose >= cost[1])
        {
            FabricoftheMind();
            controller.gnose -= cost[1];
        }
        else if (PlayerPrefs.GetInt(nome + player + "ButtonY") == 2 && controller.gnose >= cost[2])
        {
            Eletrecute();
            controller.gnose -= cost[2];
        }
    }
}

public enum PlayerDom
{
    Player1,
    Player2,
    Player3,
    Player4
}
