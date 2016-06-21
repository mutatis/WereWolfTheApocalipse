    using UnityEngine;
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
    [HideInInspector]
    public bool combate = true;

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
    bool chamei;
    bool isIdle = true;
    bool isRun = true;
    bool block;

    Vector3 direction;

    float dist;
    float dist1;
    float dist2;

    void Start()
    {
        obj = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine("Esquece");
        StartCoroutine("Procura");
    }
    
    void Update()
    {
        anim.SetBool("isWalk", isWalk);

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

            if (!isWalk && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdle"))
            {
                anim.SetTrigger("Idle");
            }

            if (roamming || player == null) 
			{
				transform.Translate (vel1, 0, vel2);
			}

			if (player != null) 
			{
				dist = Vector3.Distance (player.transform.position, transform.position);
			}

			if (dist > 1f && player != null) 
			{
				StartCoroutine (Engage ());
				isWalk = true;
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

    IEnumerator Esquece()
    {
        yield return new WaitForSeconds(10);
        Switch();
    }

    IEnumerator Procura()
    {
        procura = false;
        var tempo = Random.Range(1, 2);
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
        StopCoroutine("Procura");
        StopCoroutine("Pode");
        StartCoroutine("Pode");
        roamming = false;
        int num;
        if(!stun && dist < 0.5f && combate && !isWalk)
        {
            var temp = player.GetComponent<PlayerEngage>().playercontroller.transform.position;
            if (temp.x > transform.position.x && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
            if (temp.x < transform.position.x && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
            num = probabilidade.ChooseAttack();

            switch(num)
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
                    Switch();
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

    void Switch()
    {
        //escolhe outro player
        StopCoroutine("Esquece");
        StartCoroutine("Esquece");
        StopCoroutine("Engage");
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
        anim.SetTrigger("SocoFraco2");
    }

    IEnumerator Defesa()
    {
        roamming = false;
        combate = false;
        anim.SetTrigger("Block");
        block = true;
        yield return new WaitForSeconds(2f);
        combate = true;
        block = false;
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
                transform.Translate((direction / 25) * Time.deltaTime);
            }
            if (direction.x > 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
            
            dist = Vector3.Distance(player.transform.position, transform.position);
            yield return new WaitForSeconds(0.01f);
        }
        isWalk = false;
        isIdle = true;
        isRun = true;
        StopCoroutine("Pode");
        StartCoroutine("Pode");
    }


    public void DanoAgain()
    {
        roamming = false;
        dano = true;
        chamei = false;
        text.text = "";
    }

    public void Dano(float dmg, bool crit, GameObject obj)
    {
        roamming = false;
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
                player = obj;
            
            stun = true;
            isWalk = false;
            chamei = true;
            StopCoroutine("Pode");
            if ((player.transform.localScale.x < 0 && transform.localScale.x > 0) || (player.transform.localScale.x > 0 && transform.localScale.x < 0))
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
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
        if(player == null)
        {
            player = obj;
        }
        text.text = dmg.ToString();
        StopCoroutine("Pode");
        chamei = true;
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