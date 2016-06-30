using UnityEngine;
using System.Collections;

public class EnemyAnim : MonoBehaviour
{
    public EnemyController controller;

    public EnemyRanged controller2;

    public SubBossController subBoss;

    public BossController boss;

    public GameObject obj;

    public int tipo;

    public float dmg;

    [HideInInspector]
    public string nome;

    public void Liberated()
    {
        controller.combate = true;
    }

    public void Tiro()
    {
        controller2.Atira();
    }

    public void Return()
    {
        if (tipo == 1)
        {
            controller2.stun = false;
        }
        if (tipo == 2)
        {
            subBoss.stun = false;
        }
        if(tipo == 3)
        {
            boss.stun = false;
        }
        else
        {
            controller.stun = false;
            controller.slam = false;
            controller.anim.SetBool("isSlam", false);
            controller.transform.position = new Vector3(transform.position.x, controller.transform.position.y, controller.transform.position.z);
        }
    }

    public void DanoAgain()
    {
        if (tipo == 1)
        {
            controller2.DanoAgain();
        }
        if(tipo == 2)
        {
            subBoss.DanoAgain();
        }
        if(tipo == 3)
        {
            boss.DanoAgain();
        }
        else
        {
            controller.DanoAgain();
        }
    }

    public void Dead()
    {
        FollowTarget.follow.quant--;
        if (tipo == 0)
        {
            PlayerPrefs.SetInt(nome + "XP", (PlayerPrefs.GetInt(nome + "XP") + controller.xp));
        }
        if (tipo == 1)
        {
            PlayerPrefs.SetInt(nome + "XP", (PlayerPrefs.GetInt(nome + "XP") + controller2.xp));
        }
        if(tipo == 2)
        {
            PlayerPrefs.SetInt(nome + "XP", (PlayerPrefs.GetInt(nome + "XP") + subBoss.xp));
        }
        if (tipo == 3)
        {
            PlayerPrefs.SetInt(nome + "XP", (PlayerPrefs.GetInt(nome + "XP") + boss.xp));
        }
        Destroy(obj);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerDano>().Dano(dmg, gameObject);         
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
                    controller.StartCoroutine("Wait");
                }
            }
        }
    }
}