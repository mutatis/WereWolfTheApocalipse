using UnityEngine;
using System.Collections;

public class PlayerEngage : MonoBehaviour
{
    public int engage;

    public PlayerController playercontroller;

    public string nome;

    void Start()
    {
        nome = playercontroller.nome;
    }
}
