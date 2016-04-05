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
    public float x;
    [HideInInspector]
    public float z;
    [HideInInspector]
    public float rage;

    public bool jump;
    public bool stun;

    public int contador;

    public int engage;
    public int flooda;

    bool isRun = true;
    bool isAttack = true;

    void Awake()
    {
        playerController = this;
    }

    void Update()
    {
        if(playerStatus.life <= 0)
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

                        transform.Translate(new Vector3(x, 0, z));
                    }

                    if (isAttack)
                    {
                        if (!jump)
                        {
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
                        }
                        else
                        {
                            if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                            {
                                anim.anim.SetTrigger("JumpAttack");
                            }
                        }
                    }
                    break;
            }
        }
    }

    IEnumerator Floodando()
    {
        yield return new WaitForSeconds(1.2f);
        flooda = 0;
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
                contador = 0;
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

    public void Dano(float dmg)
    {
        playerStatus.life -= dmg;
        if (playerStatus.life > 0)
        {
            stun = true;
            anim.anim.SetTrigger("Dano");
            rage += playerStatus.rageRegen;
        }
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
        if(obj == null)
        {
            contador = 0;
        }
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