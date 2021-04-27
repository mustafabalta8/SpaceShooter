using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 3;
     float shotCounter;
    [SerializeField] float minTimeBetweenShots = 1f, maxTimeBeteenShots = 3f;

    [SerializeField] GameObject EnemyProjectile;
    [SerializeField] float ProjectileVelocity;
    [SerializeField] GameObject ExplosionVFX;

     AudioSource audioSource;
    [SerializeField] AudioClip enemyLaserSFX;
    [SerializeField] AudioClip enemyDeathSFX;
    [SerializeField][Range(0,1)] float fireSoundVolume = 0.6f;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.8f;

    [SerializeField] int ScoreValue=175;

    GameManager gameManager;
    //ScoreUI scoreUI;
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBeteenShots);

        audioSource = GetComponent<AudioSource>();

        gameManager = FindObjectOfType<GameManager>();

      //  scoreUI = FindObjectOfType<ScoreUI>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShot();  
    }
    private void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBeteenShots);
        }
    }

    private void Fire()
    {
        GameObject EnemyLaser = Instantiate(EnemyProjectile, transform.position, Quaternion.identity) as GameObject;
        EnemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, ProjectileVelocity);

        audioSource.PlayOneShot(enemyLaserSFX,fireSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }  //to protect the code from null references; in case there is no damage dealer
            ProcessHit(damageDealer);
       
            
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject Explosion = Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        Destroy(Explosion, 0.5f);
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position, deathSoundVolume);
        gameManager.AddToScore(ScoreValue);

    }
}
