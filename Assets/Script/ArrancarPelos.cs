using UnityEngine;
using System.Collections;

public class ArrancarPelos : MonoBehaviour
{
    public PlayerController player;

    float x;

    int selecionado;

    void Update()
    {
        switch(player.player)
        {
            case Player.Player1:
                x = Input.GetAxis("HorizontalP1");
                if(x < -0.1f)
                {
                    Anterior();
                }
                else if(x > 0.1f)
                {
                    Proximo();
                }
                break;
        }
    }

    void Proximo()
    {
        if (selecionado <= Manager.manager.enemy.Length)
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
                Manager.manager.enemy[i].GetComponent<EnemyController>().seta.SetActive(true);
            }
            else
            {
                Manager.manager.enemy[i].GetComponent<EnemyController>().seta.SetActive(false);
            }
        }
    }

    void Anterior()
    {
        if (selecionado >= 0)
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
                Manager.manager.enemy[i].GetComponent<EnemyController>().seta.SetActive(true);
            }
            else
            {
                Manager.manager.enemy[i].GetComponent<EnemyController>().seta.SetActive(false);
            }
        }
    }
}