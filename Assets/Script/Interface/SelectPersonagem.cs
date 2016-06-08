using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectPersonagem : MonoBehaviour
{
    public static SelectPersonagem personagem;

    public MusicaManager musica;

    public Animator[] player;

    public GameObject[] atributos;

    [HideInInspector]
    public int select;
    [HideInInspector]
    public int select2 = 1;

    bool podeP1 = true;
    bool podeP2 = true;
    bool podeDpad = true;
    bool podeDpad2 = true;
    bool startP1 = false;
    bool startP2 = false;

    void Awake()
    {
        personagem = this;
    }

    void Start()
    {
        if(PlayerPrefs.GetInt("Players") == 1)
        {
            player[0].enabled = true;
            player[1].enabled = false;
            select = 0;
            select2 = 999;
        }
        else if (PlayerPrefs.GetInt("Players") == 2)
        {
            player[0].enabled = true;
            player[1].enabled = true;
            select2 = 1;
        }
    }

    void Update()
    {
        if(PlayerPrefs.GetInt("Players") == 2)
        {
            if(startP1 && startP2)
            {
                musica.Mudo();
                if (select == 0)
                {
                    PlayerPrefs.SetInt("Escolha", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("Escolha", 2);
                }
                SceneManager.LoadScene("Jogo");
            }
        }
        else if(startP1)
        {
            musica.Mudo();
            if (select == 0)
            {
                PlayerPrefs.SetInt("Escolha", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Escolha", 2);
            }
            SceneManager.LoadScene("Jogo");
        }

        if(Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            startP1 = true;
        }
        
        if(Input.GetKeyDown(KeyCode.Joystick2Button7))
        {
            startP2 = true;
        }

        if (!startP1)
        {
            if (podeP1)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("DpadXP1") > 0)
                {
                    Muda();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("DpadXP1") < 0)
                {
                    Muda();
                }
                else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    Select();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                atributos[select].SetActive(false);
                podeP1 = true;
            }
        }

        if (!startP2)
        {
            if (PlayerPrefs.GetInt("Players") == 2)
            {
                if (podeP2)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("DpadXP2") > 0)
                    {
                        Muda2();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("DpadXP2") < 0)
                    {
                        Muda2();
                    }
                    else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Joystick2Button0))
                    {
                        Select2();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick2Button1))
                {
                    atributos[select2].SetActive(false);
                    podeP2 = true;
                }
            }
        }

        if(Input.GetAxisRaw("DpadXP1") == 0)
        {
            podeDpad = true;
        }

        if (Input.GetAxisRaw("DpadXP2") == 0)
        {
            podeDpad2 = true;
        }
    }

    void Select2()
    {
        if (select2 == 0)
        {
            atributos[select2].SetActive(true);
            podeP2 = false;
            return;
        }
        else if (select2 == 1)
        {
            atributos[select2].SetActive(true);
            podeP2 = false;
            return;
        }
    }

    void Select()
    {
        if (select == 0)
        {
            atributos[select].SetActive(true);
            podeP1 = false;
            return;
        }
        else if (select == 1)
        {
            atributos[select].SetActive(true);
            podeP1 = false;
            return;
        }
    }

    void Muda2()
    {
        if (podeDpad2)
        {
            if (select2 == 0 && select != 1)
            {
                player[1].enabled = true;
                player[0].enabled = false;
                select2 = 1;
            }
            else if (select2 == 1 && select != 0)
            {
                player[0].enabled = true;
                player[1].enabled = false;
                select2 = 0;
            }
            podeDpad2 = false;
        }
    }

    void Muda()
    {
        if (podeDpad)
        {
            if (select == 0 && select2 != 1)
            {
                player[1].enabled = true;
                player[0].enabled = false;
                select = 1;
            }
            else if (select == 1 && select2 != 0)
            {
                player[0].enabled = true;
                player[1].enabled = false;
                select = 0;
            }
            podeDpad = false;
        }
    }
}