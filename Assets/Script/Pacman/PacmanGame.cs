using UnityEngine;
using System.Collections;

public class PacmanGame : MonoBehaviour
{
    public SpriteRenderer sprite;

    public Transform pos;

    public float velocity;

    float x, z;

    bool up, down, right, left;

	void Update ()
    {
        transform.Translate(x, 0, z, Space.World);

        if (Input.GetAxisRaw("DpadXP1") > 0)
        {
            pos.transform.eulerAngles = new Vector3(90, 0, 0);
            sprite.flipX = false;
            right = true;
            left = false;
            up = false;
            down = false;
        }
        else if(Input.GetAxisRaw("DpadXP1") < 0)
        {
            pos.transform.eulerAngles = new Vector3(90, 0, 0);
            sprite.flipX = true;
            right = false;
            left = true;
            up = false;
            down = false;
        }
        else if (Input.GetAxisRaw("DpadYP1") < 0)
        {
            pos.transform.eulerAngles = new Vector3(90, 90, 0);
            sprite.flipX = false;
            right = false;
            left = false;
            up = false;
            down = true;
        }
        else if (Input.GetAxisRaw("DpadYP1") > 0)
        {
            pos.transform.eulerAngles = new Vector3(90, -90, 0);
            sprite.flipX = false;
            right = false;
            left = false;
            up = true;
            down = false;
        }

        if(right)
        {
            x = velocity;
            z = 0;
        }
        else if(left)
        {
            x = velocity * -1;
            z = 0;
        }
        else if(up)
        {
            z = velocity;
            x = 0;
        }
        else if(down)
        {
            z = velocity * -1;
            x = 0;
        }
    }
}
