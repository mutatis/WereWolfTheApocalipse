using UnityEngine;
using System.Collections;

public class AtributoTroca : MonoBehaviour
{
    public PaiAtributosEdit meuNumero;

    public GameObject[] desliga;

    public GameObject proximo;
    public GameObject anterior;

    bool podeDpad = true;
    bool podeDpad2 = true;

    void Update ()
    {
        if (SelectPersonagem.personagem.select == meuNumero.meuNumero)
        {
            if (podeDpad)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("DpadXP1") > 0)
                {
                    Proximo();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("DpadXP1") < 0)
                {
                    Anterior();
                }
            }
        }
        else if (SelectPersonagem.personagem.select2 == meuNumero.meuNumero)
        {
            if (podeDpad2)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("DpadXP2") > 0)
                {
                    Proximo();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("DpadXP2") < 0)
                {
                    Anterior();
                }
            }
        }

        if (Input.GetAxisRaw("DpadXP1") == 0)
        {
            podeDpad = true;
        }

        if (Input.GetAxisRaw("DpadXP2") == 0)
        {
            podeDpad2 = true;
        }
    }

    void Proximo()
    {
        for(int i = 0; i < desliga.Length; i++)
        {
            desliga[i].SetActive(false);
        }
        podeDpad = false;
        podeDpad2 = false;
        proximo.SetActive(true);
    }

    void Anterior()
    {
        for (int i = 0; i < desliga.Length; i++)
        {
            desliga[i].SetActive(false);
        }
        podeDpad = false;
        podeDpad2 = false;
        anterior.SetActive(true);
    }
}
