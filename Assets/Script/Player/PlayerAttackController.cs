using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour
{
    public PlayerAnimation playerAnim;
    public PlayerMovment playerMov;
    public PlayerDano playerDano;
    public PlayerStatus playerStatus;
    public PlayerStats playerStats;

    public Animator grabAnim;

    public bool mov;

    [HideInInspector]
    public bool bate, presa, jumpAttack, block, pulaBate;

    public GameObject enemy;

    public GameObject obj;

    public int attackComboNum = 0;
    public int solta;

    void Update()
    {
        if (!Input.GetKey(KeyCode.Joystick1Button5))
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button2) && presa)
            {
                solta++;
            }

            if (!playerMov.jump)
            {
                if (Input.GetKeyDown(KeyCode.Joystick1Button1) && !mov)
                {
                    playerAnim.anim.SetTrigger("Block");
                    playerAnim.anim.SetBool("isBlock", true);
                    block = true;
                }
                else if (Input.GetKeyUp(KeyCode.Joystick1Button1))
                {
                    playerAnim.anim.SetBool("isBlock", false);
                    block = false;
                }
                else if (Input.GetKeyDown(KeyCode.Joystick1Button2) && !presa && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho"))
                {
                    bate = true;
                    if (((!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoAndarilho")) && (attackComboNum == 0 || attackComboNum > 3)) || playerMov.isGrab && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("GrabAttackAndarilho"))
                    {
                        SocoFraco();
                    }
                }
                else if(Input.GetKeyDown(KeyCode.Joystick1Button3) && !presa && playerMov.isGrab)
                {
                    GrabThrow();
                }
            }
            else if (playerMov.jump)
            {
                if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                {
                    JumpAttack();
                }
            }
        }

		if (Input.GetKeyUp(KeyCode.Joystick1Button1))
		{
			playerAnim.anim.SetBool("isBlock", false);
			block = false;
		}

        if(mov && !playerDano.stun && !playerMov.isGrab && !playerStats.crinos)
        {
            if(transform.localScale.x > 0)
				transform.Translate(0.02f, 0, 0, Space.World);
            else
				transform.Translate(-0.02f, 0, 0, Space.World);
        }
    }

    void GrabThrow()
    {
        grabAnim.SetTrigger("GrabThrow");
        playerAnim.anim.SetTrigger("GrabThrow");
    }

    void JumpAttack()
    {
        if (!jumpAttack)
        {
            playerAnim.anim.SetTrigger("JumpAttack");
            jumpAttack = true;
        }
    }

    public void Libero()
    {
        mov = false;
        if(bate)
        {
            SocoFraco();
        }
        else
        {
            attackComboNum = 0;
        }
    }

    void SocoFraco()
    {
        if (!playerMov.isGrab)
        {
            if (attackComboNum >= 3)
            {
                attackComboNum = 0;
            }
            if (attackComboNum < 3)
            {
                attackComboNum++;
            }
            else
            {
                attackComboNum = 0;
            }
            if (obj == null || attackComboNum > 3)
            {
                attackComboNum = 1;
            }
            playerAnim.anim.SetInteger("AttackComboNum", attackComboNum);
            bate = false;
            //attackComboNum++;
            //playerAnim.anim.SetInteger("AttackComboNum", attackComboNum);
            switch (attackComboNum)
            {
                case 1:
                    if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoAndarilho") && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco1Andarilho") && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho"))
                        playerAnim.anim.SetTrigger("SocoFraco");
                    break;

                case 2:
                    if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco1Andarilho"))
                        playerAnim.anim.SetTrigger("SocoFraco2");
                    break;

                case 3:
                    if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho"))
                        playerAnim.anim.SetTrigger("SocoFraco3");
                    break;

                default:
                    if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoAndarilho") && playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco1Andarilho") && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho"))
                        playerAnim.anim.SetTrigger("SocoFraco");
                    break;
            }
        }
        else
        {
            bate = false;
            if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("GrabAttackAndarilho"))
            {
                grabAnim.SetTrigger("GrabAttack");
                playerAnim.anim.SetTrigger("GrabAttack");
                enemy.GetComponent<EnemyController>().Dano(playerStatus.dmg, false, gameObject);
                playerAnim.audioInstance = FMODUnity.RuntimeManager.CreateInstance(playerAnim.socoFraco);
                playerAnim.audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                playerAnim.audioInstance.start();
            }
        }
    }
}