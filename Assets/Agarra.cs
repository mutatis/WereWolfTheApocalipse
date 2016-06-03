using UnityEngine;
using System.Collections;

public class Agarra : MonoBehaviour 
{
	GameObject enemy;
	bool pego;

	void Update()
	{
		if (enemy != null && pego) 
		{

		}
	}

	IEnumerator GO()
	{
		enemy.GetComponent<EnemyController> ().peguei = true;
		yield return new WaitForSeconds (1.5f);
		enemy = null;
		pego = false;
		enemy.GetComponent<EnemyController> ().peguei = false;
		StopCoroutine("GO");
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			if (!pego) 
			{
				enemy = other.gameObject;
				pego = true;
				StartCoroutine ("GO");
			}
		}
	}
}
