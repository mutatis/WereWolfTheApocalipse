using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour
{
    public PlayerAnimation playerAnim;
    public PlayerMovment playerMov;

    [HideInInspector]
    public bool isAttack, bate, mov;

    public int attackComboNum;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && !isAttack && !playerMov.jump)
        {
            bate = true;
            if(attackComboNum == 0 && (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") || !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoAndarilho")))
            {
                SocoFraco();
            }
            if (attackComboNum >= 3)
            {
                attackComboNum = 0;
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
        bate = false;
        attackComboNum++;
        playerAnim.anim.SetInteger("AttackComboNum", attackComboNum);
        playerAnim.anim.SetTrigger("SocoFraco");
        isAttack = true;
    }
}