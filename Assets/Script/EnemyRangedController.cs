using UnityEngine;
using System.Collections;

public class EnemyRangedController : MonoBehaviour
{
    public EnemyRanged longe;

    public float dist1;
    public float dist2;

    public GameObject[] player;

    void Update()
    {
        player = Manager.manager.player;

        dist1 = Vector3.Distance(player[0].transform.position, transform.position);
        if(PlayerPrefs.GetInt("Players") > 1)
        dist2 = Vector3.Distance(player[1].transform.position, transform.position);

        if (dist1 > 3f || dist2 > 3f)
        {
            longe.perto = false;
        }
        else
        {
            longe.perto = true;
            if (dist1 < 1f)
                longe.player = player[0];
            if (dist2 < 1f && PlayerPrefs.GetInt("Players") > 1)
                longe.player = player[1];
        }
    }
}