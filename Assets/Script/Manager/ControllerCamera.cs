using UnityEngine;
using System.Collections;

public class ControllerCamera : MonoBehaviour
{
    public FollowTarget camera;

    bool pode;

    public void GG()
    {
        if (!pode)
        {
            camera.cont++;
            camera.segue = true;
            pode = true;
        }
    }
}
