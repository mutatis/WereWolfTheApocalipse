using UnityEngine;
using System.Collections;

public class EnemyAnim : MonoBehaviour
{
    public EnemyController controller;

    public EnemyRanged controller2;

    public GameObject obj;

    public int tipo;

    public float dmg;

    [HideInInspector]
    public string nome;

    public void Return()
    {
        if (tipo == 1)
        {
            controller2.stun = false;
        }
        else
        {
            controller.stun = false;
        }
    }

    public void DanoAgain()
    {
        if (tipo == 1)
        {
            controller2.DanoAgain();
        }
        else
        {
            controller.DanoAgain();
        }
    }

    public void Dead()
    {
        FollowTarget.follow.quant--;
        if (tipo == 1)
        {
            PlayerPrefs.SetInt(nome + "XP", (PlayerPrefs.GetInt(nome + "XP") + controller2.xp));
        }
        else
        {
            PlayerPrefs.SetInt(nome + "XP", (PlayerPrefs.GetInt(nome + "XP") + controller.xp));
        }
        Destroy(obj);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Dano(dmg);         
        }
        if (other.gameObject.tag == "Parede")
        {
            if (tipo == 1)
            {
                if (controller2.roamming)
                {
                    controller2.Wait();
                }
            }
            else
            {
                if (controller.roamming)
                {
                    controller.Wait();
                }
            }
        }
    }
}