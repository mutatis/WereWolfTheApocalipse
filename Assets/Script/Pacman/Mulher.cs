using UnityEngine;
using System.Collections;
using UnityEditor;

public class Mulher : MonoBehaviour
{
    public GameObject pacman, posInicial;

    public NavMeshAgent agent;

    public Vector2 vel;

    GameObject target;

    Vector3 direction;

    Vector2 velInicial;

    public bool roamming = true, volta;

    void Start()
    {
        velInicial = vel;
    }

	void Update ()
    {
        if(volta)
        {
            if(Vector3.Distance(transform.position, posInicial.transform.position) > 1)
            {
                direction = posInicial.transform.position - transform.position;
                direction.Normalize();
                transform.Translate(direction, Space.World);
            }
            else
            {
                volta = false;
            }
        }
        else
        {
            transform.Translate(vel, Space.World);
        }
	}

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
