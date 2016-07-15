using UnityEngine;
using System.Collections;

public class PlayerMovment : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerStatus playerStatus;
    public PlayerAnimation playerAnim;
    public PlayerAttackController playerAttack;
    public PlayerDano playerDano;

    public Rigidbody rig;

    public SpriteRenderer sprite;

    FMOD.Studio.EventInstance audioInstanceCreator;

    [FMODUnity.EventRef]
    public string jumpSound;

    public bool run;
    [HideInInspector]
    public bool isMov, jump, isGrab, isJump;

    [HideInInspector]
    public float x, z;

    public int contInput, xRun = 1;

    void Update()
    {
        transform.Translate(new Vector3((x * playerStatus.speed), 0, (z * playerStatus.speed)));

        if(playerStats.crinos && !sprite.flipX)
        {
            sprite.flipX = true;
        }
        else if(!playerStats.crinos && sprite.flipX)
        {
            sprite.flipX = false;
        }

        if (!isMov)
        {
            playerAnim.anim.SetInteger("ContInput", xRun);
            if (!playerAttack.jumpAttack)
            {
                x = Input.GetAxis("HorizontalP1") * xRun;
                if((x > 0 || x < 0) && !run && contInput == 0 && playerStats.crinos)
                {
                    StartCoroutine("Run");
                }
            }
            if (!jump && !playerAttack.block && !playerDano.stun && xRun == 1)
            {
                z = Input.GetAxis("VerticalP1") * 2;
                if (Input.GetKeyDown(KeyCode.Joystick1Button0) && !Input.GetKey(KeyCode.Joystick1Button5) && !isJump)
                {
                    Jump();
                }
                playerAnim.anim.SetFloat("RigVel", 0);
            }
            else if(jump && !playerAttack.block && !playerDano.stun)
            {
                z = 0;
                playerAnim.anim.SetFloat("RigVel", rig.velocity.y);
            }
            else if(playerAttack.block || playerDano.stun)
            {
                x = 0;
                z = 0;
            }
        }
        else
        {
            x = 0;
            z = 0;
        }

        if(playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("GrabWalkAndarilho"))
        {
            if (playerAttack.enemy != null)
            {
                playerAttack.enemy.GetComponent<EnemyController>().anim.SetBool("Preso", true);
                playerAttack.enemy.GetComponent<EnemyController>().anim.gameObject.SetActive(true);
            }
        }

        if((x != 0 || z != 0) && ((!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("RunAndarilho") && !isGrab) || 
            (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("GrabWalkAndarilho") && isGrab)))
        {
            playerAnim.anim.SetTrigger("Run");
        }
        else if(x == 0 && z == 0 && ((!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("IdleAndarilho") && !isGrab) || 
            (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("GrabIdleAndarilho") && isGrab)))
        {
            playerAnim.anim.SetTrigger("Idle");
            if (playerAttack.enemy != null)
            {
                playerAttack.enemy.GetComponent<EnemyController>().anim.gameObject.SetActive(false);
            }
        }

        if(x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        if(run && x == 0)
        {
            contInput = 1;
        }

        if(run && (x > 0 || x < 0) && contInput == 1)
        {
            Corre();
        }

        if(xRun > 1 && x == 0)
        {
            xRun = 1;
            run = false;
            contInput = 0;
        }
    }

    void Corre()
    {
        run = false;
        contInput = 0;
        xRun = 2;
    }

    IEnumerator Run()
    {
        run = true;
        yield return new WaitForSeconds(0.5f);
        contInput = 0;
        run = false;
    }

    void Jump()
    {
        audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(jumpSound);
        audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        audioInstanceCreator.start();
        jump = true;
        playerAnim.anim.SetTrigger("Pulo");
        playerAnim.anim.SetBool("Jump", jump);
        rig.velocity = new Vector3(rig.velocity.x, 10, rig.velocity.z);
    }

    void Normal()
    {
        playerAttack.jumpAttack = false;
        jump = false;
        playerAnim.anim.SetBool("Jump", jump);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Chao" && jump)
        {
            Normal();
        }
    }
}