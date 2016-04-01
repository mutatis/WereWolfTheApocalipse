using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public Animator anim;

    [FMODUnity.EventRef]
    public string socoFraco;

    FMOD.Studio.EventInstance heal;

   [FMODUnity.EventRef]
    public string socoForte;

    bool run;

	void Update ()
    {
        if ((PlayerController.playerController.x != 0 || PlayerController.playerController.z != 0) && !PlayerController.playerController.jump)
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
        PlayerController.playerController.stun = false;
    }

    public void Liberated()
    {
        PlayerController.playerController.Liberated();
    }

    public void Dead()
    {
        SceneManager.LoadScene("Dead");
    }

    public void Ataca()
    {
        PlayerController.playerController.Ataca();
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
        }
        else
        {
            other.gameObject.GetComponent<EnemyController>().Dano(playerStatus.dmg, false);
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
            other.gameObject.GetComponent<EnemyController>().Slam(playerStatus.dmg * 2, true);
        }
        else
        {
            other.gameObject.GetComponent<EnemyController>().Slam(playerStatus.dmg, false);
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (PlayerController.playerController.contador <= 2 && other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                Dano(other.gameObject);
            }
            else if (PlayerController.playerController.contador >= 3 && other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                SlamDmg(other.gameObject);
            }
        }
    }
}