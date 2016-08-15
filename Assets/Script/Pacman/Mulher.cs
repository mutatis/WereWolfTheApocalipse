using UnityEngine;
using System.Collections;

public class Mulher : MonoBehaviour
{
    public GameObject target;

    public NavMeshAgent agent;

    Vector3 posInicial;

    bool roamming = true;
    
    void Start()
    {
        target = ManagerRoamming.roamming.posicao[Random.Range(0, ManagerRoamming.roamming.posicao.Length)];
        posInicial = transform.position;
    }

	void Update ()
    {
        if(roamming && Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            target = ManagerRoamming.roamming.posicao[Random.Range(0, ManagerRoamming.roamming.posicao.Length)];
        }

        agent.destination = target.transform.position;
	}

    IEnumerator GO()
    {
        yield return new WaitForSeconds(15);
        roamming = false;
    }

    public void Sexo()
    {
        transform.position = posInicial;
    }
}
