using UnityEngine;
using System.Collections;
using UnityEditor;

public class Mulher : MonoBehaviour
{
    public GameObject pacman, posInicial;

    public Animator anim;

    public SpriteRenderer sprite;

    public Vector2 vel;

    public int tipo;

    GameObject target;

    Vector3 direction;

    Vector2 velInicial;

    bool roamming = true, volta;

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
        transform.position = posInicial.transform.position;
        vel = velInicial;
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
