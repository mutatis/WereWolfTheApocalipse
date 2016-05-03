﻿using UnityEngine;
using System.Collections;

public class Pacman : MonoBehaviour
{
    public float dmg;

    public GameObject obj;

    [FMODUnity.EventRef]
    public string raio;

    FMOD.Studio.EventInstance audioInstance;

    public float vel;

    int contador, temp;

    void Start()
    {
        if(obj.GetComponent<PlayerController>().transform.localScale.x < 0)
        {
            vel *= -1;
        }
    }

    void Update()
    {
        if(contador == 0)
        transform.Translate(vel, 0, 0);
        
        if(temp == 2)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(1);
        transform.position = new Vector3(transform.position.x, obj.transform.position.y, obj.transform.position.z);
        vel *= -1;
        contador = 0;
    }

    void Dano(GameObject other)
    {
        audioInstance = FMODUnity.RuntimeManager.CreateInstance(raio);
        audioInstance.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        audioInstance.start();
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().Dano(dmg, false, gameObject);
        }
        else if (other.gameObject.tag == "EnemyRanged")
        {
            other.gameObject.GetComponent<EnemyRanged>().Dano(dmg, false, gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Parede3")
        {
            temp++;
            contador = 1;
            StartCoroutine("GO");
        }

        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<EnemyController>().life > 0 && other.gameObject.GetComponent<EnemyController>().dano)
            {
                Dano(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "EnemyRanged")
        {
            if (other.gameObject.GetComponent<EnemyRanged>().life > 0 && other.gameObject.GetComponent<EnemyRanged>().dano)
            {
                Dano(other.gameObject);
            }
        }
    }
}
