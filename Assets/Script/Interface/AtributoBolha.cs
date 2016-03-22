using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AtributoBolha : MonoBehaviour
{
    public Image[] img;

    public Sprite spt;

    public string nome;

    int x;

    void Update()
    {
        x = PlayerPrefs.GetInt(nome);

        for(int i = 0; i < x; i++)
        {
            img[i].sprite = spt;
        }
    }
}
