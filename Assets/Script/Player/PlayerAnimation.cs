using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public PlayerController playerController;

    public Animator anim;

    [FMODUnity.EventRef]
    public string socoFraco;

    [FMODUnity.EventRef]
    public string crit;

    [FMODUnity.EventRef]
    public string queda;

    FMOD.Studio.EventInstance audioInstance;

   [FMODUnity.EventRef]
    public string socoForte;

    GameObject obj;

    bool run;

	void Update ()
    {
        if ((playerController.x != 0 || playerController.z != 0) && !playerController.jump)
        {
            if (run)
            {
                anim.SetTrigger("Run");
                run = false;
            }
        }
        else if(!run)
        {
            anim.SetTrigger("Idle");
            run = true;
        }
	}

    public void Queda()
    {
        audioInstance = FMODUnity.RuntimeManager.CreateInstance(queda);
        audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        audioInstance.start();
    }

    public void Levanta()
    {
        if (playerController.transform.localScale.x > 0)
        {
            playerController.transform.position = new Vector3(playerController.transform.position.x - 2f, playerController.transform.position.y, playerController.transform.position.z);
        }
        else if (playerController.transform.localScale.x < 0)
        {
            playerController.transform.position = new Vector3(playerController.transform.position.x + 2f, playerController.transform.position.y, playerController.transform.position.z);
        }
    }

    public void Stun()
    {
        playerController.stun = false;
    }

    public void Miss()
    {
        if (obj == null)
        {
            playerController.contador = 0;
        }
    }

    public void Liberated()
    {
        playerController.Liberated(obj);
        obj = null;
    }

    public void Dead()
    {
        SceneManager.LoadScene("Dead");
    }

    public void Ataca()
    {
        playerController.Ataca();
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
            playerController.rage += playerStatus.rageRegen;
            audioInstance = FMODUnity.RuntimeManager.CreateInstance(crit);
            audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            audioInstance.start();
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
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
            playerController.rage += playerStatus.rageRegen;
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
            if (other.gameObject.tag == "Enemy")
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
            playerController.rage += playerStatus.rageRegen;
            audioInstance = FMODUnity.RuntimeManager.CreateInstance(crit);
            audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            audioInstance.start();
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
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
            playerController.rage += playerStatus.rageRegen;
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (playerController.contador <= 2 && other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
            if (playerController.contador >= 3 && other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }
        }
        else if (other.gameObject.tag == "EnemyRanged")
        {
            if (playerController.contador <= 2 && other.gameObject.GetComponent<EnemyRanged>().life > 0 && other.gameObject.GetComponent<EnemyRanged>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
            if (playerController.contador >= 3 && other.gameObject.GetComponent<EnemyRanged>().life > 0 && other.gameObject.GetComponent<EnemyRanged>().dano)
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }
        }
        else if (other.gameObject.tag == "SubBoss")
        {
            if (playerController.contador <= 2 && other.gameObject.GetComponent<SubBossController>().life > 0 && other.gameObject.GetComponent<SubBossController>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
            if (playerController.contador >= 3 && other.gameObject.GetComponent<SubBossController>().life > 0 && other.gameObject.GetComponent<SubBossController>().dano)
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }
        }
        else if (other.gameObject.tag == "Boss")
        {
            if (playerController.contador <= 2 && other.gameObject.GetComponent<BossController>().life > 0 && other.gameObject.GetComponent<BossController>().dano)
            {
                obj = other.gameObject;
                Dano(obj);
            }
            if (playerController.contador >= 3 && other.gameObject.GetComponent<BossController>().life > 0 && other.gameObject.GetComponent<BossController>().dano)
            {
                obj = other.gameObject;
                SlamDmg(obj);
            }
        }
    }
}