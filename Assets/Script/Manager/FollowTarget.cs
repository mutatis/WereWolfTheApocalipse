using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public static FollowTarget follow;

    public GameObject target; // Reference to the player.

	public Vector3 offset;   // The offset at which the Health Bar follows the player.

    public GameObject[] obj;

    public float smoothTime = 0.3f; //Makes this behaviour smooth
    public float[] pos;
    
    public int cont;
    public int quant;

    public bool segue;

    float num;
    private float xPosition; //wanted X position
    private float yPosition; //wanted Y position

    private Vector3 velocity = Vector3.zero; //A reference value used by SmoothDamp that tracks this object velocity
	
    void Awake()
    {
        follow = this;
    }

	void FixedUpdate ()
	{
        target = Manager.manager.player[0];
        num = transform.position.x;
        if(num >= pos[cont])
        {
            segue = false;
            obj[cont].SetActive(true);
        }

        if (segue && quant < 0 && target.GetComponent<PlayerController>().x >= 0)
        {
            xPosition = target.transform.position.x + offset.x;
            yPosition = offset.y;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(xPosition, transform.position.y, transform.position.z), ref velocity, smoothTime);
        }
	}
}