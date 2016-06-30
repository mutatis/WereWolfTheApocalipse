﻿using UnityEngine;
using System.Collections;

public class PlayerDano : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public PlayerAnimation playerAnim;
    public PlayerMovment playerMov;
    public PlayerAttackController playerAttack;

    public bool stun;

    public GameObject pegador;

    int slamCont;
    
    public void Dano(float dmg, GameObject obj)
    {
        if (playerStatus.life > 0 && !playerAnim.levanta)
        {
            if (!playerAttack.presa)
            {
                if (!playerMov.jump)
                {
                    if (slamCont <= 3)
                    {
                        if (obj.transform.position.x > transform.position.x)
                        {
                            playerAnim.anim.SetBool("Costas", false);
                        }
                        else if (obj.transform.position.x < transform.position.x)
                        {
                            playerAnim.anim.SetBool("Costas", true);
                        }
                        playerAnim.anim.SetBool("Stun", true);
                        stun = true;
                        playerAnim.anim.SetTrigger("Dano");
                    }
                    else
                    {
                        if ((obj.transform.position.x < transform.position.x && transform.localScale.x > 0) || (obj.transform.position.x > transform.position.x && transform.localScale.x < 0))
                        {
                            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                        }
                        playerAnim.anim.SetBool("Stun", true);
                        stun = true;
                        playerAnim.anim.SetTrigger("Slam");
                        slamCont = 0;
                    }
                }
            }
            else
            {
                pegador.GetComponent<EnemyController>().anim.SetTrigger("Dano");
            }
            slamCont++;
            dmg -= playerStatus.dmgTrash;
            playerStatus.life -= dmg;
        }
    }
}
