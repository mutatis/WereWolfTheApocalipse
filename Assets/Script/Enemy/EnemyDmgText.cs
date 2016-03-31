using UnityEngine;
using System.Collections;

public class EnemyDmgText : MonoBehaviour
{
    public EnemyController enemy;

	void Update ()
    {
	    if((enemy.transform.localScale.x < 0 && transform.localScale.x > 0) || (enemy.transform.localScale.x > 0 && transform.localScale.x < 0))
        {
            transform.localScale = new Vector3((transform.localScale.x * -1), transform.localScale.y, transform.localScale.z);
        }
	}
}
