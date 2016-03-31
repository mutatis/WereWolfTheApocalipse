using UnityEngine;
using System.Collections;

public class EnemyAnim : MonoBehaviour
{
    public EnemyController controller;

    public GameObject obj;

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
            other.gameObject.GetComponent<PlayerController>().Dano(1);
        }
    }
}