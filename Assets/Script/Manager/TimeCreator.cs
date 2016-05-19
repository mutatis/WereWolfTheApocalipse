using UnityEngine;
using System.Collections;

public class TimeCreator : MonoBehaviour
{
    public ControllerCamera camera;

    public SpriteRowCreator[] creator;

    public Vector2 quant;

    int num;

    int x;

	void Start ()
    {
        num = Random.Range((int)quant.x, (int)quant.y);
        FollowTarget.follow.quant = num;
        StartCoroutine("GO");
	}

    IEnumerator GO()
    {
        yield return new WaitForSeconds(4);
        x++;
        if (x > num)
        {
            camera.GG();
            for (int i = 0; i < creator.Length; i++)
            {
                creator[i].enabled = false;
                StopCoroutine("GO");
            }
        }
        else
        {
            for (int i = 0; i < creator.Length; i++)
            {
                if (x <= num)
                {
                    creator[i].CreateSprites();
                }
            }
            Denovo();
        }
    }

    void Denovo()
    {
        StopCoroutine("GO");
        StartCoroutine("GO");
    }
}