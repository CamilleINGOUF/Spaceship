using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    private Transform target;
    public float speed;

    public AudioClip[] explosionClips;
    private AudioSource source;

    private Animator anim;
    private bool isDead = false;

	// Use this for initialization
	void Start ()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float step = speed * Time.deltaTime;
        if (!isDead && GameManager.gameAlive)
        {
            float z = Mathf.Atan2((target.position.y - transform.position.y), target.position.x - transform.position.x) * Mathf.Rad2Deg - 90;
            transform.eulerAngles = new Vector3(0, 0, z);


            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            anim.SetFloat("Speed", 1);
        }
        else
            anim.SetFloat("Speed", 0);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Projectil") || collider.CompareTag("Player"))
        {
            Destroy(GetComponent<BoxCollider2D>());
            anim.SetFloat("Speed", 0);
            isDead = true;
            anim.SetTrigger("Die");
            //SoundManagerScript.instance.RandomizeSfx(explosionClips);
            int indexclip = Random.Range(0, explosionClips.Length);
            source.PlayOneShot(explosionClips[indexclip], 1F);
            Invoke("destroy", 1f);
        }
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
