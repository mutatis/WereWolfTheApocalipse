﻿using UnityEngine;
using System.Collections;

public class TiroEnemy : MonoBehaviour
{
    public GameObject obj;

    public float velocityX, dmg, range;

    bool foi;

    void Start()
    {
        StartCoroutine("GO");
    }

    void Update()
    {
        if(obj != null && !foi)
        {
            if ((transform.position.x - obj.transform.position.x) > 0)
            {
                velocityX *= -1;
            }
            foi = true;
        }
        transform.Translate(new Vector3(velocityX, 0, 0));
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Dano(dmg, gameObject);
            Destroy(gameObject);
        }
    }
}