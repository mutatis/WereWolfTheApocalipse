using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;

    bool run;

	void Update ()
    {
	    if((PlayerController.playerController.x != 0 || PlayerController.playerController.z != 0) && !PlayerController.playerController.jump)
        {
            if (run)
            {
                anim.SetTrigger("Run");
                run = false;
            }
        }
        else if(!run)
        {
            anim.SetTrigger("Idle");
            run = true;
        }
	}

    public void Liberated()
    {
        PlayerController.playerController.Liberated();
    }

    public void Ataca()
    {
        PlayerController.playerController.Ataca();
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (PlayerController.playerController.contador <= 2)
            {
                other.gameObject.GetComponent<EnemyController>().Dano();
            }
            else if (PlayerController.playerController.contador >= 3)
            {
                other.gameObject.GetComponent<EnemyController>().Slam();
            }
        }
    }
}