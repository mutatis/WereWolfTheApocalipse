using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus playerStatus;

    public string nome;

    [HideInInspector]
    public float strength, dexterity, stamina, charisma, intelligence, spirit;

    public float critDamage, dmg, lift, blockEffect, hitStun, knockback; //strength
    
    public float critChance, speed, counterDmg, dodgeCooldown, rangedSpeed, rangedDmg; //dexterity
    
    public float life, regen, dmgTrash, resistances; //stamina
    
    public float giftPower, totenPower, packTactisPower; //charisma
    
    public float fetishCosts; // intelligence
    
    public float gnosiMax, gnosiRegen, rageMax, rageRegen, giftCritChance, giftCritBonus; //spirit

    void Awake()
    {
        playerStatus = this;
    }

    void Start()
    {
        strength = ManagerPlayerPontos.managerPontos.GetStrength(nome);
        dexterity = ManagerPlayerPontos.managerPontos.GetDexterity(nome);
        stamina = ManagerPlayerPontos.managerPontos.GetStamina(nome);
        charisma = ManagerPlayerPontos.managerPontos.GetCharisma(nome);
        intelligence = ManagerPlayerPontos.managerPontos.GetIntelligence(nome);
        spirit = ManagerPlayerPontos.managerPontos.GetSpirit(nome);

        critDamage += strength;

        dmg += (strength + 30);
    }
}