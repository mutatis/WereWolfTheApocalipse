using UnityEngine;
using System.Collections;

public class PlayerDano : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public PlayerAnimation playerAnim;
    public PlayerMovment playerMov;

    public bool stun;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Dano(1);
        }
    }

    public void Dano(float dmg)
    {
        if (!playerMov.jump)
        {
            playerAnim.anim.SetBool("Stun", true);
            stun = true;
            playerAnim.anim.SetTrigger("Dano");
        }
    }
}
