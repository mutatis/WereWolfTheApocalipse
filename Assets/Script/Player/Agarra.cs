using UnityEngine;
using System.Collections;

public class Agarra : MonoBehaviour 
{
    public PlayerController player;

    [FMODUnity.EventRef]
    public string pega;

    [FMODUnity.EventRef]
    public string joga;

    FMOD.Studio.EventInstance agarraAudioInstance;

    GameObject enemy;

	bool pego;

    public void Joga()
    {
        agarraAudioInstance = FMODUnity.RuntimeManager.CreateInstance(joga);
        agarraAudioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        agarraAudioInstance.start();
        enemy.GetComponent<EnemyController>().Slam(player.playerStatus.dmg, false, gameObject, 5);
        End();
    }

	IEnumerator GO()
	{
        player.isGrab = true;
        player.enemy = enemy;
        player.anim.anim.SetBool("Grab", true);
        if ((player.x != 0 || player.z != 0) && !player.jump)
        {
            player.anim.anim.SetTrigger("Run");            
        }
        else
        {
            player.anim.anim.SetTrigger("Idle");
        }
        player.isJump = false;
		enemy.GetComponent<EnemyController> ().peguei = gameObject;
		yield return new WaitForSeconds (1.5f);
        End();
	}

    void End()
    {
        player.isGrab = false;
        player.anim.anim.SetBool("Grab", false);
        if ((player.x != 0 || player.z != 0) && !player.jump)
        {
            player.anim.anim.SetTrigger("Run");
        }
        else
        {
            player.anim.anim.SetTrigger("Idle");
        }
        enemy.GetComponent<EnemyController>().peguei = null;
        enemy = null;
        pego = false;
        player.isJump = true;
        StopCoroutine("GO");
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			if (!pego && !player.stun && player.isAttack && other.gameObject.GetComponent<EnemyController>().life > 0 && (player.x > 0 || player.x < 0) && !player.jump)
            {
                agarraAudioInstance = FMODUnity.RuntimeManager.CreateInstance(pega);
                agarraAudioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                agarraAudioInstance.start();
                enemy = other.gameObject;
				pego = true;
				StartCoroutine ("GO");
			}
		}
	}
}
