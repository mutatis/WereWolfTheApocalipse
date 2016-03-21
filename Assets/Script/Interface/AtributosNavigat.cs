using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AtributosNavigat : MonoBehaviour
{
    public PaiAtributosEdit meuNumero;

    public GameObject[] img;

    public GameObject[] description;

    public SelectPersonagem select;

    bool podeDpad = true;
    bool podeDpad2 = true;

    int x = 0;

    void Update()
    {
        if (SelectPersonagem.personagem.select == meuNumero.meuNumero)
        {
            if (podeDpad)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("DpadYP1") < 0)
                {
                    if (x < (img.Length - 1))
                        x++;

                    for (int i = 0; i < img.Length; i++)
                    {
                        if (i == x)
                        {
                            img[i].SetActive(true);
                        }
                        else
                        {
                            img[i].SetActive(false);
                        }
                    }
                    for (int i = 0; i < description.Length; i++)
                    {
                        if (i == x)
                        {
                            description[i].SetActive(true);
                        }
                        else
                        {
                            description[i].SetActive(false);
                        }
                    }
                    podeDpad = false;
                }

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("DpadYP1") > 0)
                {
                    if (x > 0)
                        x--;

                    for (int i = 0; i < img.Length; i++)
                    {
                        if (i == x)
                        {
                            img[i].SetActive(true);
                        }
                        else
                        {
                            img[i].SetActive(false);
                        }
                    }
                    for (int i = 0; i < description.Length; i++)
                    {
                        if (i == x)
                        {
                            description[i].SetActive(true);
                        }
                        else
                        {
                            description[i].SetActive(false);
                        }
                    }
                    podeDpad = false;
                }
            }
        }
        else if(SelectPersonagem.personagem.select2 == meuNumero.meuNumero)
        {
            if (podeDpad2)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("DpadYP2") < 0)
                {
                    if (x < (img.Length - 1))
                        x++;

                    for (int i = 0; i < img.Length; i++)
                    {
                        if (i == x)
                        {
                            img[i].SetActive(true);
                        }
                        else
                        {
                            img[i].SetActive(false);
                        }
                    }
                    for (int i = 0; i < description.Length; i++)
                    {
                        if (i == x)
                        {
                            description[i].SetActive(true);
                        }
                        else
                        {
                            description[i].SetActive(false);
                        }
                    }
                    podeDpad2 = false;
                }

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("DpadYP2") > 0)
                {
                    if (x > 0)
                        x--;

                    for (int i = 0; i < img.Length; i++)
                    {
                        if (i == x)
                        {
                            img[i].SetActive(true);
                        }
                        else
                        {
                            img[i].SetActive(false);
                        }
                    }
                    for (int i = 0; i < description.Length; i++)
                    {
                        if (i == x)
                        {
                            description[i].SetActive(true);
                        }
                        else
                        {
                            description[i].SetActive(false);
                        }
                    }
                    podeDpad2 = false;
                }
            }
        }

        if (Input.GetAxisRaw("DpadYP1") == 0)
        {
            podeDpad = true;
        }
        if (Input.GetAxisRaw("DpadYP2") == 0)
        {
            podeDpad2 = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            x = 0;
        }
    }
}
