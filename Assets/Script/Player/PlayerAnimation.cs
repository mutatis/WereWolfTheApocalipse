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
        if (playerMov.transform.localScale.x > 0)
        {
            playerMov.transform.position = new Vector3(playerMov.transform.position.x - 2f, playerMov.transform.position.y, playerMov.transform.position.z);
        }
        else if (playerMov.transform.localScale.x < 0)
        {
            playerMov.transform.position = new Vector3(playerMov.transform.position.x + 2f, playerMov.transform.position.y, playerMov.transform.position.z);
        }
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
        playerAttack.isAttack = false;
        anim.SetBool("Stun", false);
        OkIdle();
    }

    public void Miss()
    {

    }

    public void Atacando()
    {
        obj = null;
        playerMov.isMov = true;
        playerAttack.mov = true;
    }

    public void Liberated()
    {
        playerAttack.obj = obj;
        playerAttack.Libero();
        playerAttack.mov = false;
        playerMov.isMov = false;
        playerMov.transform.position = new Vector3(transform.position.x, playerMov.transform.position.y, playerMov.transform.position.z);
        /* playerController.transform.position = new Vector3(transform.position.x, playerController.transform.position.y, playerController.transform.position.z);
         playerController.Liberated(obj);*/
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
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().Dano(playerStatus.dmg * 2, true, playerMov.gameObject);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                other.gameObject.GetComponent<EnemyRanged>().Dano(playerStatus.dmg * 2, true, playerMov.gameObject);
            }
            else if (other.gameObject.tag == "SubBoss")
            {
                other.gameObject.GetComponent<SubBossController>().Dano(playerStatus.dmg * 2, true, playerMov.gameObject);
            }
            else if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<BossController>().Dano(playerStatus.dmg * 2, true, playerMov.gameObject);
            }
            //playerController.rage += playerStatus.rageRegen;
            audioInstance = FMODUnity.RuntimeManager.CreateInstance(crit);
            audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            audioInstance.start();
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().Dano(playerStatus.dmg, false, playerMov.gameObject);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                other.gameObject.GetComponent<EnemyRanged>().Dano(playerStatus.dmg, false, playerMov.gameObject);
            }
            else if (other.gameObject.tag == "SubBoss")
            {
                other.gameObject.GetComponent<SubBossController>().Dano(playerStatus.dmg, false, playerMov.gameObject);
            }
            else if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<BossController>().Dano(playerStatus.dmg, false, playerMov.gameObject);
            }
            //playerController.rage += playerStatus.rageRegen;
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
            if (other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
            /*if (other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }*/
        }
        else if (other.gameObject.tag == "EnemyRanged")
        {
            if (other.gameObject.GetComponent<EnemyRanged>().life > 0 && other.gameObject.GetComponent<EnemyRanged>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
            /*if (other.gameObject.GetComponent<EnemyRanged>().life > 0 && other.gameObject.GetComponent<EnemyRanged>().dano)
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }*/
        }
        else if (other.gameObject.tag == "SubBoss")
        {
            if (other.gameObject.GetComponent<SubBossController>().life > 0 && other.gameObject.GetComponent<SubBossController>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
            /*if (other.gameObject.GetComponent<SubBossController>().life > 0 && other.gameObject.GetComponent<SubBossController>().dano)
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }*/
        }
        else if (other.gameObject.tag == "Boss")
        {
            if (other.gameObject.GetComponent<BossController>().life > 0 && other.gameObject.GetComponent<BossController>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
            /*if (other.gameObject.GetComponent<BossController>().life > 0 && other.gameObject.GetComponent<BossController>().dano)
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }*/
        }
    }
}