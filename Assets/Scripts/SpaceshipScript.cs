using UnityEngine;
using UnityEngine.UI;

public class SpaceshipScript : MonoBehaviour {

    public float speed;
    public float orientationSpeed;
    public GameObject projectile;
    public GameObject[] availableEnemies;
    public float minSpawnEnemyRange;
    public float maxSpawnEnemyRange;

    public AudioClip[] laserClips;
    public AudioClip explosion;

    private AudioSource source;
    private Text livesDisplay;
    private GameObject deadDisplay;

    public int lifePoints = 3;

    private Animator anim;
    private Rigidbody2D rg2d;

    private int width;

    void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rg2d = GetComponent<Rigidbody2D>();
        InvokeRepeating("spawnRandomEnemy", 0f, 0.75f);

        deadDisplay = GameObject.Find("DeadDisplay");
        livesDisplay = GameObject.Find("LiveDisplay").GetComponent<Text>();
        deadDisplay.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!GameManager.gameAlive) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rotationSpaceship = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = rotationSpaceship;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        float verticalInput = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", Mathf.Abs(verticalInput));
        rg2d.AddForce(gameObject.transform.up * speed * verticalInput);

        if (transform.position.x <= -16f)
        {
            transform.position = new Vector2(-16f, transform.position.y);
        }
        else if (transform.position.x >= 16f)
        {
            transform.position = new Vector2(16f, transform.position.y);
        }

        if (transform.position.y <= -8.5f)
        {
            transform.position = new Vector2(transform.position.x, -8.5f);
        }
        else if (transform.position.y >= 8.5f)
        {
            transform.position = new Vector2(transform.position.x, 8.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Enemy"))
        {
            lifePoints--;
            if(lifePoints < 0)
            {
                Die();
            }else
            livesDisplay.text = "Lives : " + lifePoints;
        }
    }

    void Update()
    {
        if (!GameManager.gameAlive) return;
        if (Input.GetButtonDown("Fire1"))
            fire();
        
    }

    void fire()
    {
        projectile.transform.position = transform.FindChild("Canon").transform.position;
        projectile.transform.eulerAngles = transform.FindChild("Canon").transform.eulerAngles;
        Instantiate(projectile);
        int indexclip = Random.Range(0, laserClips.Length);
        source.PlayOneShot(laserClips[indexclip], 0.5F);
    }

    void spawnRandomEnemy()
    {
        if (!GameManager.gameAlive) return;
        float posX;
        float posY;

        do
        {
            posX = Random.Range(minSpawnEnemyRange, maxSpawnEnemyRange);
            posY = Random.Range(minSpawnEnemyRange, maxSpawnEnemyRange);
        } while ((posX < 5 && posX >-5) || (posY < 5 && posY > -5));

        int indexEnemy = Random.Range(0, availableEnemies.Length);
        GameObject enemy = (GameObject)Instantiate(availableEnemies[indexEnemy]);
        enemy.transform.position = new Vector3(transform.position.x + posX, transform.position.y + posY, 0);
    }

    void Die()
    {
        CancelInvoke("spawnRandomEnemy");
        deadDisplay.SetActive(true);
        anim.SetTrigger("Die");
        Invoke("hide", 4f);
        source.PlayOneShot(explosion, 1F);
        GameManager.gameAlive = false;
        gameObject.transform.FindChild("Canon").gameObject.SetActive(false);
    }

    private void hide()
    {
        gameObject.SetActive(false);
    }
}
