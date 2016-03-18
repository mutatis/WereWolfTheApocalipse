using UnityEngine;
using System.Collections;

public class AtributaSetinha : MonoBehaviour
{
    public Animator anim;

    public int tipo;

	void Update ()
    {
	    if(tipo == 0)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Joystick1Button11))
            {
                anim.SetTrigger("Aperto");
            }
        }
        else if (tipo == 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Joystick1Button10))
            {
                anim.SetTrigger("Aperto");
            }
        }
    }
}
