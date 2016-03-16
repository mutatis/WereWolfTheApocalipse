using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectPersonagem : MonoBehaviour
{
    public Image[] player;

    public GameObject[] atributos;

    int select;

    bool pode = true;

    void Start()
    {
        if(PlayerPrefs.GetInt("Players") == 1)
        {
            player[0].color = new Color(1, 1, 1, 1);
            player[1].color = new Color(1, 1, 1, 0.3f);
            select = 0;
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Players") == 1)
        {
            if (pode)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Joystick1Button11) || Input.GetKeyDown(KeyCode.Joystick1Button10))
                {
                    Muda();
                }
                else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    Select();
                }
            }
            else if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                atributos[select].SetActive(false);
                pode = true;
            }

        }
    }

    void Select()
    {
        if (select == 0)
        {
            atributos[select].SetActive(true);
            pode = false;
            return;
        }
        else if (select == 1)
        {
            atributos[select].SetActive(true);
            pode = false;
            return;
        }
    }

    void Muda()
    {
        if (select == 0)
        {
            player[1].color = new Color(1, 1, 1, 1);
            player[0].color = new Color(1, 1, 1, 0.3f);
            select = 1;
            return;
        }
        else if (select == 1)
        {
            player[0].color = new Color(1, 1, 1, 1);
            player[1].color = new Color(1, 1, 1, 0.3f);
            select = 0;
            return;
        }
    }
}
