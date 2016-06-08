using UnityEngine;
using System.Collections;

public class Agarra : MonoBehaviour 
{
    public PlayerController player;

	GameObject enemy;

	bool pego;

	IEnumerator GO()
	{
        player.isJump = false;
		enemy.GetComponent<EnemyController> ().peguei = gameObject;
		yield return new WaitForSeconds (1.5f);
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
			if (!pego && !player.stun && player.isAttack) 
			{
				enemy = other.gameObject;
				pego = true;
				StartCoroutine ("GO");
			}
		}
	}
}
