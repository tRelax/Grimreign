using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    Vector3 directionToPlayer;
    GameObject player;
    Animator animator;
    AudioSource audioSource;
    float fireRate = 2f, nextFire;
    bool isAttacking = false;
    public float moveSpeed = 0.5f;
    public int currentHealth = 5, previousHealth = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        previousHealth = currentHealth;
    }
    void Start()
    {
        audioSource.Stop();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        nextFire = Time.time;
    }

    private void Update()
    {
        if(currentHealth < 1)
        {
            animator.SetBool("isDead", true);
        }
        else if(currentHealth < previousHealth)
        {
            isAttacking = false;
            animator.SetBool("isHit", true);
            previousHealth = currentHealth;
        }
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        directionToPlayer = (player.transform.position - transform.position).normalized;
        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        GetComponent<Rigidbody2D>().velocity = 
            new Vector2 (directionToPlayer.x, directionToPlayer.y) * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CheckIfTimeToFire();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isAttacking = false;
            animator.SetBool("InRange", false);
        }
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            isAttacking = true;
            animator.SetBool("InRange", true);
            nextFire = Time.time + fireRate;
        }
    }

    public void AnimationFinished()
    {
        if (isAttacking)
        {
            player.GetComponent<playerMovements>().currentPlayerHealth -= 1;
            audioSource.Play();
        }
    }

    public void deathAnimationFinished()
    {
        animator.SetBool("isHit", false);
    }

    public void DestroyOnDeath()
    {
        Destroy(gameObject);
    }
}