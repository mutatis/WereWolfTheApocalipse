using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public PlayerMovment playerMov;
    public PlayerStatus playerStatus;
    public PlayerAnimation anim;

    public Player player;

    public Animator crinosAnim;
    public Animator playerAnim;

    public string nome;
    
    public float rage, gnose;

    [HideInInspector]
    public bool call, lunar, crinos;

    void Start()
    {
        GnoseRestart();
    }

    void Update()
    {
        if (rage >= playerStatus.rageMax && Input.GetKeyDown(KeyCode.Joystick1Button5) && !crinos && !playerMov.isGrab)
        {
            playerStatus.pode = true;
            crinos = true;
            anim.anim.runtimeAnimatorController = crinosAnim.GetComponent<Animator>().runtimeAnimatorController;
            StartCoroutine("Crinos");
        }
    }

    IEnumerator Crinos()
    {
        yield return new WaitForSeconds(30);
        anim.anim.runtimeAnimatorController = playerAnim.GetComponent<Animator>().runtimeAnimatorController;
        crinos = false;
        anim.GetComponent<SpriteRenderer>().color = Color.white;
        rage = 0;
    }

    IEnumerator GnoseStart()
    {
        yield return new WaitForSeconds(1);
        if (gnose < playerStatus.gnosiMax)
        {
            gnose += playerStatus.gnosiRegen;
        }
        GnoseRestart();
    }

    void GnoseRestart()
    {
        StopCoroutine("GnoseStart");
        StartCoroutine("GnoseStart");
    }
}

public enum Player
{
    Player1,
    Player2,
    Player3,
    Player4
}