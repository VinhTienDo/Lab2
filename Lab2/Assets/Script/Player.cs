using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Rigidbody2D rigidbody2D;
    private float xInput;
    public float moveSpeed;
    public float jumpForce;
    public Animator anim;
    [SerializeField] private bool isRun;
    

    private int facingDirection = 1;
    private bool facingRight = true;

    private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;

    public Text Diem;
    public Text Mau;
    int score = 0;
    int heart = 0;



    private void OnTriggerEnter2D(Collider2D other)
    { 

        Debug.Log("va cham vao: " + other.gameObject.tag);

        if (other.gameObject.tag == "coin")
        {
            score++;
            Destroy(other.gameObject);
            Diem.text = "Score: " + score.ToString();
        }
        else if (other.gameObject.tag == "heart")
        {
            heart++;
            Destroy(other.gameObject);
            Mau.text = "x " + heart.ToString();
            
        }
        else if (other.gameObject.tag == "Mushroom")
        {
            heart--;
            Mau.text = "x " + heart.ToString();
            if (heart <= 0)
            {
                Application.LoadLevel("Level1");
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("va cham vao: " + other.gameObject.tag);
        if (other.gameObject.tag == "Mushroom")
        {
            
        }
        else if (other.gameObject.tag == "door")
        {
            SceneManager.LoadScene("Level1");
        }
        
        
    }


    


    void Start()
    {

        Diem = GameObject.Find("Diem").GetComponent<Text>();
        Mau = GameObject.Find("Mau").GetComponent<Text>();


    }


    void Update()
    {
        Movement();
        CheckInput();
        FlipController();
        AnimatorController();
        CollisionCheck();

    }

    private void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Movement()
    {
        rigidbody2D.velocity = new Vector2(xInput * moveSpeed, rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        }
        
    }

    private void AnimatorController()
    {
        if (rigidbody2D.velocity.x != 0)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        anim.SetFloat("yVelocity", rigidbody2D.velocity.y);
        anim.SetBool("isRun", isRun);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (rigidbody2D.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rigidbody2D.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }


}
