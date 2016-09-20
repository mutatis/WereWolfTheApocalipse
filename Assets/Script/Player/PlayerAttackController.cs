﻿using UnityEngine;
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

    public int attackComboNum = 0, maxCombo;
    public int solta;

    public float ataque;

    void Update()
    {
        if(playerStats.crinos)
        {
            maxCombo = 3;
        }
        else
        {
            maxCombo = 4;
        }

        if (!Input.GetKey(KeyCode.Joystick1Button5) && !Input.GetKey(KeyCode.LeftShift))
        {
            if ((Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Z)) && presa)
            {
                solta++;
            }

            if (!playerMov.jump)
            {
                if ((Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.C)) && !mov)
                {
                    playerAnim.anim.SetTrigger("Block");
                    playerAnim.anim.SetBool("isBlock", true);
                    block = true;
                }
                else if (Input.GetKeyUp(KeyCode.Joystick1Button1) || Input.GetKeyUp(KeyCode.C))
                {
                    playerAnim.anim.SetBool("isBlock", false);
                    block = false;
                }
                else if ((Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Z)) && !presa && 
                    !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") && !block)
                {
                    bate = true;
                    if (((!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoAndarilho")) && (attackComboNum == 0 || attackComboNum > 4)) || 
                        playerMov.isGrab && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("GrabAttackAndarilho") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoForte"))
                    {
                        SocoFraco();
                    }
                }
                else if((Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.X)) && !presa && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") && !block)
                {
                    if (((!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoAndarilho"))) ||
                         playerMov.isGrab && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("GrabAttackAndarilho") &&
                             !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoForte"))
                    {
                        SocoFraco(true);
                    }
                }
                else if((Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.X)) && !presa && playerMov.isGrab)
                {
                    GrabThrow();
                }
            }
            else if (playerMov.jump)
            {
                if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Z))
                {
                    JumpAttack();
                }
            }
        }

		if (Input.GetKeyUp(KeyCode.Joystick1Button1) || Input.GetKeyUp(KeyCode.C))
		{
			playerAnim.anim.SetBool("isBlock", false);
			block = false;
		}

        if(mov && !playerDano.stun && !playerMov.isGrab && !playerStats.crinos)
        {
            if (transform.localScale.x > 0)
            {
                transform.Translate(ataque, 0, 0, Space.World);
                playerMov.x = ataque * 1.5f;
            }
            else
            {
                transform.Translate(ataque * -1, 0, 0, Space.World);
                playerMov.x = (ataque * 1.5f) * -1;
            }
        }
    }

    void GrabThrow()
    {
        playerStats.rage += playerStatus.rageRegen;
        grabAnim.SetTrigger("GrabThrow");
        playerAnim.anim.SetTrigger("GrabThrow");
    }

    void JumpAttack()
    {
        if (!jumpAttack)
        {
            playerMov.pulo = 0;
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

    void SocoFraco(bool socoForte = false)
    {
        if (!playerMov.isGrab)
        {
            if(!socoForte)
            { 
                if (attackComboNum >= maxCombo)
                {
                    attackComboNum = 0;
                }
                if (attackComboNum < maxCombo)
                {
                    attackComboNum++;
                }
                else
                {
                    attackComboNum = 0;
                }
                if (obj == null || attackComboNum > maxCombo)
                {
                    attackComboNum = 1;
                }
                playerAnim.anim.SetInteger("AttackComboNum", attackComboNum);
                bate = false;
                switch (attackComboNum)
                {
                    case 1:
                        if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoAndarilho") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco1Andarilho") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoForte") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco3Presa") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco4Presa") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco5Presa") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco6Presa") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco7Presa"))
                        {
                            playerAnim.anim.SetTrigger("SocoFraco");
                            ataque = 0.03f;
                        }
                        break;

                    case 2:
                        if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco1Andarilho") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco4Presa"))
                        {
                            playerAnim.anim.SetTrigger("SocoFraco2");
                            ataque = 0.03f;
                        }
                        break;

                    case 3:
                        if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco5Presa") && !playerStats.crinos)
                        {
                            playerAnim.anim.SetTrigger("SocoFraco2");
                            ataque = 0.03f;
                        }
                        else if(!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco5Presa"))
                        { 
                            playerAnim.anim.SetTrigger("SocoFraco3");
                            ataque = 0.03f;
                        }
                        break;

                    case 4:
                        if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco6Presa"))
                        {
                            playerAnim.anim.SetTrigger("SocoFraco3");
                            ataque = 0.03f;
                        }
                        break;

                    default:
                        if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFracoAndarilho") &&
                            playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco1Andarilho") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFraco2Andarilho") &&
                            !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoForte"))
                        {
                            playerAnim.anim.SetTrigger("SocoFraco");
                            ataque = 0.03f;
                        }
                        break;
                }
            }
            else
            {
                if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoForte") && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFortePresa1") && 
                    !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("SocoFortePresa2"))
                {
                    playerAnim.anim.SetInteger("SocoForteEscolha", Random.Range(0, 3));
                    playerAnim.anim.SetTrigger("SocoForte");
                    ataque = 0;
                }
            }
        }
        else
        {
            bate = false;
            if (!playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("GrabAttackAndarilho"))
            {
                playerStats.rage += playerStatus.rageRegen;
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