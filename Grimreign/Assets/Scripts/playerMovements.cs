using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerMovements : MonoBehaviour
{
    int previousPlayerHealth = 5;
    bool isAttacking = false, isPaused = false;
    Image currentHeartPosition;
    RectTransform currentHeartTransform;
    Animator animator;
    AudioSource audioSource;
    GameObject tempEnemy;
    public int maxPlayerhealth = 5, currentPlayerHealth = 5;
    public float moveSpeed = 5f, moveValue;
    public Image deadHeart;
    public Image[] hearts = new Image[5];
    public Image[] heartsBackup = new Image[5];
    public Collider2D groundCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (previousPlayerHealth > currentPlayerHealth && currentPlayerHealth > 0)
        {
            currentHeartTransform = hearts[currentPlayerHealth].rectTransform;
            currentHeartPosition = hearts[currentPlayerHealth];
            hearts[currentPlayerHealth] = Instantiate(deadHeart);
            Destroy(currentHeartPosition);
            hearts[currentPlayerHealth].transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            hearts[currentPlayerHealth].rectTransform.localPosition = currentHeartTransform.localPosition;
            
            previousPlayerHealth = currentPlayerHealth;
        }

        if (currentPlayerHealth > 0)
        {
            Jump();
            Attack();
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed;
            animator.SetFloat("moveValue", Mathf.Abs(Input.GetAxis("Horizontal")));

            if (Input.GetAxis("Horizontal") > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

        }
        else
        {
            animator.SetBool("isDead", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            animator.SetBool("isJump", false);
        }
        else if (collision.gameObject.tag == "enemy")
        {
            isAttacking = true;
            tempEnemy = collision.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tempEnemy = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            isAttacking = false;
            tempEnemy = null;
        }
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && GetComponent<Collider2D>().IsTouching(groundCollider))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
        }
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isAttack", true);
            if(tempEnemy != null)
            {
                isAttacking = true;
            }
        }
        else
        {
            animator.SetBool("isAttack", false);
            isAttacking=false;
        }
    }

    public void attackAnimationFinished()
    {
        audioSource.Play();
        if (isAttacking)
        {
            if (tempEnemy.tag == "enemy")
            {
                tempEnemy.GetComponent<enemyScript>().currentHealth -= 1;
            }
            else if (tempEnemy.tag == "eye")
            {
                tempEnemy.GetComponent<eyeScript>().currentHealth -= 1;
            }
        }
    }

    private void loadSceneOnDeath()
    {
        SceneManager.LoadScene("gameOver");
    }
}
