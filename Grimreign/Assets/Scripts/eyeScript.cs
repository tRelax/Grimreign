using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeScript : MonoBehaviour
{
    Vector3 directionToPlayer;
    GameObject player;
    Animator animator;
    AudioSource audioSource;
    float fireRate = 2f, nextFire;
    bool isAttacking = false;
    public float moveSpeed = 0.5f, distance;
    public static float eyeHeight = 0.15f;
    public int minDistance, currentHealth = 1;
    public GameObject bullet;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        audioSource.Stop();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        eyeHeight = GetComponent<Transform>().position.y;
        nextFire = Time.time;
    }

    private void Update()
    {
        if (currentHealth != 1)
        {
            animator.SetBool("isDead", true);
        }
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        distance = Vector3.Distance(GetComponent<Transform>().position, player.transform.position);

        if (distance > 4.0f) {
            animator.SetBool("InRange", false);
            directionToPlayer = (player.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity =
                new Vector2(directionToPlayer.x, directionToPlayer.y) * moveSpeed;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            isAttacking = true;
            animator.SetBool("InRange", true);
            CheckIfTimeToFire();
        }

        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            audioSource.Play();
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

    public void DestroyOnDeath()
    {
        Destroy(gameObject);
    }
}
