﻿    using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Animator anim;

    public Rigidbody rig;

    public ProbabilidadeEnemy probabilidade;

    public EnemyAnim enemyanim;

    public bool stun;
    [HideInInspector]
    public bool dano = true;
    public bool roamming = true;
    public bool combate = true;
    [HideInInspector]
    public bool slam;

    public TextMesh text;

    public float tempoResposta;
    public float life;
    public float vel1, vel2;

    public int xp;

    public string[] attack;

    [HideInInspector]
    public GameObject peguei;
    public GameObject player, seta;

    public GameObject[] obj;

    bool isWalk = true;
    bool procura = false;
    bool prepare = true;
    bool taPego;
    bool chamei;
    bool isIdle = true;
    bool isRun = true;
    bool block;
    bool isEngage;
    bool costas = false;
    bool autorizo;

    Vector3 direction;

    public float dist;
    float dist1;
    float dist2;

    void Start()
    {
        obj = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine("Procura");
    }
    
    void Update()
    {
        anim.SetBool("isWalk", isWalk);

        if(taPego && player.GetComponent<PlayerEngage>().playercontroller.solta >= 10)
        {
            Solta();
        }

        if (peguei ==  null) 
		{
			if (life <= 0) 
			{
				StopAllCoroutines ();
				anim.SetTrigger ("Dead");
				if (player != null) 
				{
					player.GetComponent<PlayerEngage> ().engage--;
					enemyanim.nome = player.GetComponent<PlayerEngage> ().nome;
				}
				dano = false;
				gameObject.GetComponent<EnemyController> ().enabled = false;
			}

            if (!stun)
            {
                if (Manager.manager.player[0].GetComponent<PlayerController>().transform.position.x > transform.position.x && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
                else if (Manager.manager.player[0].GetComponent<PlayerController>().transform.position.x < transform.position.x && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
            }

            /*if (!isWalk && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdle") && !isEngage)
            {
                anim.SetTrigger("Idle");
            }*/

            if ((roamming || player == null) && !stun) 
			{
				transform.Translate (vel1, 0, vel2);
                if(vel1 == 0 && vel2 == 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdle"))
                {
                    anim.SetTrigger("Idle");
                }
                else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRun") && !costas)
                {
                    anim.SetTrigger("Run");
                }
                else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRunCostas") && costas)
                {
                    anim.SetTrigger("Run");
                }

                if ((vel1 > 0 && transform.localScale.x > 0) || (vel1 < 0 && transform.localScale.x < 0))
                {
                    costas = true;
                    anim.SetBool("Costas", true);
                }
                else
                {
                    costas = false;
                    anim.SetBool("Costas", false);
                }
			}

			if (player != null) 
			{
				dist = Vector3.Distance (player.transform.position, transform.position);
			}

			if (dist > 1f && player != null && !isEngage)
            {
                isWalk = true;
                StartCoroutine (Engage ());
			} 
			else if (player != null && dist < 1 && !chamei) 
			{
				StartCoroutine ("Pode");
				chamei = true;
			} 
			else if (isWalk && player != null && dist > 2) 
			{
				player.GetComponent<PlayerEngage> ().engage--;
				player = null;
				StopCoroutine ("Pode");
				StartCoroutine ("Pode");
				isWalk = false;
			}
			else if (player == null) 
			{
				Prepare ();
			}

			if (player == null && procura) 
			{
				var x = Random.Range (0, Manager.manager.playerEngage.Length);
				if (Manager.manager.playerEngage [x].GetComponent<PlayerEngage> ().engage < 1) 
				{
					player = Manager.manager.playerEngage [x];
					player.GetComponent<PlayerEngage> ().engage++;
				}
			}
		}	
        else
        {
            if(peguei.GetComponent<Agarra>().player.transform.localScale.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (peguei.GetComponent<Agarra>().player.transform.localScale.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            //player = null;
            StopAllCoroutines();
            transform.position = peguei.transform.position;
        }	
    }

    /*IEnumerator Esquece()
    {
        yield return new WaitForSeconds(10);
        Switch();
    }*/

    IEnumerator Procura()
    {
        procura = false;
        var tempo = Random.Range(2f, 3f);
        yield return new WaitForSeconds(tempo);
        var ran = Random.value;
        if (ran > 0.3f)
        {
            procura = true;
        }
        else
        {
            roamming = true;
            if(player != null)
                player.GetComponent<PlayerEngage>().engage--;
            player = null;
            Denovo();
        }
    }

    void Denovo()
    {
        StartCoroutine("Procura");
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

    public void Para()
    {
        StopAllCoroutines();
    }

    IEnumerator Foi()
    {
        if (vel1 != 0)
        {
            anim.SetTrigger("Run");
        }
        /*if (vel1 > 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
        }
        else if (vel1 < 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
        }*/
        yield return new WaitForSeconds(1);
        prepare = true;
    }

    public void Combate()
    {
        StopCoroutine("Procura");
        StopCoroutine("Pode");
        StartCoroutine("Pode");
        roamming = false;
        int num;
        if(!stun && dist < 0.54f && combate && !player.GetComponent<PlayerEngage>().playercontroller.anim.levanta)
        {
            var temp = player.GetComponent<PlayerEngage>().playercontroller.transform.position;
            /*if (temp.x > transform.position.x && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
            if (temp.x < transform.position.x && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }*/
            num = probabilidade.ChooseAttack();

            switch (num)
            {
                case 0:
                    Soco();
                    break;

                case 1:
                    StartCoroutine(Defesa());
                    break;

                case 2:
                    SpecialMove();
                    break;

                case 3:
                    Soco2();
                    break;

                case 4:
                    Grab();
                    break;

                default:
                    Soco();
                    break;
            }

            print(num);
        }
        chamei = false;
    }

    IEnumerator Pode()
    {
        yield return new WaitForSeconds(tempoResposta);
        Combate();
    }

    void Grab()
    {
        if((player.GetComponent<PlayerEngage>().playercontroller.transform.position.x > transform.position.x && player.GetComponent<PlayerEngage>().playercontroller.transform.localScale.x > 0) || (player.GetComponent<PlayerEngage>().playercontroller.transform.position.x < transform.position.x && player.GetComponent<PlayerEngage>().playercontroller.transform.localScale.x < 0))
        {
            player.GetComponent<PlayerEngage>().playercontroller.anim.gameObject.SetActive(false);
            player.GetComponent<PlayerEngage>().playercontroller.stun = true;
            player.GetComponent<PlayerEngage>().playercontroller.pegador = gameObject;
            player.GetComponent<PlayerEngage>().playercontroller.presa = true;
            anim.SetBool("isGrab", true);
            anim.SetTrigger("Grab");
            roamming = false;
            combate = false;
            taPego = true;
        }
    }

    void Solta()
    {
        anim.SetBool("isGrab", false);
        anim.SetTrigger("Idle");
        player.GetComponent<PlayerEngage>().playercontroller.anim.gameObject.SetActive(true);
        player.GetComponent<PlayerEngage>().playercontroller.stun = false;
        player.GetComponent<PlayerEngage>().playercontroller.solta = 0;
        player.GetComponent<PlayerEngage>().playercontroller.pegador = null;
        player.GetComponent<PlayerEngage>().playercontroller.presa = false;
        anim.SetBool("isGrab", false);
        anim.SetTrigger("Idle");
        combate = true;
        taPego = false;
    }

    public void Switch()
    {
        //escolhe outro player
        StopCoroutine("Engage");
        isEngage = false;
        roamming = true;
        if (player.GetComponent<PlayerEngage>().engage > 0)
        {
            player.GetComponent<PlayerEngage>().engage--;
        }
        player = null;
        procura = true;
    }

    public void Wait()
    {
        roamming = true;
        vel1 = 0.05f * Random.Range(-2, 2);
        vel2 = 0.05f * Random.Range(-1, 2);
    }

    void SpecialMove()
    {
        roamming = false;
        combate = false;
        anim.SetTrigger("SocoFraco0");
    }

    IEnumerator Defesa()
    {
        if (!autorizo)
        {
            roamming = false;
            combate = false;
            anim.SetTrigger("Block");
            block = true;
            yield return new WaitForSeconds(2f);
            combate = true;
            block = false;
            anim.SetTrigger("Idle");
        }
    }

    void Soco2()
    {
        roamming = false;
        combate = false;
        anim.SetTrigger("SocoFraco1");
    }

    void Soco()
    {
        roamming = false;
        combate = false;
        anim.SetTrigger("SocoFraco0");
    }

    IEnumerator Engage()
    {
        isEngage = true;
        if (isIdle)
        {
            anim.SetTrigger("Idle");
            isIdle = false;
        }
        StopCoroutine("Procura");
        roamming = false;
        yield return new WaitForSeconds(1);
        if (isRun)
        {
            anim.SetTrigger("Run");
            isRun = false;
        }
        while (dist > 0.5f)
        {
            direction = player.transform.position - transform.position;
            direction.Normalize();
            if (!stun)
            {
                Vector3 deus = (direction / 25);
                transform.Translate(deus);
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRun") && !costas)
                {
                    anim.SetTrigger("Run");
                }
                else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRunCostas") && costas)
                {
                    anim.SetTrigger("Run");
                }

                if ((direction.x > 0 && transform.localScale.x < 0) || (direction.x < 0 && transform.localScale.x > 0))
                {
                    anim.SetBool("Costas", false);
                    costas = false;
                }
                else
                {
                    costas = true;
                    anim.SetBool("Costas", true);
                }
            }
            dist = Vector3.Distance(player.transform.position, transform.position);
            yield return new WaitForSeconds(0.01f);
        }
        combate = true;
        isIdle = true;
        isRun = true;
        isEngage = false;
        anim.SetTrigger("Idle");
        isWalk = false;
        StopCoroutine("Pode");
        StartCoroutine("Pode");
    }


    public void DanoAgain()
    {
        stun = false;
        roamming = false;
        dano = true;
        chamei = false;
        text.text = "";
    }

    IEnumerator Autorizacao()
    {
        yield return new WaitForSeconds(1);
        autorizo = false;
    }

    public void Dano(float dmg, bool crit, GameObject obj)
    {
        roamming = false;
        anim.SetBool("isSlam", true);
        autorizo = true;
        StopCoroutine("Autorizacao");
        StartCoroutine("Autorizacao");
        if (dano)
        {
            if (!block)
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
            }
            else
            {
                life -= (dmg / 2);
                if (crit == true)
                {
                    text.color = Color.red;
                    text.text = (dmg / 2).ToString() + " CRIT";
                }
                else
                {
                    text.color = Color.white;
                    text.text = (dmg / 2).ToString();
                }
            }           
            stun = true;
            isWalk = false;
            chamei = true;
            StopCoroutine("Pode");
            if (!block)
            {
                anim.SetTrigger("Dano");
            }
            else
            {
                anim.SetTrigger("BlockDmg");
            }
            dano = false;
        }
    }

    public void Slam(float dmg, bool crit, GameObject obj, float knockback)
    {
        roamming = false;
        dano = false;
        anim.SetBool("isSlam", true);
        slam = true;
        life -= dmg;
        autorizo = true;
        StopCoroutine("Autorizacao");
        StartCoroutine("Autorizacao");
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
        text.text = dmg.ToString();
        StopCoroutine("Pode");
        chamei = true;
        stun = true;
        anim.SetTrigger("Slam");
        Switch();
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