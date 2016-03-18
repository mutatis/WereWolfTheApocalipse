using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpcoesNavigat : MonoBehaviour
{

    public Controllopcao[] efeito;

    int x;

    bool podeDpad;
    bool editando = false;
    
    void Start()
    {
        efeito[x].Select();
    }

	void Update ()
    {
        if (podeDpad && !editando)
        {
            if (Input.GetAxisRaw("DpadYP1") < 0)
            {
                Proximo();
            }
            else if (Input.GetAxisRaw("DpadYP1") > 0)
            {
                Anterior();
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                SceneManager.LoadScene("Menu");
            }
        }

        if(!editando && Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            editando = true;
        }

        if(editando && podeDpad)
        {
            if(Input.GetAxisRaw("DpadXP1") > 0)
            {
                efeito[x].Aumenta();
                podeDpad = false;
            }
            else if (Input.GetAxisRaw("DpadXP1") < 0)
            {
                efeito[x].Diminui();
                podeDpad = false;
            }
            else if(Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                editando = false;
            }
        }

        if (Input.GetAxisRaw("DpadYP1") == 0 && Input.GetAxisRaw("DpadXP1") == 0)
        {
            podeDpad = true;
        }
    }

    void Anterior()
    {
        podeDpad = false;
        if (x > 0)
        {
            x--;
        }
        for (int i = 0; i < efeito.Length; i++)
        {
            if (i == x)
            {
                efeito[i].Select();
            }
            else
            {
                efeito[i].Deselect();
            }
        }
    }

    void Proximo()
    {
        podeDpad = false;
        if (x < (efeito.Length - 1))
        {
            x++;
        }
        for (int i = 0; i < efeito.Length; i++)
        {
            if (i == x)
            {
                efeito[i].Select();
            }
            else
            {
                efeito[i].Deselect();
            }
        }
    }
}
