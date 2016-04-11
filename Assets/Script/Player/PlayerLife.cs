using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public Slider sli;

    void Start()
    {
        sli.maxValue = playerStatus.lifeMax;
    }

    void Update()
    {
        sli.maxValue = playerStatus.lifeMax;
        sli.value = playerStatus.life;
    }
}