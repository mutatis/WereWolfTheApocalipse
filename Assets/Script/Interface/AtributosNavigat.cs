using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AtributosNavigat : MonoBehaviour
{
    public GameObject[] img;

    public GameObject[] description;

    public SelectPersonagem select;

    bool podeDpad = true;

    int x = 0;

    void Update()
    {
        if (podeDpad)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("DpadYP1") < 0)
            {
                if (x < (img.Length - 1))
                    x++;

                for (int i = 0; i < img.Length; i++)
                {
                    if (i == x)
                    {
                        img[i].SetActive(true);
                    }
                    else
                    {
                        img[i].SetActive(false);
                    }
                }
                for (int i = 0; i < description.Length; i++)
                {
                    if (i == x)
                    {
                        description[i].SetActive(true);
                    }
                    else
                    {
                        description[i].SetActive(false);
                    }
                }
                podeDpad = false;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("DpadYP1") > 0)
            {
                if (x > 0)
                    x--;

                for (int i = 0; i < img.Length; i++)
                {
                    if (i == x)
                    {
                        img[i].SetActive(true);
                    }
                    else
                    {
                        img[i].SetActive(false);
                    }
                }
                for (int i = 0; i < description.Length; i++)
                {
                    if (i == x)
                    {
                        description[i].SetActive(true);
                    }
                    else
                    {
                        description[i].SetActive(false);
                    }
                }
                podeDpad = false;                
            }
        }

        if (Input.GetAxisRaw("DpadYP1") == 0)
        {
            podeDpad = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            x = 0;
        }
    }
}
