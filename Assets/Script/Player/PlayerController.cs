using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerController;

    public PlayerAnimation anim;

    public Player player;

    [HideInInspector]
    public float x;
    [HideInInspector]
    public float z;
    
    public int contador;

    public bool isRun = true;
    public bool isAttack = true;

    void Awake()
    {
        playerController = this;
    }

    void Update()
    {
        if(x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
        }
        else if (x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
        }

        switch (player)
        {
            case Player.Player1:
                if (isRun)
                {
                    x = Input.GetAxis("HorizontalP1");
                    z = Input.GetAxis("VerticalP1");

                    transform.Translate(x, 0, z);
                }

                if (isAttack)
                {
                    if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                    {
                        StopCombo();
                        SocoFraco();
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                    {
                        SocoForte();
                    }
                }
                break;
        }
    }

    void SocoForte()
    {
        isAttack = false;
        isRun = false;
        anim.anim.SetTrigger("SocoForte");
    }

    void SocoFraco()
    {
        contador++;
        isAttack = false;
        isRun = false;
        switch (contador)
        {
            case 1:
                anim.anim.SetTrigger("SocoFraco0");
                break;
            case 2:
                anim.anim.SetTrigger("SocoFraco1");
                break;
            case 3:
                anim.anim.SetTrigger("SocoFraco2");    
                break;
            default:
                isRun = true;
                isAttack = true;
                contador = 0;
                break;
        }
        PlayCombo();
    }

    public void PlayCombo()
    {
        StartCoroutine("GO");
    }

    public void StopCombo()
    {
        StopCoroutine("GO");
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(0.75f);
        contador = 0;
        isRun = true;
        isAttack = true;
        StopCoroutine("GO");
    }

    public void Liberated()
    {
        isRun = true;
        isAttack = true;
    }

    public void Ataca()
    {
        isAttack = true;
    }
}

public enum Player
{
    Player1,
    Player2,
    Player3,
    Player4
}