using UnityEngine;
using System.Collections;
using UnityEditor;

public class SpaceshipScript : MonoBehaviour {

    public float speed;
    public float orientationSpeed;
    public GameObject projectile;
    public GameObject[] availableEnemies;
    public float minSpawnEnemyRange;
    public float maxSpawnEnemyRange;

    private Animator anim;
    private Rigidbody2D rg2d;

    private int width;

    void Start()
    {
        anim = GetComponent<Animator>();
        rg2d = GetComponent<Rigidbody2D>();
        //string[] res = UnityStats.screenRes.Split('x');
        //Debug.Log(int.Parse(res[0]) + " " + int.Parse(res[1]));
        InvokeRepeating("spawnRandomEnemy", 0f, 2f);
        //spawnRandomEnemy();
    }

	void FixedUpdate()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rotationSpaceship = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = rotationSpaceship;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        // float rotation = transform.localEulerAngles.z + 10 * orientationSpeed * horizontalInput;
        //transform.localEulerAngles = new Vector3(0,0, rotation);

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

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            fire();
    }

    void fire()
    {
        projectile.transform.position = transform.FindChild("Canon").transform.position;
        projectile.transform.eulerAngles = transform.FindChild("Canon").transform.eulerAngles;
        Instantiate(projectile);
    }

    void spawnRandomEnemy()
    {
        float poxX = Random.Range(minSpawnEnemyRange, maxSpawnEnemyRange);
        float posY = Random.Range(minSpawnEnemyRange, maxSpawnEnemyRange);

        int indexEnemy = Random.Range(0, availableEnemies.Length);
        GameObject enemy = (GameObject)Instantiate(availableEnemies[indexEnemy]);
        enemy.transform.position = new Vector3(transform.position.x + poxX, transform.position.y + posY, 0);
        
    }
}
