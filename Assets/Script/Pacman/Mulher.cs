using UnityEngine;
using System.Collections;

public class Mulher : MonoBehaviour
{
    public GameObject target;

    public NavMeshAgent agent;

    Vector3 posInicial;
    
    void Start()
    {
        posInicial = transform.position;
    }

	void Update ()
    {
        agent.destination = target.transform.position;
	}

    public void Sexo()
    {
        transform.position = posInicial;
    }
}
