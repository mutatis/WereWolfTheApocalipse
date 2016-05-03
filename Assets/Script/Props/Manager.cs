using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    public static Manager manager;

    public GameObject[] player;

    void Awake()
    {
        manager = this;
    }

    void Update()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
    }
}
