using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public static FollowTarget follow;

    public GameObject target; // Reference to the player.

	public Vector3 offset;   // The offset at which the Health Bar follows the player.

    public GameObject[] obj;

	public Transform[] maxMin;

    public float smoothTime = 0.3f; //Makes this behaviour smooth
    public float[] pos;
    
    public int cont;
    public int quant;

    public bool segue;

    float num;

    int enemy;

    private float xPosition; //wanted X position
    private float yPosition; //wanted Y position

    private Vector3 velocity = Vector3.zero; //A reference value used by SmoothDamp that tracks this object velocity
	
    void Awake()
    {
        follow = this;
    }

	void FixedUpdate ()
	{
		if (Manager.manager.player.Length > 0) 
		{
			target = Manager.manager.player [0];
		}
        
		if (segue) 
		{
			transform.Translate (target.GetComponent<PlayerMovment> ().x, 0, 0);
		}

		if (target != null) 
		{
			if (target.GetComponent<PlayerMovment> ().x > 0 && transform.position.x < 7.3f && target.transform.position.x >= maxMin [0].position.x ||
			    target.GetComponent<PlayerMovment> ().x < 0 && transform.position.x > -7.3f && target.transform.position.x <= maxMin [1].position.x) 
			{
				segue = true;
			}	

			if (target.GetComponent<PlayerMovment> ().x == 0 || 
				target.GetComponent<PlayerMovment> ().x > 0 && transform.position.x > 7.3f ||
			    target.GetComponent<PlayerMovment> ().x < 0 && transform.position.x < -7.3f) 
			{
				segue = false;
			}
		}

        /*if (target != null)
        {
            if (transform.position.y < 4.1f && target.GetComponent<PlayerMovment>().z > 0 && target.transform.position.z <= 3.76f)
            {
                transform.Translate(0, target.GetComponent<PlayerMovment>().z / 17, 0);
            }
            else if (transform.position.y > 1.7f && target.GetComponent<PlayerMovment>().z < 0 && target.transform.position.z > -11.6f)
            {
                transform.Translate(0, target.GetComponent<PlayerMovment>().z / 17, 0);
            }
        }*/

	}
}