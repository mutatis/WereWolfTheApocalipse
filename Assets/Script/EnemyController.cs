using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Animator anim;

    public void Dano()
    {
        anim.SetTrigger("Dano");
    }

    public void Slam()
    {
        anim.SetTrigger("Slam");
    }
}
