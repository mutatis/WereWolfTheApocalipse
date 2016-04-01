﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Animator anim;

    public ProbabilidadeEnemy probabilidade;

    public bool stun;
    [HideInInspector]
    public bool dano = true;

    public TextMesh text;

    public float tempoResposta;
    public float life;

    public string[] attack;

    public GameObject player;

    bool isWalk = true;
    bool procura = true;

    Vector3 direction;

    float dist;
    
    void Update()
    {
        if (player != null)
        {
            dist = Vector3.Distance(player.transform.position, transform.position);
        }

        if(life <= 0)
        {
            anim.SetTrigger("Dead");
            if (player != null)
            {
                player.GetComponent<PlayerController>().engage--;
            }
            dano = false;
            gameObject.GetComponent<EnemyController>().enabled = false;
        }

        if (dist > 2f && isWalk && player != null)
        {
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
        player.GetComponent<PlayerController>().engage--;
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
    }

    void Defesa()
    {
        anim.SetTrigger("SocoForte");
    }

    void Soco()
    {
        anim.SetTrigger("SocoFraco0");
    }

    IEnumerator Engage()
    {
        while (dist > 2f)
        {
            if (isWalk)
            {
                direction = player.transform.position - transform.position;
                direction.Normalize();
                transform.Translate((direction / 80) * Time.deltaTime);
                if (direction.x > 0 && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x < 0 && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
                }
            }
            dist = Vector3.Distance(player.transform.position, transform.position);
            yield return new WaitForEndOfFrame();
        }
        StopCoroutine("Engage");
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(5);
        isWalk = true;
    }


    public void DanoAgain()
    {
        dano = true;
        text.text = "";
    }

    public void Dano(float dmg, bool crit)
    {
        if (dano)
        {
            life -= dmg;
            if(crit == true)
            {
                text.color = Color.red;
                text.text = dmg.ToString() + " CRIT";
            }
            else
            {
                text.color = Color.white;
                text.text = dmg.ToString();
            }
            stun = true;
            isWalk = false;
            StopCoroutine("Pode");
            StopCoroutine("GO");
            StartCoroutine("GO");
            if ((player.transform.localScale.x > 0 && transform.localScale.x < 0) || (player.transform.localScale.x < 0 && transform.localScale.x > 0))
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
            anim.SetTrigger("Dano");
            dano = false;
        }
    }

    public void Slam(float dmg, bool crit)
    {
        dano = false;
        life -= dmg;
        if (crit == true)
        {
            text.color = Color.red;
            text.text = dmg.ToString() + " CRIT";
        }
        else
        {
            text.color = Color.white;
            text.text = dmg.ToString();
        }
        text.text = dmg.ToString();
        StopCoroutine("Pode");
        stun = true;
        anim.SetTrigger("Slam");
    }
}