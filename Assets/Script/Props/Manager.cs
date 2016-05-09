using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    public static Manager manager;

    public GameObject[] player;

    public GameObject[] enemy;

    void Awake()
    {
        manager = this;
    }

    void Update()
    {
        player = GameObject.FindGameObjectsWithTag("Player");

        enemy = GameObject.FindGameObjectsWithTag("Enemy");
    }
}