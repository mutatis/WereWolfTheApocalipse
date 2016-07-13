using UnityEngine;
using System.Collections;

public class Agarra : MonoBehaviour 
{
    public PlayerStatus playerStatus;
    public PlayerMovment playerMov;
    public PlayerAttackController playerAttack;
    public PlayerAnimation playerAnim;
    public PlayerDano playerDano;

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
        enemy.GetComponent<EnemyController>().Slam(playerStatus.dmg, false, gameObject, 5);
        End();
    }

	IEnumerator GO()
	{
        playerMov.isGrab = true;
        enemy.GetComponent<EnemyController>().head.enabled = false;
        enemy.GetComponent<EnemyController>().head.gameObject.SetActive(true);
        playerAttack.enemy = enemy;
        enemy.GetComponent<EnemyController>().enemyAnim.SetActive(false);
        playerAnim.anim.SetBool("Grab", true);
        playerAnim.anim.SetTrigger("GrabInicio");
        if ((playerMov.x != 0 || playerMov.z != 0))
        {
            playerAnim.anim.SetTrigger("Run");            
        }
        else
        {
            playerAnim.anim.SetTrigger("Idle");
        }
        playerMov.isJump = true;
		enemy.GetComponent<EnemyController> ().peguei = gameObject;
		yield return new WaitForSeconds (2f);
        End();
	}

    public void End()
    {
        enemy.GetComponent<EnemyController>().head.gameObject.SetActive(false);
        enemy.GetComponent<EnemyController>().head.enabled = false;
        enemy.GetComponent<EnemyController>().enemyAnim.SetActive(true);
        playerMov.isGrab = false;
        playerAnim.anim.SetBool("Grab", false);
        if ((playerMov.x != 0 || playerMov.z != 0) && !playerMov.jump)
        {
            playerAnim.anim.SetTrigger("Run");
        }
        else
        {
            playerAnim.anim.SetTrigger("Idle");
        }
        enemy.GetComponent<EnemyController>().peguei = null;
        pego = false;
        playerMov.isJump = false;
        enemy.GetComponent<EnemyController>().Switch();
        enemy = null;
        StopCoroutine("GO");
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			if (!pego && !playerDano.stun && other.gameObject.GetComponent<EnemyController>().life > 0 && !other.gameObject.GetComponent<EnemyController>().slam && (playerMov.x > 0 || playerMov.x < 0) && !playerMov.jump)
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
