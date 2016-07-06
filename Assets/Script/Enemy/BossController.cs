using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public Animator anim;

    public Rigidbody rig;

    public GameObject sprt, salto;

    public ProbabilidadeEnemy probabilidade;

    public ProbabilidadeEnemy probabilidade2;

    [HideInInspector]
    public SpriteRowCreator summon;

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
    public float vel1, vel2, dist;

    public int xp;

    public string[] attack;

    public GameObject player;

    public int marcado;

    GameObject obj;

    int dir;
    int contSalto, contSaltoMax;

    float lifeMax;

    bool isWalk = true;
    bool procura = true;
    bool prepare = true;
    public bool regen;

    Vector3 direction;

    void Start()
    {
        lifeMax = life;
        summon = Manager.manager.summoner;
        Wait();
        StartCoroutine("Pode");
        StartCoroutine("Regen");
        //Combate();
    }

    void Update()
    {
        if (player != null)
            dist = Vector3.Distance(player.transform.position, transform.position);

        if(lifeMax > life && regen)
        {
            life += 0.05f;
        }

        if(life > (lifeMax * 0.5f))
        {
            contSaltoMax = 2;
        }
        else if(life > (lifeMax * 0.25f))
        {
            contSaltoMax = 4;
        }
        else
        {
            contSaltoMax = 6;
        }

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
            StopAllCoroutines();
            anim.SetTrigger("Dead");
            dano = false;
            gameObject.GetComponent<SubBossController>().enabled = false;
        }

        if (player == null && procura)
        {
            var x = Random.Range(0, Manager.manager.posSubBoss.Length);
            dir = x;
            player = Manager.manager.posSubBoss[x];

        }
    }

    IEnumerator Regen()
    {
        regen = false;
        yield return new WaitForSeconds(2);
        regen = true;
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
        if (!perto)
        {
            num = probabilidade2.ChooseAttack();
            roamming = false;
            switch (num)
            {
                case 0:
                    StartCoroutine("Engage");
                    break;
                case 1:
                    StartCoroutine("Engage");
                    break;
                case 2:
                    Summoner();
                    break;

            }
        }
        else
        {
            num = probabilidade.ChooseAttack();
            switch (num)
            {
                case 0:
                    Soco();
                    break;

                case 1:
                    Defesa();
                    break;

                case 2:
                    var esco = Random.Range(0, Manager.manager.posSubBoss.Length);
                    dir = esco;
                    player = Manager.manager.posSubBoss[esco];
                    StartCoroutine("Engage");
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

    IEnumerator Pode()
    {
        var tempo = tempoResposta;
        yield return new WaitForSeconds(tempo);
        Combate();
    }

    void Summoner()
    {
        summon.CreateSprites();
    }

    void Switch()
    {
        //escolhe outro player
        roamming = false;
        player = null;
        procura = true;
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
        contSalto = 0;
        StopCoroutine("Pode");
        roamming = false;
        dano = false;
        while (dist > 0.5f)
        {
            dano = false;
            sprt.SetActive(false);
            direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.Translate((direction * 10) * Time.deltaTime);
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
        sprt.SetActive(true);
        dano = true;
        StartCoroutine("Attack");
        StopCoroutine("Engage");
    }

    IEnumerator Attack()
    {
        salto.SetActive(true);
        StopCoroutine("Pode");
        roamming = false;
        var esco = Random.Range(0, Manager.manager.posSubBoss.Length);
        dir = esco;
        player = Manager.manager.posSubBoss[esco];
        dist = Vector3.Distance(player.transform.position, transform.position);
        if (contSalto <= contSaltoMax)
        {
            while (dist > 0.5f)
            {
                direction = player.transform.position - transform.position;
                direction.Normalize();
                transform.Translate((direction * 20) * Time.deltaTime);
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
            contSalto += 1;
            StartCoroutine("Attack");
        }
        else
        {
            salto.SetActive(false);
            marcado = 0;
            StartCoroutine("Volta");
            StopCoroutine("Attack");
        }
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
            StopCoroutine("GO");
            StartCoroutine("GO");
            StopCoroutine("Regen");
            StartCoroutine("Regen");
            if ((player.transform.localScale.x > 0 && transform.localScale.x < 0) || (player.transform.localScale.x < 0 && transform.localScale.x > 0))
            {
                transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
            }
            anim.SetTrigger("Dano");
            anim.SetInteger("DanoEscolha", 1);
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
        StopCoroutine("Regen");
        StartCoroutine("Regen");
        text.text = dmg.ToString();
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