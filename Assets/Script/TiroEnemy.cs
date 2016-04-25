using UnityEngine;
using System.Collections;

public class TiroEnemy : MonoBehaviour
{
    public GameObject obj;

    public float velocityX, dmg, range;

    void Start()
    {
        if((transform.position.x - obj.transform.position.x) > 0)
        {
            velocityX *= -1;
        }
        StartCoroutine("GO");
    }

    void Update()
    {
        transform.Translate(new Vector3(velocityX, 0, 0));
    }

    IEnumerator GO()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}