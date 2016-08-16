using UnityEngine;
using System.Collections;
using UnityEditor;

public class PacmanGame : MonoBehaviour
{
    public SpriteRenderer sprite;

    public Animator anim;

    public Transform pos;

    public float velocity;

    float x, z;

    int cheirada;

    bool up, down, right, left, duro;

    void Update ()
    {
        transform.Translate(x, z, 0, Space.World);

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
            pos.transform.eulerAngles = new Vector3(0, 0, 90);
            sprite.flipX = false;
            right = false;
            left = false;
            up = false;
            down = true;
        }
        else if (Input.GetAxisRaw("DpadYP1") > 0)
        {
            pos.transform.eulerAngles = new Vector3(0, 0, -90);
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
        yield return new WaitForSeconds(5);
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
            sprite.color = Color.black;
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
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {

    }

}   