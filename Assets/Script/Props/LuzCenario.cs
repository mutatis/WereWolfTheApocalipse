using UnityEngine;
using System.Collections;

public class LuzCenario : MonoBehaviour
{
    public Vector2 minMax;

    public Animator anim;

    void Start()
    {
        StartCoroutine("Pisca");
    }

    IEnumerator Pisca()
    {
        yield return new WaitForSeconds(Random.Range(minMax.x, minMax.y));
        anim.SetTrigger("Pisca");
        Volta();
    }

    void Volta()
    {
        StopCoroutine("Pisca");
        StartCoroutine("Pisca");
    }
}
