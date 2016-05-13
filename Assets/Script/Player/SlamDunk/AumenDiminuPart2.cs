using UnityEngine;
using System.Collections;

public class AumenDiminuPart2 : MonoBehaviour
{
    public GameObject obj;
  
    public float maxSli;
    
    int selecionado;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick2Button0) && transform.localScale.x < maxSli)
        {
            Manager.manager.player[1].AddComponent<PlayerGoSlamDunk>();
            Manager.manager.player[1].GetComponent<PlayerGoSlamDunk>().selecionado = selecionado;
            Manager.manager.player[1].GetComponent<PlayerGoSlamDunk>().obj = Manager.manager.enemy[selecionado];
            Destroy(obj);
        }
        if (transform.localScale.x <= 0.05f)
        {
            Manager.manager.player[1].AddComponent<PlayerGoSlamDunk>();
            Manager.manager.player[1].GetComponent<PlayerGoSlamDunk>().selecionado = selecionado;
            Manager.manager.player[1].GetComponent<PlayerGoSlamDunk>().obj = Manager.manager.enemy[selecionado];
            Destroy(obj);
        }

        transform.localScale = new Vector3(transform.localScale.x - 0.004f, transform.localScale.y - 0.004f, transform.localScale.z - 0.004f);
    }
}