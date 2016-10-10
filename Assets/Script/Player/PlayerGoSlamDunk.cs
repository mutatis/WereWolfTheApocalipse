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
        if (obj != null)
        {
            dist = Vector3.Distance(obj.transform.position, transform.position);
        }

        if (dist > 2)
        {
            direction = obj.transform.position - transform.position;
            direction.Normalize();
            transform.Translate((direction.x), (direction.y / 4), direction.z / 4);
        }
        else
        {
            obj.GetComponent<SlamArea>().Matador();
            for (int i = 0; i < Manager.manager.player.Length; i++)
            {
                Manager.manager.player[i].GetComponent<PlayerStats>().playerAnim.SetBool("SlamDunk", false);
            }
            //Destroy(obj);
            Destroy(gameObject.GetComponent<PlayerGoSlamDunk>());
        }
    }
}
