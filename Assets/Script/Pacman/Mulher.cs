using UnityEngine;
using System.Collections;

public class Mulher : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    
	void Update ()
    {
        agent.destination = target.transform.position;
	}
}
