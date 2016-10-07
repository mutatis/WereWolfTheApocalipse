using UnityEngine;
using System.Collections;

public class SlamArea : MonoBehaviour
{
    public PlayerSlamArea player;

    public float x, z;

    void Update()
    {
        transform.Translate(x, 0, z);

        switch (player)
        {
            case PlayerSlamArea.Player1:
                x = Input.GetAxis("HorizontalP1");
                z = Input.GetAxis("VerticalP1");
                break;

            case PlayerSlamArea.Player2:
                x = Input.GetAxis("HorizontalP2");
                z = Input.GetAxis("VerticalP2");
                break;
        }
    }
}

public enum PlayerSlamArea
{
    Player1,
    Player2,
    Player3,
    Player4
}