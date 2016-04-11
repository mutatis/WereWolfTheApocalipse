using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    public string nome;

    [HideInInspector]
    public float strength, dexterity, stamina, charisma, intelligence, spirit; //padrao
    
    public float critDamage, dmg, lift, blockEffect, hitStun, knockback; //strength
    
    public float critChance, speed, counterDmg, dodgeCooldown, rangedSpeed, rangedDmg; //dexterity
    
    public float life, lifeMax, regen, dmgTrash, resistances; //stamina
    
    public float giftPower, totenPower, packTactisPower; //charisma
    
    public float fetishCosts, giftPower2, resistances2; // intelligence
    
    public float gnosiMax, gnosiRegen, rageMax, rageRegen, giftCritChance, giftCritBonus; //spirit

    void Start()
    {
        strength = ManagerPlayerPontos.managerPontos.GetStrength(nome);
        dexterity = ManagerPlayerPontos.managerPontos.GetDexterity(nome);
        stamina = ManagerPlayerPontos.managerPontos.GetStamina(nome);
        charisma = ManagerPlayerPontos.managerPontos.GetCharisma(nome);
        intelligence = ManagerPlayerPontos.managerPontos.GetIntelligence(nome);
        spirit = ManagerPlayerPontos.managerPontos.GetSpirit(nome);

        //Strength
        dmg = ((strength * 0.2f) * dmg) + dmg;
        critDamage = ((strength * 0.2f) * critDamage) + critDamage;
        blockEffect = ((strength * 0.05f) * blockEffect) + blockEffect;
        if(strength > 2)
        {
            lift = (strength / 2);
        }
        knockback = ((strength / 2f) * knockback) + knockback;

        //Dexterity
        rangedDmg = ((dexterity * 0.2f) * rangedDmg) + rangedDmg;
        speed = ((dexterity * 0.04f) * speed) + speed;
        rangedDmg = speed / 2;
        rangedDmg = ((dexterity * 0.2f) * rangedDmg) + rangedDmg;
        counterDmg = ((dexterity * 0.05f) * counterDmg) + counterDmg;
        critChance = (dexterity * 4.5f) + critChance;

        //Stamina
        life = ((stamina * 0.2f) * life) + life;
        lifeMax = life;
        resistances = ((stamina * 0.2f) * resistances) + resistances;
        dmgTrash = ((stamina * 0.2f) * dmgTrash) + dmgTrash;
        regen = (stamina * 2);

        //Charisma
        giftPower = ((charisma + 0.2f) + giftPower) + giftPower;
        totenPower = ((charisma + 0.2f) + totenPower) + totenPower;
        packTactisPower = ((charisma + 0.2f) + packTactisPower) + packTactisPower;

        //Intelligence
        fetishCosts = (intelligence + 4.5f) + fetishCosts;
        giftPower2 = ((charisma + 0.2f) + giftPower2) + giftPower2; ;
        resistances2 = ((stamina * 0.2f) * resistances2) + resistances2;

        //Spirit
        gnosiMax = ((spirit * 0.2f) * gnosiMax) + gnosiMax;
        gnosiRegen = ((spirit * 0.2f) * gnosiRegen) + gnosiRegen;
        rageMax = ((spirit * 0.2f) * rageMax) + rageMax;
        rageRegen = (spirit * 4.5f) + rageRegen;
        giftCritChance = (spirit * 4.5f) + giftCritChance;
        giftCritBonus = (spirit * 4.5f) + giftCritBonus;
    }
}