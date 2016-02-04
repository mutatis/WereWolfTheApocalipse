using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Animator anim;

    public void Dano()
    {
        if((PlayerController.playerController.transform.localScale.x > 0 && transform.localScale.x < 0) || (PlayerController.playerController.transform.localScale.x < 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
        }
        anim.SetTrigger("Dano");
    }

    public void Slam()
    {
        anim.SetTrigger("Slam");
    }
}