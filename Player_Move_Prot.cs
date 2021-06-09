using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move_Prot : MonoBehaviour
{
    //variabler
    public int playerSpeed = 10;
    private bool facingRight = false;
    public int playerJumpPower = 2000;
    private float moveX;
    public bool isGrounded;

    void Update()
    {
        PlayerMove();
        PlayerRaycast();
    }

    void PlayerMove()
    {
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }
        //PLAYER DIRECTION
        if (moveX < 0.0f && facingRight == false)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        //PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }

    //metoder

    void Jump()
    {
        //JUMPING CODE
        GetComponent<Rigidbody2D>().AddForce (Vector2.up * playerJumpPower);
        isGrounded = false;
    }
    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    void OnCollisionEnter2D (Collision2D col)
    {

    }
    void PlayerRaycast()
    {
        RaycastHit2D rayRight = Physics2D.Raycast(transform.position, Vector2.right);
        if (rayRight != null && rayRight.collider != null && rayRight.distance < 1.5f && rayRight.collider.name == "spike")
        {
            Destroy(rayRight.collider.gameObject);
        }

            RaycastHit2D rayDown = Physics2D.Raycast (transform.position, Vector2.down);
        if (rayDown != null && rayDown.collider != null && rayDown.distance < 1.5f && rayDown.collider.tag == "enemy")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            rayDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rayDown.collider.gameObject.GetComponent<EnemyMove>().enabled = false;
            
        }
        if (rayDown != null && rayDown.collider != null && rayDown.distance < 1.5f && rayDown.collider.tag != "enemy")
        {
            isGrounded = true;

        }
    }
}
