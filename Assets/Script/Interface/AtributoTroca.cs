using UnityEngine;
using System.Collections;

public class AtributoTroca : MonoBehaviour
{
    public GameObject[] desliga;

    public GameObject proximo;
    public GameObject anterior;

	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Joystick1Button11))
        {
            Proximo();
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Joystick1Button10))
        {
            Anterior();
        }
	}

    void Proximo()
    {
        for(int i = 0; i < desliga.Length; i++)
        {
            desliga[i].SetActive(false);
        }

        proximo.SetActive(true);
    }

    void Anterior()
    {
        for (int i = 0; i < desliga.Length; i++)
        {
            desliga[i].SetActive(false);
        }

        anterior.SetActive(true);
    }
}
