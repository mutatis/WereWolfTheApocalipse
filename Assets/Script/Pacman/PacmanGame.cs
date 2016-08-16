using UnityEngine;
using System.Collections;
using UnityEditor;

public class PacmanGame : MonoBehaviour
{
    public SpriteRenderer sprite;

    public Animator anim;

    public Transform pos;

    public float velocity;

    public GameObject[] obj;

    float x, z;

    int cheirada;

    bool up, down, right, left, duro, dead;

    void Start()
    {
        obj = GameObject.FindGameObjectsWithTag("Fantasma");
    }

    void Update ()
    {
        if (!dead)
        {
            transform.Translate(x, z, 0, Space.World);
        }

        if (cheirada < ManagerCocaina.cocaina.pozinho.Length / 3)
        {
            anim.SetInteger("Cheiro", 1);
        }
        else if(cheirada < (ManagerCocaina.cocaina.pozinho.Length / 3) * 2)
        {
            anim.SetInteger("Cheiro", 2);
        }
        else
        {
            anim.SetInteger("Cheiro", 3);
        }

        if (Input.GetAxisRaw("DpadXP1") > 0)
        {
            pos.transform.eulerAngles = new Vector3(0, 0, 0);
            sprite.flipX = false;
            right = true;
            left = false;
            up = false;
            down = false;
        }
        else if(Input.GetAxisRaw("DpadXP1") < 0)
        {
            pos.transform.eulerAngles = new Vector3(0, 0, 0);
            sprite.flipX = true;
            right = false;
            left = true;
            up = false;
            down = false;
        }
        else if (Input.GetAxisRaw("DpadYP1") < 0)
        {
            pos.transform.eulerAngles = new Vector3(0, 0, -90);
            sprite.flipX = false;
            right = false;
            left = false;
            up = false;
            down = true;
        }
        else if (Input.GetAxisRaw("DpadYP1") > 0)
        {
            pos.transform.eulerAngles = new Vector3(0, 0, 90);
            sprite.flipX = false;
            right = false;
            left = false;
            up = true;
            down = false;
        }

        if(right)
        {
            x = velocity;
            z = 0;
        }
        else if(left)
        {
            x = velocity * -1;
            z = 0;
        }
        else if(up)
        {
            z = velocity;
            x = 0;
        }
        else if(down)
        {
            z = velocity * -1;
            x = 0;
        }
    }

    IEnumerator Excitado()
    {
        for(int i = 0; i < obj.Length; i++)
        {
            obj[i].GetComponent<Mulher>().anim.SetBool("Sexo", true);
        }
        yield return new WaitForSeconds(5);
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].GetComponent<Mulher>().anim.SetBool("Sexo", false);
        }
        duro = false;
        sprite.color = Color.white;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Pozinho")
        {
            anim.SetTrigger("Cheiradinha");
            cheirada++;
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Viagra")
        {
            sprite.color = Color.blue;
            duro = true;
            StartCoroutine("Excitado");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Fantasma")
        {
            if (duro)
            {
                other.gameObject.GetComponent<Mulher>().Sexo();
            }
            else
            {
                pos.transform.eulerAngles = new Vector3(0, 0, 0);
                sprite.flipX = false;
                for (int i = 0; i < obj.Length; i++)
                {
                    obj[i].GetComponent<Mulher>().vel = new Vector2(0, 0);
                    obj[i].GetComponent<Mulher>().transform.position = new Vector3(transform.position.x + 1, transform.position.y + 0.5f, transform.position.z);
                }
                dead = true;
                anim.SetTrigger("Dead");
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {

    }

}   