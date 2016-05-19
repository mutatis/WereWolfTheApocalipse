using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    public static Manager manager;

    public GameObject[] player;

    public GameObject[] playerEngage;

    public GameObject[] enemy;

    public GameObject[] posSubBoss;

    void Awake()
    {
        manager = this;
    }

    void Update()
    {
        player = GameObject.FindGameObjectsWithTag("Player");

        playerEngage = GameObject.FindGameObjectsWithTag("PlayerEngage");

        enemy = GameObject.FindGameObjectsWithTag("Enemy");
    }
}