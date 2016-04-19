using UnityEngine;
using System.Collections;

public class TimeCreator : MonoBehaviour
{
    public ControllerCamera camera;

    public SpriteRowCreator creator;

    public Vector2 quant;

    int num;

    int x;

	void Start ()
    {
        num = Random.Range((int)quant.x, (int)quant.y);
        StartCoroutine("GO");
	}

    IEnumerator GO()
    {
        yield return new WaitForSeconds(4);
        if (x > num)
        {
            camera.GG();
            creator.enabled = false;
            StopCoroutine("GO");
        }
        else
        {
            x++;
            creator.CreateSprites();
            Denovo();
        }
    }

    void Denovo()
    {
        StopCoroutine("GO");
        StartCoroutine("GO");
    }
}