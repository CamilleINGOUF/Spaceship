using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public Transform target;
    public float speed;

    private Animator anim;
    private bool isDead = false;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float step = speed * Time.deltaTime;
        if (!isDead)
        {
            float z = Mathf.Atan2((target.position.y - transform.position.y), target.position.x - transform.position.x) * Mathf.Rad2Deg - 90;
            transform.eulerAngles = new Vector3(0, 0, z);


            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            anim.SetFloat("Speed", 1);
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Projectil"))
        {
            Destroy(GetComponent<BoxCollider2D>());
            anim.SetFloat("Speed", 0);
            isDead = true;
            anim.SetTrigger("Die");
            Invoke("destroy", 0.5f);
        }
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
