using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public static PlayerController playerController;

    public PlayerAnimation anim;

    public Rigidbody rig;

    public Player player;
    
    [HideInInspector]
    public float rage, gnose, z, x;

    [HideInInspector]
    public bool block;
    public bool jump, stun, crinos, call;

    public int contador, engage, flooda;

    public string nome;

    bool isRun = true;
    bool isAttack = true;

    void Awake()
    {
        playerController = this;
    }

    void Start()
    {
        StartCoroutine("GnoseStart");
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (playerStatus.life <= 0)
            {
                anim.anim.SetTrigger("Dead");
                gameObject.GetComponent<PlayerController>().enabled = false;
            }

            if (!stun && playerStatus.life > 0)
            {
                if (x > 0 && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
                }
                else if (x < 0 && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
                }

                switch (player)
                {
                    case Player.Player1:
                        if (isRun)
                        {
                            x = Input.GetAxis("HorizontalP1");
                            if (!jump)
                            {
                                z = Input.GetAxis("VerticalP1");
                            }
                            else
                            {
                                z = 0;
                            }
                            transform.Translate(new Vector3((x * playerStatus.speed), 0, (z * playerStatus.speed)));
                        }

                        if (isAttack)
                        {
                            if (!jump)
                            {
                                if(rage >= playerStatus.rageMax && Input.GetKeyDown(KeyCode.Joystick1Button5) && !crinos)
                                {
                                    playerStatus.pode = true;
                                    crinos = true;
                                    anim.GetComponent<SpriteRenderer>().color = Color.blue;
                                    StartCoroutine("Crinos");
                                }
                                if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Space))
                                {
                                    flooda++;
                                    StopCoroutine("Floodando");
                                    StartCoroutine("Floodando");
                                    StopCombo();
                                    SocoFraco();
                                }
                                else if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                                {
                                    flooda++;
                                    StopCoroutine("Floodando");
                                    StartCoroutine("Floodando");
                                    SocoForte();
                                }
                                else if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                                {
                                    Jump();
                                }
                                else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                                {
                                    block = true;
                                    anim.anim.SetBool("Block", true);
                                    isAttack = false;
                                    isRun = false;
                                }
                            }
                            else
                            {
                                if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                                {
                                    anim.anim.SetTrigger("JumpAttack");
                                }
                            }
                        }

                        if (Input.GetKeyUp(KeyCode.Joystick1Button1))
                        {
                            block = false;
                            anim.anim.SetBool("Block", false);
                            isAttack = true;
                            isRun = true;
                        }
                        break;

                    case Player.Player2:
                        if (isRun)
                        {
                            x = Input.GetAxis("HorizontalP2");
                            if (!jump)
                            {
                                z = Input.GetAxis("VerticalP2");
                            }
                            else
                            {
                                z = 0;
                            }
                            transform.Translate(new Vector3((x * playerStatus.speed), 0, (z * playerStatus.speed)));
                        }

                        if (isAttack)
                        {
                            if (!jump)
                            {
                                if (Input.GetKeyDown(KeyCode.Joystick2Button2) || Input.GetKeyDown(KeyCode.Space))
                                {
                                    flooda++;
                                    StopCoroutine("Floodando");
                                    StartCoroutine("Floodando");
                                    StopCombo();
                                    SocoFraco();
                                }
                                else if (Input.GetKeyDown(KeyCode.Joystick2Button3))
                                {
                                    flooda++;
                                    StopCoroutine("Floodando");
                                    StartCoroutine("Floodando");
                                    SocoForte();
                                }
                                else if (Input.GetKeyDown(KeyCode.Joystick2Button0))
                                {
                                    Jump();
                                }
                                else if (Input.GetKeyDown(KeyCode.Joystick2Button1))
                                {
                                    block = true;
                                    anim.anim.SetBool("Block", true);
                                    isAttack = false;
                                    isRun = false;
                                }
                            }
                            else
                            {
                                if (Input.GetKeyDown(KeyCode.Joystick2Button2))
                                {
                                    anim.anim.SetTrigger("JumpAttack");
                                }
                            }
                        }

                        if (Input.GetKeyUp(KeyCode.Joystick2Button1))
                        {
                            block = false;
                            anim.anim.SetBool("Block", false);
                            isAttack = true;
                            isRun = true;
                        }
                        break;
                }
            }
        }
    }

    IEnumerator Crinos()
    {
        yield return new WaitForSeconds(30);
        crinos = false;
        anim.GetComponent<SpriteRenderer>().color = Color.white;
        rage = 0;
    }

    IEnumerator Floodando()
    {
        yield return new WaitForSeconds(1.2f);
        flooda = 0;
    }

    IEnumerator GnoseStart()
    {
        yield return new WaitForSeconds(1);
        gnose += playerStatus.gnosiRegen;
        GnoseRestart();
    }

    void GnoseRestart()
    {
        StopCoroutine("GnoseStart");
        StartCoroutine("GnoseStart");
    }

    void Jump()
    {
        jump = true;
        anim.anim.SetBool("Jump", jump);
        rig.velocity = new Vector3(rig.velocity.x, 10, rig.velocity.z);
    }

    void SocoForte()
    {
        isAttack = false;
        isRun = false;
        anim.anim.SetTrigger("SocoForte");

        PlayCombo();
    }

    void SocoFraco()
    {   
        if (contador > 3)
        {
            contador = 0;
        }
        contador++;
        isAttack = false;
        isRun = false;
        switch (contador)
        {
            case 1:
                anim.anim.SetTrigger("SocoFraco0");
                break;
            case 2:
                anim.anim.SetTrigger("SocoFraco1");
                break;
            case 3:
                anim.anim.SetTrigger("SocoFraco2");    
                break;
            default:
                isRun = true;
                isAttack = true;
                break;
        }
        PlayCombo();
    }

    public void PlayCombo()
    {
        StartCoroutine("GO");
    }

    public void StopCombo()
    {
        StopCoroutine("GO");
    }

    public void DanoMagico(float dmg, int tipo)
    {
        if (tipo == 1)
        {
            playerStatus.life -= (dmg * playerStatus.resistances);
        }
        else if(tipo == 2)
        {
            playerStatus.life -= (dmg * playerStatus.resistances2);
        }
    }

    public void Dano(float dmg)
    {
        if (playerStatus.life > 0)
        {
            if (block)
            {
                dmg = dmg * playerStatus.blockEffect;
                stun = true;
                anim.anim.SetTrigger("Dano");
            }
            rage += playerStatus.rageRegen;
        }
        playerStatus.life -= dmg;
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(0.75f);
        contador = 0;
        isRun = true;
        isAttack = true;
        StopCoroutine("GO");
    }

    public void Liberated(GameObject obj)
    {
        isRun = true;
        isAttack = true;
        jump = false;
        anim.anim.SetBool("Jump", jump);
    }

    public void Ataca()
    {
        isAttack = true;
    }
}

public enum Player
{
    Player1,
    Player2,
    Player3,
    Player4
}