using UnityEngine;
using System.Collections;

public class PlayerEngage : MonoBehaviour
{
    public int engage;

    public PlayerController player;

    public string nome;

    void Start()
    {
        nome = player.nome;
    }
}
