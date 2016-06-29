using UnityEngine;
using System.Collections;

public class PlayerEngage : MonoBehaviour
{
    public int engage;

    public PlayerStats playerStats;

    public PlayerAttackController playerAttack;

    public PlayerDano playerDano;

    public string nome;

    void Start()
    {
        nome = playerStats.nome;
    }
}
