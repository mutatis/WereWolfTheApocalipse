using UnityEngine;
using System.Collections;

public class BossSalto : MonoBehaviour
{
    public float dmg;

    void OnTriggerEnter(Collider other)
    {
        print("BATEUUUUU");
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Dano(dmg);
        }
    }
}
