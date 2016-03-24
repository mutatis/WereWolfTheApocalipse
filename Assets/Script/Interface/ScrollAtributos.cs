using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollAtributos : MonoBehaviour
{
    public RectTransform content;

    public float valor;

    public int tipo;

    float x;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            x = content.position.y;
            content.position = new Vector3(content.position.x, valor, content.position.z);
        }
        if(tipo == 1 && valor > content.position.y)
        {
           
        }
    }
}
