using UnityEngine;
using System.Collections;

public class ArrancarPelos : MonoBehaviour
{
    public PlayerController player;

    float x;

    int selecionado;

    bool zero;

    void Update()
    {
        switch(player.player)
        {
            case Player.Player1:
                x = Input.GetAxis("HorizontalP1");
                if (zero)
                {
                    if (x < -0.1f)
                    {
                        Anterior();
                    }
                    else if (x > 0.1f)
                    {
                        Proximo();
                    }
                }

                if(x == 0)
                {
                    zero = true;
                }

                if(Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    Manager.manager.enemy[selecionado].GetComponent<EnemyController>().Dano(500, false, gameObject);
                    for (int i = 0; i < Manager.manager.enemy.Length; i++)
                    {
                        Manager.manager.enemy[i].GetComponent<EnemyController>().enabled = true;
                    }
                    Destroy(gameObject.GetComponent<ArrancarPelos>());
                }
                break;
        }
    }

    void Proximo()
    {
        zero = false;
        if (selecionado < Manager.manager.enemy.Length)
        {
            selecionado++;
        }
        else
        {
            selecionado = 0;
        }
        for(int i = 0; i < Manager.manager.enemy.Length; i++)
        {
            if(i == selecionado)
            {
                Manager.manager.enemy[selecionado].GetComponent<EnemyController>().seta.SetActive(true);
            }
            else
            {
                Manager.manager.enemy[i].GetComponent<EnemyController>().seta.SetActive(false);
            }
        }
    }

    void Anterior()
    {
        zero = false;
        if (selecionado > 0)
        {
            selecionado--;
        }
        else
        {
            selecionado = Manager.manager.enemy.Length;
        }
        for (int i = 0; i < Manager.manager.enemy.Length; i++)
        {
            if (i == selecionado)
            {
                Manager.manager.enemy[selecionado].GetComponent<EnemyController>().seta.SetActive(true);
            }
            else
            {
                Manager.manager.enemy[i].GetComponent<EnemyController>().seta.SetActive(false);
            }
        }
    }
}