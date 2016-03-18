using UnityEngine;
using System.Collections;

public class AtributaSetinha : MonoBehaviour
{
    public Animator anim;

    public int tipo;

    bool podeDpad = true;

	void Update ()
    {
        if (podeDpad)
        {
            if (tipo == 0)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("DpadXP1") > 0)
                {
                    anim.SetTrigger("Aperto");
                }
            }
            else if (tipo == 1)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("DpadXP1") < 0)
                {
                    anim.SetTrigger("Aperto");
                }
            }
        }

        if (Input.GetAxisRaw("DpadXP1") == 0)
        {
            podeDpad = true;
        }
    }
}
