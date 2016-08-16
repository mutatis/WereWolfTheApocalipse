using UnityEngine;
using System.Collections;
using UnityEditor;

public class Mulher : MonoBehaviour
{
    public GameObject pacman, posInicial;

    public NavMeshAgent agent;

    public Vector2 vel;

    GameObject target;

    bool roamming = true, volta;
    
    void Start()
    {
        //StartCoroutine("GO");
        //target = ManagerRoamming.roamming.posicao[Random.Range(0, ManagerRoamming.roamming.posicao.Length)];
    }

	void Update ()
    {
        transform.Translate(vel, Space.World);
        /*if(volta && Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            volta = false;
        }

        if(roamming && Vector3.Distance(transform.position, target.transform.position) < 1f && !volta)
        {
            target = ManagerRoamming.roamming.posicao[Random.Range(0, ManagerRoamming.roamming.posicao.Length)];
        }
        else if(!roamming && !volta)
        {
            target = pacman;
        }

        agent.destination = target.transform.position;*/
	}

    /*IEnumerator GO()
    {
        yield return new WaitForSeconds(15);
        print("sdçvjsdoivhsirj");
        roamming = false;
    }*/

    public void Sexo()
    {
        target = posInicial;
        volta = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Roamming")
        {
            transform.position = other.gameObject.transform.position;
            vel = other.gameObject.GetComponent<DirecaoGhost>().vel[Random.Range(0, other.gameObject.GetComponent<DirecaoGhost>().vel.Length)];
        }
    }
}
