using UnityEngine;
using System.Collections;

public class GranitePresas : MonoBehaviour
{
    public float vel, tempopara, tempomorre;

    bool para = true;

    void Start()
    {
        StartCoroutine("GO");
    }

    void Update()
    {
        if(para)
        transform.Translate(0, vel, 0);
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(tempopara);
        para = false;
        yield return new WaitForSeconds(tempomorre);
        Sai();
    }

    public void Sai()
    {
        Destroy(gameObject);
    }
}
