using UnityEngine;
using System.Collections;

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

    public void Ataca()
    {
        PlayerController.playerController.Ataca();
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (PlayerController.playerController.contador <= 2)
            {
                heal = FMODUnity.RuntimeManager.CreateInstance(socoFraco);
                heal.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                heal.start();
                other.gameObject.GetComponent<EnemyController>().Dano(playerStatus.dmg);
            }
            else if (PlayerController.playerController.contador >= 3)
            {
                heal = FMODUnity.RuntimeManager.CreateInstance(socoForte);
                heal.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                heal.start();
                other.gameObject.GetComponent<EnemyController>().Slam(playerStatus.dmg);
            }
        }
    }
}