using UnityEngine;
using System.Collections;

public class EnemyProbController : MonoBehaviour
{
    public EnemyController enemyController;

    public ProbabilidadeEnemy probEnemy;

    public float tempProbabilidade;

	void Update ()
    {
        if (enemyController.player != null)
        {
            if (transform.position.x > enemyController.player.transform.position.x && Manager.manager.player[0].GetComponent<PlayerMovment>().transform.localScale.x < 0 ||
                transform.position.x < enemyController.player.transform.position.x && Manager.manager.player[0].GetComponent<PlayerMovment>().transform.localScale.x > 0)
            {
                foreach (Attack elem in probEnemy.attack)
                {
                    if (elem.attack == "Defesa")
                    {
                        elem.probalidade = 0;
                    }
                }
            }
            else
            {
                foreach (Attack elem in probEnemy.attack)
                {
                    if (elem.attack == "Defesa")
                    {
                        print("sdiluhsdkhfkashlsdhfja");
                        elem.probalidade = tempProbabilidade;
                    }
                }
            }
        }
	}
}