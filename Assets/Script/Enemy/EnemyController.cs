using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Animator anim;

    public ProbabilidadeEnemy probabilidade;

    public bool stun;

    public string[] attack;

    bool isWalk = true;
    bool isAttack = false;

    Vector3 direction;

    float dist;

    void Update()
    {
        dist = Vector3.Distance(PlayerController.playerController.transform.position, transform.position);

        if (dist > 2f && isWalk)
        {
            Engage(); //Idle ou Roaming tb
        }
        else if(isWalk)
        {
            Combate();
            StopCoroutine("GO");
            StartCoroutine("GO");
            isWalk = false;
        }
    }

    public void Combate()
    {
        StopCoroutine("Pode");
        StartCoroutine("Pode");
        int num;
        if(!stun)
        {
            num = probabilidade.ChooseAttack();

            switch(num)
            {
                case 0:
                    Soco();
                    break;

                case 1:
                    Defesa();
                    break;

                case 2:
                    SpecialMove();
                    break;

                default:
                    Soco();
                    break;
            }
        }
    }

    IEnumerator Pode()
    {
        yield return new WaitForSeconds(2);
        Combate();
    }

    void SpecialMove()
    {
        anim.SetTrigger("SocoFraco2");
        print("SpecialMove");
    }

    void Defesa()
    {
        anim.SetTrigger("SocoForte");
        print("Defesa");
    }

    void Soco()
    {
        anim.SetTrigger("SocoFraco0");
        print("Soco");
    }

    public void Engage()
    {
        while (dist > 2f)
        {
            if (isWalk)
            {
                direction = PlayerController.playerController.transform.position - transform.position;
                direction.Normalize();
                transform.Translate(direction / 8);
                if (direction.x > 0 && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x < 0 && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
                }
            }
            dist = Vector3.Distance(PlayerController.playerController.transform.position, transform.position);
        }
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(5);
        isWalk = true;
    }

    public void Dano()
    {
        stun = true;
        isWalk = false;
        StopCoroutine("Pode");
        StopCoroutine("GO");
        StartCoroutine("GO");
        if((PlayerController.playerController.transform.localScale.x > 0 && transform.localScale.x < 0) || (PlayerController.playerController.transform.localScale.x < 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
        }
        anim.SetTrigger("Dano");
    }

    public void Slam()
    {
        StopCoroutine("Pode");
        stun = true;
        anim.SetTrigger("Slam");
    }
}