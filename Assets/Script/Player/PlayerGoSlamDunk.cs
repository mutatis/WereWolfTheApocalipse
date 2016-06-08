using UnityEngine;
using System.Collections;

public class PlayerGoSlamDunk : MonoBehaviour
{
    [HideInInspector]
    public GameObject obj;

    [HideInInspector]
    public int selecionado;

    Vector3 direction;

    float dist;

    void Update()
    {
        dist = Vector3.Distance(obj.transform.position, transform.position);

        if (dist > 2)
        {
            direction = obj.transform.position - transform.position;
            direction.Normalize();
            transform.Translate((direction.x), (direction.y / 4), direction.z / 4);
        }
        else
        {
            for (int i = 0; i < Manager.manager.player.Length; i++)
            {
                Manager.manager.player[i].GetComponent<PlayerController>().enabled = true;
            }
            Manager.manager.enemy[selecionado].GetComponent<EnemyController>().Dano(500, false, gameObject);
            Destroy(gameObject.GetComponent<PlayerGoSlamDunk>());
        }
    }
}
