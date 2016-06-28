using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerStatus playerStatus;

    //public PlayerController playerController;

    public PlayerMovment playerMov;
    public PlayerAttackController playerAttack;
    public PlayerDano playerDano;

    public Animator anim;

    [FMODUnity.EventRef]
    public string socoFraco;

    [FMODUnity.EventRef]
    public string crit;

    [FMODUnity.EventRef]
    public string queda;

    [HideInInspector]
    public FMOD.Studio.EventInstance audioInstance;

   [FMODUnity.EventRef]
    public string socoForte;

    public bool levanta;

    GameObject obj;

    bool idle = true;
    bool run;
    

    public void Queda()
    {
        audioInstance = FMODUnity.RuntimeManager.CreateInstance(queda);
        audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        audioInstance.start();
    }

    void NotIdle()
    {
        idle = false;
    }

    void OkIdle()
    {
        idle = true;
    }

    public void Levanta()
    {
        /*if (playerController.transform.localScale.x > 0)
        {
            playerController.transform.position = new Vector3(playerController.transform.position.x - 2f, playerController.transform.position.y, playerController.transform.position.z);
        }
        else if (playerController.transform.localScale.x < 0)
        {
            playerController.transform.position = new Vector3(playerController.transform.position.x + 2f, playerController.transform.position.y, playerController.transform.position.z);
        }*/
        levanta = false;
    }

    public void Cai()
    {
        levanta = true;
    }

    public void DanoAgain()
    {
        /*playerController.apanha = false;
        playerController.jump = false;*/
    }

    public void Stun()
    {
        /*playerController.stun = false;
        playerController.isAttack = true;*/
        playerDano.stun = false;
        anim.SetBool("Stun", false);
        OkIdle();
    }

    public void Miss()
    {
        if (obj == null)
        {
            playerAttack.attackComboNum = 0;
            //playerController.contador = 0;
        }
    }

    public void Atacando()
    {
        playerMov.isMov = true;
        playerAttack.mov = true;
    }

    public void Liberated()
    {
        playerAttack.Libero();
        playerMov.isMov = false;
        playerMov.transform.position = new Vector3(transform.position.x, playerMov.transform.position.y, playerMov.transform.position.z);
        /* playerController.transform.position = new Vector3(transform.position.x, playerController.transform.position.y, playerController.transform.position.z);
         playerController.Liberated(obj);*/
        obj = null;
        OkIdle();
    }

    public void Dead()
    {
        SceneManager.LoadScene("Dead");
    }

    public void Ataca()
    {
        NotIdle();
        playerAttack.isAttack = false;
        // playerController.Ataca();
    }

    void Dano(GameObject other)
    {
        audioInstance = FMODUnity.RuntimeManager.CreateInstance(socoFraco);
        audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        audioInstance.start();
        int x = Random.Range(0, 100);
        if(x <= playerStatus.critChance)
        {
            /*if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().Dano(playerStatus.dmg * 2, true, playerController.gameObject);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                other.gameObject.GetComponent<EnemyRanged>().Dano(playerStatus.dmg * 2, true, playerController.gameObject);
            }
            else if (other.gameObject.tag == "SubBoss")
            {
                other.gameObject.GetComponent<SubBossController>().Dano(playerStatus.dmg * 2, true, playerController.gameObject);
            }
            else if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<BossController>().Dano(playerStatus.dmg * 2, true, playerController.gameObject);
            }
            playerController.rage += playerStatus.rageRegen;*/
            audioInstance = FMODUnity.RuntimeManager.CreateInstance(crit);
            audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            audioInstance.start();
        }
        else
        {
            /*if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().Dano(playerStatus.dmg, false, playerController.gameObject);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                other.gameObject.GetComponent<EnemyRanged>().Dano(playerStatus.dmg, false, playerController.gameObject);
            }
            else if (other.gameObject.tag == "SubBoss")
            {
                other.gameObject.GetComponent<SubBossController>().Dano(playerStatus.dmg, false, playerController.gameObject);
            }
            else if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<BossController>().Dano(playerStatus.dmg, false, playerController.gameObject);
            }
            playerController.rage += playerStatus.rageRegen;*/
        }
    }

    public void SlamDmg(GameObject other)
    {
        audioInstance = FMODUnity.RuntimeManager.CreateInstance(socoForte);
        audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        audioInstance.start();
        int x = Random.Range(0, 100);
        if (x <= playerStatus.critChance)
        {
            /*if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().Slam((playerStatus.dmg * 2) + (playerStatus.dmg * 0.25f), true, playerController.gameObject, playerStatus.knockback);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                other.gameObject.GetComponent<EnemyRanged>().Slam((playerStatus.dmg * 2) + (playerStatus.dmg * 0.25f), true, playerController.gameObject, playerStatus.knockback);
            }
            else if (other.gameObject.tag == "SubBoss")
            {
                other.gameObject.GetComponent<SubBossController>().Slam((playerStatus.dmg * 2) + (playerStatus.dmg * 0.25f), true, playerController.gameObject, playerStatus.knockback);
            }
            else if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<BossController>().Slam((playerStatus.dmg * 2) + (playerStatus.dmg * 0.25f), true, playerController.gameObject, playerStatus.knockback);
            }
            playerController.rage += playerStatus.rageRegen;*/
            audioInstance = FMODUnity.RuntimeManager.CreateInstance(crit);
            audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            audioInstance.start();
        }
        else
        {
            /*if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().Slam((playerStatus.dmg + (playerStatus.dmg * 0.25f)), false, playerController.gameObject, playerStatus.knockback);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                other.gameObject.GetComponent<EnemyRanged>().Slam((playerStatus.dmg + (playerStatus.dmg * 0.25f)), false, playerController.gameObject, playerStatus.knockback);
            }
            else if (other.gameObject.tag == "SubBoss")
            {
                other.gameObject.GetComponent<SubBossController>().Slam((playerStatus.dmg + (playerStatus.dmg * 0.25f)), false, playerController.gameObject, playerStatus.knockback);
            }
            else if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<BossController>().Slam((playerStatus.dmg + (playerStatus.dmg * 0.25f)), false, playerController.gameObject, playerStatus.knockback);
            }
            playerController.rage += playerStatus.rageRegen;*/
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (playerAttack.attackComboNum <= 2 && other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
            if (playerAttack.attackComboNum >= 3 && other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }
        }
        else if (other.gameObject.tag == "EnemyRanged")
        {
            if (playerAttack.attackComboNum <= 2 && other.gameObject.GetComponent<EnemyRanged>().life > 0 && other.gameObject.GetComponent<EnemyRanged>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
            if (playerAttack.attackComboNum >= 3 && other.gameObject.GetComponent<EnemyRanged>().life > 0 && other.gameObject.GetComponent<EnemyRanged>().dano)
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }
        }
        else if (other.gameObject.tag == "SubBoss")
        {
            if (playerAttack.attackComboNum <= 2 && other.gameObject.GetComponent<SubBossController>().life > 0 && other.gameObject.GetComponent<SubBossController>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
            if (playerAttack.attackComboNum >= 3 && other.gameObject.GetComponent<SubBossController>().life > 0 && other.gameObject.GetComponent<SubBossController>().dano)
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }
        }
        else if (other.gameObject.tag == "Boss")
        {
            if (playerAttack.attackComboNum <= 2 && other.gameObject.GetComponent<BossController>().life > 0 && other.gameObject.GetComponent<BossController>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
            if (playerAttack.attackComboNum >= 3 && other.gameObject.GetComponent<BossController>().life > 0 && other.gameObject.GetComponent<BossController>().dano)
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }
        }
    }
}