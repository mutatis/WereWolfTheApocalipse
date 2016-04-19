using UnityEngine;
using System.Collections;

public class EnemyAnim : MonoBehaviour
{
    public EnemyController controller;

    public GameObject obj;

    public float dmg;

    public void Return()
    {
        controller.stun = false;
    }

    public void DanoAgain()
    {
        controller.DanoAgain();
    }

    public void Dead()
    {
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
            if(controller.roamming)
            {
                controller.Wait();
            }
        }
    }
}