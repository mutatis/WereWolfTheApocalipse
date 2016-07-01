﻿using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour
{
    public PlayerAnimation playerAnim;
    public PlayerMovment playerMov;
    public PlayerDano playerDano;

    public bool mov;

    [HideInInspector]
    public bool bate, presa, jumpAttack, block, pulaBate;

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
                else if (Input.GetKeyDown(KeyCode.Joystick1Button2) && !presa)
                {
                    bate = true;
                    if ((!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoAndarilho")) && (attackComboNum == 0 || attackComboNum > 3))
                    {
                        SocoFraco();
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
        }

        if(mov && !playerDano.stun)
        {
            if(transform.localScale.x > 0)
                transform.Translate(0.02f, 0, 0);
            else
                transform.Translate(-0.02f, 0, 0);
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
        if (attackComboNum < 4)
        {
            attackComboNum++;
        }
        else
        {
            attackComboNum = 0;
        }
        if (obj == null)
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
                if(!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoAndarilho") && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco1Andarilho") && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho"))
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
        }
    }
}