using UnityEngine;
using System.Collections;

public class PlayerMovment : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public PlayerAnimation playerAnim;

    [HideInInspector]
    public bool isMov;

    public float x, z;

    void Update()
    {
        transform.Translate(new Vector3((x * playerStatus.speed), 0, (z * playerStatus.speed)));

        if (!isMov)
        {
            x = Input.GetAxis("HorizontalP1");
            z = Input.GetAxis("VerticalP1");
        }
        else
        {
            x = 0;
            z = 0;
        }

        if((x != 0 || z != 0) && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("RunAndarilho"))
        {
            playerAnim.anim.SetTrigger("Run");
        }
        else if(x == 0 && z == 0 && !playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("IdleAndarilho"))
        {
            playerAnim.anim.SetTrigger("Idle");
        }

        if(x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
}