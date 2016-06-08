using UnityEngine;
using System.Collections;

public class SelectEnemySlam : MonoBehaviour
{
    public GameObject obj;

    float x;

    int selecionado;

    bool zero;

    void Start()
    {
        StartCoroutine("GO");
    }

    void Update()
    {
        if (Manager.manager.player[1].GetComponent<Rigidbody>().velocity.y < 0)
        {
            Manager.manager.player[1].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        x = Input.GetAxis("HorizontalP2");
        if (zero)
        {
            if (x < -0.1f)
            {
                Anterior();
            }
            else if (x > 0.1f)
            {
                Proximo();
            }
        }

        if (x == 0)
        {
            zero = true;
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            Aperto();
            /*Manager.manager.player[1].AddComponent<PlayerGoSlamDunk>();
            Manager.manager.player[1].GetComponent<PlayerGoSlamDunk>().selecionado = selecionado;
            Manager.manager.player[1].GetComponent<PlayerGoSlamDunk>().obj = Manager.manager.enemy[selecionado];
            Destroy(gameObject);*/
        }
        
    }

    void Aperto()
    {
        for (int i = 0; i < Manager.manager.enemy.Length; i++)
        {
            Manager.manager.enemy[i].GetComponent<EnemyController>().enabled = true;
        }
        obj.SetActive(true);
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(2);
        Aperto();
    }

    void Proximo()
    {
        zero = false;
        if (selecionado < Manager.manager.enemy.Length)
        {
            selecionado++;
        }
        else
        {
            selecionado = 0;
        }
        for (int i = 0; i < Manager.manager.enemy.Length; i++)
        {
            if (i == selecionado)
            {
                Manager.manager.enemy[selecionado].GetComponent<EnemyController>().seta.SetActive(true);
            }
            else
            {
                Manager.manager.enemy[i].GetComponent<EnemyController>().seta.SetActive(false);
            }
        }
    }

    void Anterior()
    {
        zero = false;
        if (selecionado > 0)
        {
            selecionado--;
        }
        else
        {
            selecionado = Manager.manager.enemy.Length;
        }
        for (int i = 0; i < Manager.manager.enemy.Length; i++)
        {
            if (i == selecionado)
            {
                Manager.manager.enemy[selecionado].GetComponent<EnemyController>().seta.SetActive(true);
            }
            else
            {
                Manager.manager.enemy[i].GetComponent<EnemyController>().seta.SetActive(false);
            }
        }
    }
}