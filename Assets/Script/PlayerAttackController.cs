using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour
{
    public PlayerAnimation playerAnim;
    public PlayerMovment playerMov;

    [HideInInspector]
    public bool isAttack, bate, mov, jumpAttack, block;

    public int attackComboNum;

    void Update()
    {
        if (!playerMov.jump)
        {
            if(Input.GetKeyDown(KeyCode.Joystick1Button1) && !mov)
            {
                playerAnim.anim.SetTrigger("Block");
                playerAnim.anim.SetBool("isBlock", true);
                block = true;
            }
            else if(Input.GetKeyUp(KeyCode.Joystick1Button1))
            {
                playerAnim.anim.SetBool("isBlock", false);
                block = false;
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button2) && !isAttack)
            {
                bate = true;
                if (attackComboNum == 0 && (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") || !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoAndarilho")))
                {
                    SocoFraco();
                }
                if (attackComboNum >= 3)
                {
                    attackComboNum = 0;
                }
            }
        }
        else if (playerMov.jump)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                JumpAttack();
            }
        }
        if(mov)
        {
            if(transform.localScale.x > 0)
                transform.Translate(0.04f, 0, 0);
            else
                transform.Translate(-0.04f, 0, 0);
        }
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
        jumpAttack = false;
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
        bate = false;
        attackComboNum++;
        playerAnim.anim.SetInteger("AttackComboNum", attackComboNum);
        playerAnim.anim.SetTrigger("SocoFraco");
        isAttack = true;
    }
}