﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerDano : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerStatus playerStatus;
    public PlayerAnimation playerAnim;
    public PlayerMovment playerMov;
    public PlayerAttackController playerAttack;
    public Agarra agarra;
    
    public bool stun;

    [FMODUnity.EventRef]
    public string blockSound;
    [FMODUnity.EventRef]
    public string socoFracoEnemy;

    FMOD.Studio.EventInstance audioInstanceCreator;

    public GameObject pegador;

    int slamCont;

    IEnumerator GO()
    {
        yield return new WaitForSeconds(1);
        slamCont = 0;
    }
    
    public void Dano(float dmg, GameObject obj)
    {
        StopCoroutine("GO");
        StartCoroutine("GO");
        if (playerStatus.life > 0 && !playerAnim.levanta)
        {
            if(playerMov.isGrab)
            {
                agarra.End();
            }
            if (!playerAttack.presa)
            {
                if (!playerMov.jump)
                {
                    if (slamCont > 3 && !playerAttack.block)
                    {
                        audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(socoFracoEnemy);
                        audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                        audioInstanceCreator.start();
                        if ((obj.transform.position.x < transform.position.x && transform.localScale.x > 0) || (obj.transform.position.x > transform.position.x && transform.localScale.x < 0))
                        {
                            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                        }
                        playerAnim.anim.SetBool("Stun", true);
                        stun = true;
                        playerAnim.anim.SetTrigger("Slam");
                        slamCont = 0;
                    }
                    else
                    {
                        if(slamCont > 3)
                        {
                            slamCont = 0;
                        }
                        if (playerAttack.block)
                        {
                            audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(blockSound);
                            audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                            audioInstanceCreator.start();
                        }
                        else
                        {
                            audioInstanceCreator = FMODUnity.RuntimeManager.CreateInstance(socoFracoEnemy);
                            audioInstanceCreator.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                            audioInstanceCreator.start();
                        }
                        if ((obj.transform.position.x > transform.position.x && transform.localScale.x > 0) || obj.transform.position.x < transform.position.x && transform.localScale.x > 0)
                        {
                            playerAnim.anim.SetBool("Costas", false);
                        }
                        else if ((obj.transform.position.x < transform.position.x && transform.localScale.x < 0) || obj.transform.position.x > transform.position.x && transform.localScale.x < 0)
                        {
                            playerAnim.anim.SetBool("Costas", true);
                        }
                        playerAnim.anim.SetBool("Stun", true);
                        stun = true;
                        playerAnim.anim.SetTrigger("Dano");
                    }
                }
            }
            else
            {
                pegador.GetComponent<EnemyController>().anim.SetTrigger("Dano");
            }
            slamCont++;
            dmg -= playerStatus.dmgTrash;
            if(playerAttack.block)
            {
                dmg *= playerStatus.blockEffect;
            }
            playerStatus.life -= dmg;
            playerStats.rage += playerStatus.rageRegen;
            if (playerStatus.life <= 0)
            {
                if ((obj.transform.position.x < transform.position.x && transform.localScale.x > 0) || (obj.transform.position.x > transform.position.x && transform.localScale.x < 0))
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
                stun = true;
                playerAnim.anim.SetTrigger("Dead");
            }
            StartCoroutine(Vibrar());
        }
    }

    IEnumerator Vibrar()
    {
        GamePad.SetVibration(0, 1f, 1f);
        yield return new WaitForSeconds(0.2f);
        GamePad.SetVibration(0, 0, 0);
    }
}
