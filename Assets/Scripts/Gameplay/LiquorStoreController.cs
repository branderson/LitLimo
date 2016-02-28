using UnityEngine;
using System.Collections;

public class LiquorStoreController : MonoBehaviour
{
    [SerializeField] private Sprite NormalSprite;
    [SerializeField] private Sprite DestroyedSprite;
    [SerializeField] private Sprite RebuildingSprite;
    [SerializeField] private int framesToRespawn = 1800;
    private bool destroyed = false;
    private int destroySpeed = 30;
    private int respawnCounter = 0;

    private SpriteRenderer renderer;
    private BoxCollider2D collider;

	// Use this for initialization
	void Start ()
	{
	    renderer = GetComponent<SpriteRenderer>();
	    renderer.sprite = NormalSprite;
	    collider = GetComponent<BoxCollider2D>();
	}
	
	void Update () {
	    if (destroyed)
	    {
	        if (respawnCounter < framesToRespawn)
	        {
	            if (respawnCounter == framesToRespawn/2)
	            {
	                // Make liquor store partially rebuilt
	                renderer.sprite = RebuildingSprite;
	            }
	            respawnCounter += 1;
	        }
	        else
	        {
	            respawnCounter = 0;
	            destroyed = false;

                // Restore liquor store
	            renderer.sprite = NormalSprite;
                collider.enabled = true;
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
                renderer.sprite = DestroyedSprite;
                collider.enabled = false;
            }
            else
            {
                // Fuck up the player
            }
        }
    }
}
