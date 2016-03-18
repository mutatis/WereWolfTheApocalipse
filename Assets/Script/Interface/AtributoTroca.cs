using UnityEngine;
using System.Collections;

public class AtributoTroca : MonoBehaviour
{
    public GameObject[] desliga;

    public GameObject proximo;
    public GameObject anterior;

    bool podeDpad = true;

	void Update ()
    {
        if (podeDpad)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxisRaw("DpadXP1") > 0)
            {
                Proximo();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxisRaw("DpadXP1") < 0)
            {
                Anterior();
            }
        }

        if (Input.GetAxisRaw("DpadXP1") == 0)
        {
            podeDpad = true;
        }
    }

    void Proximo()
    {
        for(int i = 0; i < desliga.Length; i++)
        {
            desliga[i].SetActive(false);
        }
        podeDpad = false;
        proximo.SetActive(true);
    }

    void Anterior()
    {
        for (int i = 0; i < desliga.Length; i++)
        {
            desliga[i].SetActive(false);
        }
        podeDpad = false;
        anterior.SetActive(true);
    }
}
