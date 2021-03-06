﻿using UnityEngine;
using System.Collections;

public class EnemyRanged : MonoBehaviour
{
    public Animator anim;

    public Rigidbody rig;

    public ProbabilidadeEnemy probabilidade;

    public EnemyAnim enemyanim;

    public bool stun;
    [HideInInspector]
    public bool dano = true;
    [HideInInspector]
    public bool roamming = false;
    public bool perto = false;

    public TextMesh text;

    public float tempoResposta;
    public float life;
    public float vel1, vel2;

    public int xp;

    public string[] attack;

    public GameObject player, tiro;

    public int marcado;

    bool isWalk = true;
    bool procura = true;
    bool prepare = true;

    Vector3 direction;

    void Start()
    {
        Wait();
        StartCoroutine("Pode");
        //Combate();
    }

    void Update()
    {
        if (!perto)
        {
            if (marcado == 0)
            {
                transform.Translate(vel1, 0, vel2);
            }

            if (marcado == 1)
            {
                transform.Translate(new Vector3(0.001f, 0, -0.1f));
            }
            else if (marcado == 2)
            {
                transform.Translate(new Vector3(0.001f, 0, 0.1f));
            }
        }

        if (life <= 0)
        {
            anim.SetTrigger("Dead");
            if (player != null)
            {
                enemyanim.nome = player.GetComponent<PlayerController>().nome;
                player.GetComponent<PlayerController>().engage--;
            }
            dano = false;
            gameObject.GetComponent<EnemyRanged>().enabled = false;
        }

        if (player == null && procura)
        {
            var x = Random.Range(0, Manager.manager.player.Length);
                player = Manager.manager.player[x];
                player.GetComponent<PlayerController>().engage++;
           
        }
    }

    IEnumerator Procura()
    {
        procura = false;
        yield return new WaitForSeconds(1);
        procura = true;
    }
    public void Prepare()
    {
        if (prepare)
        {
            Wait();
            StopCoroutine("Foi");
            StartCoroutine("Foi");
            prepare = false;
        }
    }

    IEnumerator Foi()
    {
        if (vel1 != 0)
        {
            anim.SetTrigger("Run");
        }
        if (vel1 > 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
        }
        else if (vel1 < 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
        }
        yield return new WaitForSeconds(1);
        prepare = true;
    }

    public void Combate()
    {
        StopCoroutine("Pode");
        StartCoroutine("Pode");
        roamming = false;
        int num;
        if (!stun)
        {
            var temp = player.GetComponent<PlayerController>().transform.position;
            if (temp.x > transform.position.x && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
            if (temp.x < transform.position.x && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
            num = probabilidade.ChooseAttack();

            if (!perto)
            {
                roamming = false;
                StartCoroutine("Engage");
            }
            else
            {
                switch (num)
                {
                    case 0:
                        Soco();
                        break;

                    case 1:
                        Defesa();
                        break;

                    case 2:
                        Engage();
                        break;

                    case 3:
                        Wait();
                        break;

                    case 4:
                        Wait();
                        break;

                    case 5:
                        Wait();
                        break;

                    case 6:
                        Wait();
                        break;

                    default:
                        Soco();
                        break;
                }
            }
        }
    }

    IEnumerator Pode()
    {
        var tempo = tempoResposta;
        if(perto)
        {
            tempo *= 0.5f;
        }
        yield return new WaitForSeconds(tempo);
        Combate();
    }

    void Switch()
    {
        //escolhe outro player
        roamming = false;
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

    public void Wait()
    {
        roamming = true;
        vel1 = 0.05f * Random.Range(-2, 2);
        vel2 = 0.05f * Random.Range(-1, 2);
        //chama a animacao de idle e deixar ele parado perto do player
    }

    void SpecialMove()
    {
        roamming = false;
        anim.SetTrigger("SocoFraco2");
    }

    void Defesa()
    {
        roamming = false;
        anim.SetTrigger("SocoForte");
    }

    void Soco()
    {
        roamming = false;
        anim.SetTrigger("SocoFraco0");
    }

    IEnumerator Engage()
    {
        roamming = false;
        float x = (transform.position.z - player.transform.position.z);
        if(x < 0)
        {
            x *= -1;
        }
        while (x > 1)
        {
                if (transform.position.z > player.transform.position.z)
                {
                    marcado = 1;
                }
                else if (transform.position.z < player.transform.position.z)
                {
                    marcado = 2;
                }
            
            x = (transform.position.z - player.transform.position.z);
            if (x < 0)
            {
                x *= -1;
            }
            yield return new WaitForEndOfFrame();
        }
        Attack();
        Wait();
        marcado = 0;
        StopCoroutine("Engage");
    }

    void Attack()
    {
        anim.SetTrigger("Tiro");
    }

    public void Atira()
    {
        GameObject obj = Instantiate(tiro);
        obj.GetComponent<TiroEnemy>().transform.position = transform.position;
        obj.GetComponent<TiroEnemy>().obj = player;
        StopCoroutine("Pode");
        StartCoroutine("Volta");
        roamming = true;
    }

    IEnumerator Volta()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine("Pode");
        StopCoroutine("Volta");
    }

    IEnumerator GO()
    {
        roamming = false;
        yield return new WaitForSeconds(5);
        isWalk = true;
    }


    public void DanoAgain()
    {
        roamming = false;
        dano = true;
        text.text = "";
    }

    public void Dano(float dmg, bool crit, GameObject obj)
    {
        roamming = false;
        if (dano)
        {
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
            if (player == null)
            {
                player = obj;
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

    public void Slam(float dmg, bool crit, GameObject obj, float knockback)
    {
        roamming = false;
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
        if (transform.localScale.x < 0)
        {
            rig.velocity = new Vector2((knockback * (-1)), knockback);
        }
        else if (transform.localScale.x > 0)
        {
            rig.velocity = new Vector2(knockback, knockback);
        }
        if (player == null)
        {
            player = obj;
        }
        text.text = dmg.ToString();
        StopCoroutine("Pode");
        stun = true;
        anim.SetTrigger("Slam");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Parede1")
        {
            vel1 = 0.05f * Random.Range(-2, -0.2f);
            vel2 = 0.05f * Random.Range(-1, 2);
        }
        if (other.gameObject.tag == "Parede2")
        {
            print("P2");
            vel1 = 0.05f * Random.Range(0.2f, 2);
            vel2 = 0.05f * Random.Range(-1, 2);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Parede1")
        {
            vel1 = 0.05f * Random.Range(-2, -0.2f);
            vel2 = 0.05f * Random.Range(-1, 2);
        }
        if (other.gameObject.tag == "Parede2")
        {
            vel1 = 0.05f * Random.Range(0.2f, 2);
            vel2 = 0.05f * Random.Range(-1, 2);
        }
    }
}