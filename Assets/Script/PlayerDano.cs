using UnityEngine;
using System.Collections;

public class PlayerDano : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public PlayerAnimation playerAnim;
    public PlayerMovment playerMov;
    public PlayerAttackController playerAttack;

    public bool stun;

    public GameObject pegador;
    
    public void Dano(float dmg, GameObject obj)
    {
        if (!playerAttack.presa)
        {
            if (!playerMov.jump)
            {
                playerAnim.anim.SetBool("Stun", true);
                stun = true;
                playerAnim.anim.SetTrigger("Dano");
            }
        }
        else
        {
            pegador.GetComponent<EnemyController>().anim.SetTrigger("Dano");
        }
    }
}
