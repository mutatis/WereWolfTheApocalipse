using UnityEngine;
using System.Collections;

public class TimeCreator : MonoBehaviour
{

    public SpriteRowCreator creator;

	void Start ()
    {
        StartCoroutine("GO");
	}

    IEnumerator GO()
    {
        yield return new WaitForSeconds(4);
        creator.CreateSprites();
        Denovo();
    }

    void Denovo()
    {
        StopCoroutine("GO");
        StartCoroutine("GO");
    }
}
