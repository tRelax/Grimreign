using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullestScript : MonoBehaviour
{
    Rigidbody2D rigidBody;
    GameObject player;
    Vector2 moveDirection;
    public float moveSpeed = 5f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        moveDirection = (player.transform.position - transform.position).normalized * moveSpeed;
        rigidBody.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            player.GetComponent<playerMovements>().currentPlayerHealth -= 1;
        }
        else if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
