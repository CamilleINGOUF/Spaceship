using UnityEngine;
using System.Collections;

public class ProjectilScript : MonoBehaviour {

    public float speed;

    private Rigidbody2D rg2d;
    private bool seen = false;

    // Use this for initialization
    void Start ()
    {
        rg2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        rg2d.AddForce(gameObject.transform.up * speed);

        if (GetComponent<Renderer>().isVisible)
            seen = true;

        if (seen && !GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
