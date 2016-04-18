using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target; // Reference to the player.

	public Vector3 offset;   // The offset at which the Health Bar follows the player.

    public GameObject[] obj;

    public float smoothTime = 0.3f; //Makes this behaviour smooth
    public float[] pos;
    
    public int cont;

    public bool segue;

    float num;
    private float xPosition; //wanted X position
    private float yPosition; //wanted Y position

    private Vector3 velocity = Vector3.zero; //A reference value used by SmoothDamp that tracks this object velocity
	
	void FixedUpdate ()
	{
        num = transform.position.x;
        if(num >= pos[cont])
        {
            segue = false;
            obj[cont].SetActive(true);
        }

        if (segue)
        {
            xPosition = target.position.x + offset.x;
            yPosition = offset.y;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xPosition, transform.position.y, transform.position.z), ref velocity, smoothTime);
        }
	}
}