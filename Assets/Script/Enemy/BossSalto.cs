using UnityEngine;
using System.Collections;

public class BossSalto : MonoBehaviour
{
    public float dmg;

    public Animator anim;

    public GameObject garra;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            garra.SetActive(true);
            anim.SetTrigger("SaltoAcerto");
            other.gameObject.GetComponent<PlayerDano>().Dano(dmg, gameObject);
        }
    }
}
