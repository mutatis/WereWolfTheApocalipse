using UnityEngine;
using System.Collections;

public class PlayerMovment : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public PlayerAnimation playerAnim;
    public PlayerAttackController playerAttack;
    public PlayerDano playerDano;

    public Rigidbody rig;

    FMOD.Studio.EventInstance audioInstanceCreator;

    [FMODUnity.EventRef]
    public string jumpSound;

    [HideInInspector]
    public bool isMov, jump;

    float x, z;

    void Update()
    {
        transform.Translate(new Vector3((x * playerStatus.speed), 0, (z * playerStatus.speed)));

        if (!isMov)
        {
            x = Input.GetAxis("HorizontalP1");
            if (!jump && !playerAttack.block && !playerDano.stun)
            {
                z = Input.GetAxis("VerticalP1");
                if (Input.GetKeyDown(KeyCode.Joystick1Button0) && !Input.GetKey(KeyCode.Joystick1Button5))
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

        if((x != 0 || z != 0) && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("RunAndarilho"))
        {
            playerAnim.anim.SetTrigger("Run");
        }
        else if(x == 0 && z == 0 && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("IdleAndarilho"))
        {
            playerAnim.anim.SetTrigger("Idle");
        }

        if(x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
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