using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectPersonagem : MonoBehaviour
{
    public Image[] player;

    int select;

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
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Joystick1Button11) || Input.GetKeyDown(KeyCode.Joystick1Button10))
            {
                Muda();
            }
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
