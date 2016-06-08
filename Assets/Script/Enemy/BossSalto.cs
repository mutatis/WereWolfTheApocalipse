using UnityEngine;
using System.Collections;

public class BossSalto : MonoBehaviour
{
    public float dmg;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Dano(dmg, gameObject);
        }
    }
}
