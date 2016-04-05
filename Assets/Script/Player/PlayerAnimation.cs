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

    FMOD.Studio.EventInstance heal;

   [FMODUnity.EventRef]
    public string socoForte;

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

    public void Stun()
    {
        playerController.stun = false;
    }

    public void Liberated()
    {
        playerController.Liberated();
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
        heal = FMODUnity.RuntimeManager.CreateInstance(socoFraco);
        heal.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        heal.start();
        int x = Random.Range(0, 100);
        if(x <= playerStatus.critChance)
        {
            other.gameObject.GetComponent<EnemyController>().Dano(playerStatus.dmg * 2, true);
            playerController.rage += ((playerStatus.dmg * 2) * playerStatus.rageRegen);
        }
        else
        {
            other.gameObject.GetComponent<EnemyController>().Dano(playerStatus.dmg, false);
            playerController.rage += ((playerStatus.dmg) * playerStatus.rageRegen);
        }
    }

    void SlamDmg(GameObject other)
    {

        heal = FMODUnity.RuntimeManager.CreateInstance(socoForte);
        heal.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        heal.start();
        int x = Random.Range(0, 100);
        if (x <= playerStatus.critChance)
        {
            other.gameObject.GetComponent<EnemyController>().Slam((playerStatus.dmg * 2) + (playerStatus.dmg * 0.25f), true);
            playerController.rage += ((playerStatus.dmg * 2) * playerStatus.rageRegen);
        }
        else
        {
            other.gameObject.GetComponent<EnemyController>().Slam((playerStatus.dmg + (playerStatus.dmg * 0.25f)), false);
            playerController.rage += ((playerStatus.dmg) * playerStatus.rageRegen);
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (playerController.contador <= 2 && other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                Dano(other.gameObject);
            }
            else if (playerController.contador >= 3 && other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                SlamDmg(other.gameObject);
            }
        }
    }
}