using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour
{
    public PlayerAnimation playerAnim;

    public bool isAttack;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && !isAttack)
        {
            SocoFraco();
        }
    }

    void SocoFraco()
    {
        playerAnim.anim.SetTrigger("SocoFraco0");
        isAttack = true;
    }
}
