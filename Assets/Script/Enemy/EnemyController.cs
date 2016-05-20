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
    [HideInInspector]
    public bool roamming = true;
    [HideInInspector]
    public bool combate = true;

    public TextMesh text;

    public float tempoResposta;
    public float life;
    public float vel1, vel2;

    public int xp;

    public string[] attack;

    public GameObject player, seta;

    public GameObject[] obj;

    bool isWalk = true;
    bool procura = false;
    bool prepare = true;
    bool chamei;

    Vector3 direction;

    float dist;
    float dist1;
    float dist2;

    void Start()
    {
        obj = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine("Procura");
    }
    
    void Update()
    {
        if (life <= 0)
        {
            StopAllCoroutines();
            anim.SetTrigger("Dead");
            if (player != null)
            {
                player.GetComponent<PlayerEngage>().engage--;
                enemyanim.nome = player.GetComponent<PlayerEngage>().nome;
            }
            dano = false;
            gameObject.GetComponent<EnemyController>().enabled = false;
        }

        if (roamming && player == null)
        {
            transform.Translate(vel1, 0, vel2);
        }

        if (player != null)
        {
            dist = Vector3.Distance(player.transform.position, transform.position);
        }

        if (dist > 4f && player != null && !chamei)
        {
            StartCoroutine(Engage());
            roamming = true;
            isWalk = true;
        }
        else if(isWalk && player != null && dist > 2)
        {
            player.GetComponent<PlayerEngage>().engage--;
            player = null;
            StopCoroutine("Pode");
            StartCoroutine("Pode");
            isWalk = false;
        }
        else if(player == null)
        {
            Prepare();
        }

        if(player == null && procura)
        {
            var x = Random.Range(0, Manager.manager.playerEngage.Length);
            if(Manager.manager.playerEngage[x].GetComponent<PlayerEngage>().engage < 1)
            {
                player = Manager.manager.playerEngage[x];
                player.GetComponent<PlayerEngage>().engage++;
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
            player = null;
            Denovo();
        }
    }

    void Denovo()
    {
        StopCoroutine("Procura");
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
                    Defesa();
                    break;

                case 2:
                    SpecialMove();
                    break;

                case 3:
                    //Wait();
                    break;

                case 4:
                    Flank();
                    break;

                case 5:
                    Flee();
                    break;

                case 6:
                    Soco();
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
        roamming = true;
        if (player.GetComponent<PlayerEngage>().engage > 0)
        {
            player.GetComponent<PlayerEngage>().engage--;
        }
        player = null;
        Denovo();
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
    }

    void SpecialMove()
    {
        roamming = false;
        combate = false;
        anim.SetTrigger("SocoFraco2");
    }

    void Defesa()
    {
        roamming = false;
        combate = false;
        anim.SetTrigger("SocoForte");
    }

    void Soco()
    {
        roamming = false;
        combate = false;
        anim.SetTrigger("SocoFraco0");
    }

    IEnumerator Engage()
    {
        StopCoroutine("Procura");
        roamming = false;
        chamei = true;
        yield return new WaitForSeconds(1);
        while (dist > 0.5f)
        {
            direction = player.transform.position - transform.position;
            direction.Normalize();
            if (!stun)
            {
                transform.Translate((direction * 4) * Time.deltaTime);
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
            yield return new WaitForEndOfFrame();
        }
        isWalk = false;
        chamei = false;
        StopCoroutine("Pode");
        StartCoroutine("Pode");
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
            if(player == null)
            {
                player = obj;
            }
            stun = true;
            isWalk = false;
            StopCoroutine("Pode");
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
        if(player == null)
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