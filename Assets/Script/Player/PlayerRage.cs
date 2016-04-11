using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRage : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public PlayerController playerController;

    public Slider sli;

    void Start()
    {
        sli.maxValue = playerStatus.rageMax;
    }

    void Update()
    {
        sli.value = playerController.rage;
    }
}