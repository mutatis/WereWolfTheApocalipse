using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public Slider sli;

    void Start()
    {
        sli.maxValue = playerStatus.life;
    }

    void Update()
    {
        sli.value = playerStatus.life;
    }
}