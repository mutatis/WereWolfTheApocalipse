using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliAumenDimi : MonoBehaviour
{
    public Slider sli;

    public float maxSli, temp, x;

    void Start()
    {
        maxSli = sli.maxValue;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Joystick1Button0) && temp >= 0.45f && temp <= 0.65f)
        {
            sli.gameObject.SetActive(false);
        }

        if(x == 1)
        {
            temp -= 0.05f;
        }
        else
        {
            temp += 0.05f;
        }

        sli.value = temp;

        if(temp >= maxSli)
        {
            x = 1;
        }
        else if(temp <= 0)
        {
            x = 0;
        }
    }
}