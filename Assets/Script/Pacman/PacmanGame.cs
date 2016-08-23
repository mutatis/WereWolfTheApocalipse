﻿using UnityEngine;
using System.Collections;

public class PacmanGame : MonoBehaviour
{
    public SpriteRenderer sprite;

    public Animator anim;

    public Transform pos;

    public float velocity;

    FMOD.Studio.EventInstance pacmanTap;

    [FMODUnity.EventRef]
    public string tap, cheiro, morreu;

    public GameObject[] obj;

    GameObject enemy;

    float x, z;

    int cheirada;

    public bool toca;

    bool up, down, right, left, duro, dead, cheirando;

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

        if (Input.GetAxisRaw("DpadXP1") > 0 && !right)
        {
            toca = true;
            PlaySound();
            pos.transform.eulerAngles = new Vector3(0, 0, 0);
            sprite.flipX = false;
            right = true;
            left = false;
            up = false;
            down = false;
        }
        else if(Input.GetAxisRaw("DpadXP1") < 0 && !left)
        {
            toca = true;
            PlaySound();
            pos.transform.eulerAngles = new Vector3(0, 0, 0);
            sprite.flipX = true;
            right = false;
            left = true;
            up = false;
            down = false;
        }
        else if (Input.GetAxisRaw("DpadYP1") < 0 && !down)
        {
            toca = true;
            PlaySound();
            pos.transform.eulerAngles = new Vector3(0, 0, -90);
            sprite.flipX = false;
            right = false;
            left = false;
            up = false;
            down = true;
        }
        else if (Input.GetAxisRaw("DpadYP1") > 0 && !up)
        {
            toca = true;
            PlaySound();
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

        if(cheirando)
        {
            enemy.GetComponent<Mulher>().transform.position = new Vector3(transform.position.x + 1, transform.position.y + 0.5f, transform.position.z);
            enemy.GetComponent<Mulher>().vel = new Vector2(0, 0);
        }
    }

    void PlaySound()
    {
        if (toca)
        {
            pacmanTap = FMODUnity.RuntimeManager.CreateInstance(tap);
            pacmanTap.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            pacmanTap.start();
            toca = false;
        }
    }

    public void Gozo()
    {
        enemy.GetComponent<Mulher>().sprite.enabled = true;
        enemy.gameObject.GetComponent<Mulher>().Sexo();
    }

    void Cheirando()
    {
        anim.SetInteger("Fantasma", enemy.GetComponent<Mulher>().tipo);
        anim.SetTrigger("Comeu");
        enemy.GetComponent<Mulher>().sprite.enabled = false;
    }

    IEnumerator Excitado()
    {
        for(int i = 0; i < obj.Length; i++)
        {
            obj[i].GetComponent<Mulher>().anim.SetBool("Sexo", true);
        }
        yield return new WaitForSeconds(12);
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].GetComponent<Mulher>().anim.SetTrigger("Acabando");
        }
        yield return new WaitForSeconds(3);
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].GetComponent<Mulher>().anim.SetBool("Sexo", false);
        }
        duro = false;
        anim.SetBool("Woow", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Pozinho")
        {
            pacmanTap = FMODUnity.RuntimeManager.CreateInstance(cheiro);
            pacmanTap.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
            pacmanTap.start();
            anim.SetTrigger("Cheiradinha");
            cheirada++;
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "Viagra")
        {
            anim.SetBool("Woow", true);
            anim.SetTrigger("Locao");
            duro = true;
            StopCoroutine("Excitado");
            StartCoroutine("Excitado");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Fantasma")
        {
            if (duro)
            {
                enemy = other.gameObject;
                Cheirando();
            }
            else
            {
                pacmanTap = FMODUnity.RuntimeManager.CreateInstance(morreu);
                pacmanTap.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
                pacmanTap.start();
                pos.transform.eulerAngles = new Vector3(0, 0, 0);
                sprite.flipX = false;
                other.gameObject.GetComponent<Mulher>().transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);
                for (int i = 0; i < obj.Length; i++)
                {
                    obj[i].GetComponent<Mulher>().vel = new Vector2(0, 0);
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