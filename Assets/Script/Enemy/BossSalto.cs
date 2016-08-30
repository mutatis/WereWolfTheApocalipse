using UnityEngine;
using System.Collections;

public class BossSalto : MonoBehaviour
{
    public float dmg;
    public Animator anim;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("SaltoAcerto");
            other.gameObject.GetComponent<PlayerDano>().Dano(dmg, gameObject);
        }
    }
}
