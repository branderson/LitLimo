using UnityEngine;
using System.Collections;

public class LiquorStoreController : MonoBehaviour
{
    [SerializeField] private int framesToRespawn = 1800;
    private bool destroyed = false;
    private int destroySpeed = 30;
    private int respawnCounter = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
	    if (destroyed)
	    {
	        if (respawnCounter < framesToRespawn)
	        {
	            if (respawnCounter == framesToRespawn/2)
	            {
	                // Make liquor store partially rebuilt
	            }
	            respawnCounter += 1;
	        }
	        else
	        {
	            respawnCounter = 0;
	            destroyed = false;

                // Restore liquor store
	        }
	    }
	}

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !destroyed)
        {
            if (other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > destroySpeed)
            {
                other.gameObject.GetComponent<PlayerController>().DestroyLiquorStore(this);
                destroyed = true;

                // "Destroy" liquor store
            }
            else
            {
                // Fuck up the player
            }
        }
    }
}
