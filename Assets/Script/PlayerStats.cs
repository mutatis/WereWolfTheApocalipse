using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public Player player;

    public string nome;

    [HideInInspector]
    public float rage, gnose;

    [HideInInspector]
    public bool call, lunar, crinos;

    void Start()
    {
        GnoseRestart();
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