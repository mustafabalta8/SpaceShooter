using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{   //configuration parameters
    [Header("Player")]
    [SerializeField] float MoveSpeed=12f;
    [SerializeField] int health = 3;
    [SerializeField] [Range(0,1)] float playerDeathSoundVolume = 1f;
    [SerializeField] AudioClip PlayerDeath;

    [Header("Projectile")]
    [SerializeField] GameObject PlayerLaser;
    [SerializeField] float LaserVelocity;
    [SerializeField] float projectileFiringPeriod;

    [SerializeField] AudioClip LaserSFX;
    [SerializeField] [Range(0, 1)] float playerLaserSoundVolume = 0.9f;

    AudioSource audioSource;
    Coroutine firingCoroutine;
    float xMin, xMax,yMin,yMax,padding=0.5f;

    
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundiries();

        // firingCoroutine = StartCoroutine(ContiniousFire());
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        slowTime();
    }
    
    void Move()
    {
        var DeltaX = Input.GetAxis("Horizontal")*Time.deltaTime*MoveSpeed;
        var NewPosX = transform.position.x + DeltaX;
        var DeltaY = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed;
        var NewPosY = transform.position.y + DeltaY;

        transform.position = new Vector2(Mathf.Clamp(NewPosX,xMin,xMax),Mathf.Clamp(NewPosY,yMin,yMax));

    }
    void SetUpMoveBoundiries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Fire()
    {
       /* if (Input.GetButtonDown("Fire1"))
        {
            GameObject Laser = Instantiate(PlayerLaser, transform.position, Quaternion.identity) as GameObject;
            Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, LaserVelocity);
        }*/
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(ContiniousFire());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator ContiniousFire()
    {
        while (true)
        {
            GameObject Laser = Instantiate(PlayerLaser, transform.position, Quaternion.identity) as GameObject;
            Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, LaserVelocity);
            //Destroy(Laser, 1f);

            audioSource.PlayOneShot(LaserSFX, playerLaserSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        HitProcess(other);
    }

    private void HitProcess(Collider2D other)
    {

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }  //to protect the code from null references; in case there is no damage dealer
        health -= damageDealer.GetDamage();
        FindObjectOfType<HealthUI>().DisplayHealth();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }
    public int GetHealth()
    {
        return health;
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(PlayerDeath, Camera.main.transform.position, playerDeathSoundVolume);
        Destroy(gameObject);
       // Invoke("GameOverLoadProcess", 2f);
        FindObjectOfType<SceneControl>().LoadGameOver();
    }
    void slowTime()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Time.timeScale = 0.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Time.timeScale = 1f;
        }
    }

}
