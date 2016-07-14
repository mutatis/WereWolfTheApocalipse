using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Animator anim;
    public Animator animHead;

    public SpriteRenderer head;

    public Rigidbody rig;

    public ProbabilidadeEnemy probabilidade;

    public GameObject enemyAnim;

    public EnemyAnim enemyanim;

    public Vector3 deus;

    public bool stun;
    [HideInInspector]
    public bool dano = true;
    public bool roamming = true;
    public bool combate = true;
    [HideInInspector]
    public bool slam;
    public bool isEngage, procura;

    public TextMesh text;
    
    public float tempoResposta, life, vel1, vel2, dist;

    public int xp;

    public string[] attack;

    [HideInInspector]
    public GameObject peguei;
    public GameObject player, seta;

    public GameObject[] obj;

    bool isWalk = true;
    bool prepare = true;
    bool isIdle = true;
    bool isRun = true;
    bool taPego, chamei, block, costas, autorizo, denovo;

    Vector3 direction;
    
    float dist1, dist2, distOportunidade;

    int danoEscolha, oportunidade;

    void Start()
    {
        obj = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine("Procura");
    }
    
    void Update()
    {
        anim.SetBool("isWalk", isWalk);

        if(taPego && player.GetComponent<PlayerEngage>().playerAttack.solta >= 10)
        {
            Solta();
        }

        if (peguei == null)
        {
            anim.gameObject.SetActive(true);
            head.enabled = false;
        }

        if (peguei ==  null) 
		{
			if (life <= 0) 
			{
				StopAllCoroutines ();
                anim.SetBool("Morreu", true);
				anim.SetTrigger ("Dead");
				if (player != null)
                {
                    player.GetComponent<PlayerEngage>().enemy = null;
                    player.GetComponent<PlayerEngage>().engage--;
					enemyanim.nome = player.GetComponent<PlayerEngage> ().nome;
				}
				dano = false;
				gameObject.GetComponent<EnemyController> ().enabled = false;
			}

            if (!stun)
            {
                if (Manager.manager.player[0].GetComponent<PlayerMovment>().transform.position.x > transform.position.x && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
                else if (Manager.manager.player[0].GetComponent<PlayerMovment>().transform.position.x < transform.position.x && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
            }

            if ((roamming || player == null) && !stun) 
			{
                distOportunidade = Vector3.Distance(Manager.manager.player[0].transform.position, transform.position);

                if(distOportunidade > 1)
                {
                    oportunidade = 0;
                }

                if (distOportunidade > 1 || oportunidade > 0)
                {
                    transform.Translate(vel1, 0, vel2);
                    if (vel1 == 0 && vel2 == 0)
                    {
                        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdle"))
                            anim.SetTrigger("Idle");
                    }
                    else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRun") && !costas)
                    {
                        anim.SetTrigger("Run");
                    }
                    else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRunCostas") && costas)
                    {
                        anim.SetTrigger("Run");
                    }
                }
                else if(oportunidade < 1)
                {
                    anim.SetTrigger("SocoFraco0");
                    distOportunidade++;
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
                StartCoroutine ("Engage");
			} 
			else if (player != null && dist < 1 && !chamei) 
			{
				StartCoroutine ("Pode");
				chamei = true;
			} 
			else if (isWalk && player != null && dist > 2)
            {
                player.GetComponent<PlayerEngage>().enemy = null;
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
                    player.GetComponent<PlayerEngage>().enemy = gameObject;
                    player.GetComponent<PlayerEngage>().engage++;

                }
			}
		}	
        else
        {
            if(peguei.GetComponent<Agarra>().playerMov.transform.localScale.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if (peguei.GetComponent<Agarra>().playerMov.transform.localScale.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            StopAllCoroutines();
            transform.position = new Vector3(peguei.transform.position.x, transform.position.y, peguei.transform.position.z);
        }	
    }

    IEnumerator Procura()
    {
        procura = false;
        var tempo = Random.Range(2f, 3f);
        yield return new WaitForSeconds(tempo);
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
        StopCoroutine("Pode");
        StartCoroutine("Pode");
        roamming = false;
        int num;
        if(!stun && dist < 0.57f && combate && !player.GetComponent<PlayerEngage>().playerAttack.playerAnim.levanta && !taPego)
        {
            var temp = player.GetComponent<PlayerEngage>().playerAttack.transform.position;
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
        if((player.GetComponent<PlayerEngage>().playerStats.transform.position.x > transform.position.x && 
            player.GetComponent<PlayerEngage>().playerStats.transform.localScale.x > 0) || 
            (player.GetComponent<PlayerEngage>().playerStats.transform.position.x < transform.position.x && 
            player.GetComponent<PlayerEngage>().playerStats.transform.localScale.x < 0))
        {
            if (!player.GetComponent<PlayerEngage>().playerMov.isGrab)
            {
                player.GetComponent<PlayerEngage>().playerAttack.playerAnim.gameObject.SetActive(false);
                player.GetComponent<PlayerEngage>().playerDano.stun = true;
                player.GetComponent<PlayerEngage>().playerDano.pegador = gameObject;
                player.GetComponent<PlayerEngage>().playerAttack.presa = true;
                anim.SetBool("isGrab", true);
                anim.SetTrigger("Grab");
                roamming = false;
                combate = false;
                taPego = true;
            }
        }
    }

    void Solta()
    {
        anim.SetBool("isGrab", false);
        anim.SetTrigger("Idle");
        player.GetComponent<PlayerEngage>().playerAttack.playerAnim.gameObject.SetActive(true);
        player.GetComponent<PlayerEngage>().playerDano.stun = false;
        player.GetComponent<PlayerEngage>().playerAttack.solta = 0;
        player.GetComponent<PlayerEngage>().playerDano.pegador = null;
        player.GetComponent<PlayerEngage>().playerAttack.presa = false;
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
        if (player != null)
        {
            if (player.GetComponent<PlayerEngage>().engage > 0)
            {
                player.GetComponent<PlayerEngage>().enemy = null;
                player.GetComponent<PlayerEngage>().engage--;
            }
        }
        player = null;
        procura = true;
    }

    public void Wait()
    {
        roamming = true;
        int y = Random.Range(1, 3);
        if (y == 1)
        {
            vel1 = 0.03f;
        }
        else
        {
            vel1 = -0.03f;
        }
        int x = Random.Range(1, 3);
        if (x == 1)
        {
            vel2 = 0.03f;
        }
        else
        {
            vel2 = -0.03f;
        }
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
            StopCoroutine("Autorizacao");
            StartCoroutine("Autorizacao");
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
        roamming = false;
        yield return new WaitForSeconds(0.5f);
        if (isRun)
        {
            anim.SetTrigger("Run");
            isRun = false;
        }
        if (player != null)
        {
            while (dist > 0.5f)
            {
                if (player != null)
                {
                    direction = player.transform.position - transform.position;
                    direction.Normalize();
                    if (!stun)
                    {
                        deus = (direction / 7);
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
                        combate = false;
                    }
                    dist = Vector3.Distance(player.transform.position, transform.position);
                    yield return new WaitForSeconds(0.01f);
                }
                else
                {
                    var x = Random.Range(0, Manager.manager.playerEngage.Length);
                    if (Manager.manager.playerEngage[x].GetComponent<PlayerEngage>().engage < 1)
                    {
                        player = Manager.manager.playerEngage[x];
                        player.GetComponent<PlayerEngage>().enemy = gameObject;
                        player.GetComponent<PlayerEngage>().engage++;
                    }
                }
            }
            combate = true;
            isIdle = true;
            isRun = true;
            deus = new Vector3(0, 0, 0);
            isEngage = false;
            anim.SetTrigger("Idle");
            isWalk = false;
            StopCoroutine("Pode");
            StartCoroutine("Pode");
        }
        else
        {
            isEngage = false;
        }
    }


    public void DanoAgain()
    {
        combate = true;
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
        if (taPego)
        {
            Solta();
        }
        roamming = false;
        anim.SetBool("isSlam", true);
        autorizo = true;
        StopCoroutine("Autorizacao");
        StartCoroutine("Autorizacao");
        if (peguei == null)
        {
            if (danoEscolha >= 2)
            {
                danoEscolha = 0;
            }
            danoEscolha++;
        }
        else
        {
            anim.gameObject.SetActive(true);
            danoEscolha = 3;
        }
        anim.SetInteger("DanoEscolha", danoEscolha);
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
                if(danoEscolha == 3)
                {
                    animHead.SetTrigger("Dano");
                }
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
        if (anim.gameObject.active)
        {
            while (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySlam"))
            {
                anim.SetTrigger("Slam");
            }
        }
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
        Switch();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Parede1")
        {
            vel1 = -0.03f;
            int x = Random.Range(1, 3);
            if(x == 1)
            {
                vel2 = 0.03f;
            }
            else
            {
                vel2 = -0.03f;
            }
        }
        if (other.gameObject.tag == "Parede2")
        {
            print("P2");
            vel1 = 0.03f;
            int x = Random.Range(1, 3);
            if (x == 1)
            {
                vel2 = 0.03f;
            }
            else
            {
                vel2 = -0.03f;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Parede1")
        {
            vel1 = -0.03f;
            int x = Random.Range(1, 3);
            if (x == 1)
            {
                vel2 = 0.03f;
            }
            else
            {
                vel2 = -0.03f;
            }
        }
        if (other.gameObject.tag == "Parede2")
        {
            vel1 = 0.03f;
            int x = Random.Range(1, 3);
            if (x == 1)
            {
                vel2 = 0.03f;
            }
            else
            {
                vel2 = -0.03f;
            }
        }
    }
}