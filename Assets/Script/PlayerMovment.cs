using UnityEngine;
using System.Collections;

public class PlayerMovment : MonoBehaviour
{
    public Player player;

    float x;
    float z;

    void Update()
    {
        switch (player)
        {
            case Player.Player1:
                x = Input.GetAxis("HorizontalP1");
                z = Input.GetAxis("VerticalP1");

                transform.Translate(x, 0, z);
            /*    if(Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(-0.1f, 0, 0);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(0.1f, 0, 0);
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(0, 0, 0.1f);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(0, 0, -0.1f);
                }*/
                break;
        }
    }
}

public enum Player
{
    Player1,
    Player2,
    Player3,
    Player4
}