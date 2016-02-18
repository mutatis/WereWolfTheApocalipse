using UnityEngine;
using System.Collections;

public class EnemyAnim : MonoBehaviour
{
    public EnemyController controller;

    public void Return()
    {
        controller.stun = false;
    }
}
