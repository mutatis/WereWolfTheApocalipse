﻿using UnityEngine;
using System.Collections;

public class XpPlayer : MonoBehaviour
{
    public PlayerController player;

    public int[] xp;

    [HideInInspector]
    public int lvl;

    void Start()
    {
        lvl = PlayerPrefs.GetInt(player.nome + "LVL");
    }

    void Update()
    {
        if(PlayerPrefs.GetInt(player.nome + "XP") >= xp[lvl])
        {
            Upo();
        }
    } 

    void Upo()
    {
        lvl++;
        PlayerPrefs.SetInt(player.nome + "LVL", lvl);
        Debug.Break();
    }
}
