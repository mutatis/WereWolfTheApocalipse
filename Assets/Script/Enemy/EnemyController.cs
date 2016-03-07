using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Animator anim;

    public ProbabilidadeEnemy probabilidade;

    public bool stun;

    public float tempoResposta;

    public string[] attack;

    public GameObject player;

    bool isWalk = true;
    bool isAttack = false;
    bool procura = true;

    Vector3 direction;

    float dist;
    
    void Update()
    {
        dist = Vector3.Distance(PlayerController.playerController.transform.position, transform.position);

        if (dist > 2f && isWalk && player != null)
        {
            /*direction = PlayerController.playerController.transform.position - transform.position;
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
            //Engage(); //Idle ou Roaming tb*/
            StartCoroutine("Engage");
        }
        else if(isWalk && player != null)
        {
            player.GetComponent<PlayerController>().engage--;
            player = null;
            StopCoroutine("Pode");
            StartCoroutine("Pode");
            StopCoroutine("GO");
            StartCoroutine("GO");
            isWalk = false;
        }

        if(player == null && procura)
        {
            var x = Random.Range(0, Manager.manager.player.Length);
            if(Manager.manager.player[x].GetComponent<PlayerController>().engage < 2)
            {
                player = Manager.manager.player[x];
                player.GetComponent<PlayerController>().engage++;
            }
            else
            {
                StopCoroutine("Procura");
                StartCoroutine("Procura");
            }
        }
    }

    IEnumerator Procura()
    {
        procura = false;
        yield return new WaitForSeconds(1);
        procura = true;
    }

    public void Combate()
    {
        StopCoroutine("Pode");
        StartCoroutine("Pode");
        int num;
        if(!stun && dist < 2)
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

                case 3:
                    Wait();
                    break;

                case 4:
                    Flank();
                    break;

                case 5:
                    Flee();
                    break;

                case 6:
                    Switch();
                    break;

                default:
                    Soco();
                    break;
            }
        }
    }

    IEnumerator Pode()
    {
        yield return new WaitForSeconds(tempoResposta);
        Combate();
    }

    void Switch()
    {
        //escolhe outro player
        player = null;
        procura = true;
    }

    void Flee()
    {
        //sai do range de ataque
    }

    void Flank()
    {
        //fica se movendo perto do player
    }

    void Wait()
    {
        //chama a animacao de idle e deixar ele parado perto do player
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

    IEnumerator Engage()
    {
        while (dist > 2f)
        {
            if (isWalk)
            {
                direction = PlayerController.playerController.transform.position - transform.position;
                direction.Normalize();
                transform.Translate(direction / 80);
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
            yield return new WaitForEndOfFrame();
        }
        StopCoroutine("Engage");
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