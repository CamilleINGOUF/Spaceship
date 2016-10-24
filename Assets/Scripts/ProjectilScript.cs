using UnityEngine;
using UnityEngine.UI;

public class ProjectilScript : MonoBehaviour {

    public float speed;
    private Text ScoreText;

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
            GameManager.score++;
            Destroy(gameObject);
        }
    }
}
