using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGnose : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public PlayerController playerController;

    public Slider sli;

    void Start()
    {
        sli.maxValue = playerStatus.gnosiMax;
    }

    void Update()
    {
        sli.value = playerController.gnose;
    }
}

