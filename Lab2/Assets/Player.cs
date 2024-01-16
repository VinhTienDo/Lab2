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
    int score = 0;

    //Transform transform;
    //SpriteRenderer spriteRenderer;
    //bool isGrounded = false;
    //Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    { 

        Debug.Log("va cham vao: " + other.gameObject.tag);

        if (other.gameObject.tag == "coin")
        {
            score++;
            Destroy(other.gameObject);
            Diem.text = "Score: " + score.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("va cham vao: " + other.gameObject.tag);
        if (other.gameObject.tag == "Brick")
        {
            //isGrounded = true;
            //animator.SetBool("isGround", true);
            //animator.SetBool("isRun", false);
        }
        else if (other.gameObject.tag == "door")
        {
            SceneManager.LoadScene("Level1");
        }
        
    }


    


    void Start()
    {

        Diem = GameObject.Find("Diem").GetComponent<Text>();

        //rigidbody2D = GetComponent<Rigidbody2D>();
        ///*transform = this.gameObject.GetComponent<Transform>();*/
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
    }

    //float movePrefix = 3;
    //int JumpCount = 1;

    void Update()
    {
        Movement();
        CheckInput();
        FlipController();
        AnimatorController();
        CollisionCheck();



        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (isGrounded)
        //    {
        //        rigidbody2D.AddForce(Vector2.up * movePrefix * 2, ForceMode2D.Impulse);
        //        isGrounded = false;

        //        animator.SetBool("isGround", false);
        //        animator.SetBool("isRun", false);
        //    }

        //}
        //else if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    rigidbody2D.AddForce(Vector2.left * movePrefix, ForceMode2D.Impulse);
        //    /*transform.localScale = new Vector3(-1, 1, 1);*/
        //    spriteRenderer.flipX = true;

        //    animator.SetBool("isRun", true);

        //}
        //else if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    rigidbody2D.AddForce(Vector2.right * movePrefix, ForceMode2D.Impulse);
        //    /*transform.localScale = new Vector3(1, 1, 1);*/
        //    spriteRenderer.flipX = false;

        //    animator.SetBool("isRun", true);
        //}
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

    


    //private void setToIdle ()
    //{
    //    StartCoroutine(wait(1f));

    //    animator.SetBool("isRun", false );
    //    animator.SetBool("isGround", true);
    //}

    //IEnumerator wait(float timeSeconds)
    //{
    //    yield return new WaitForSeconds(timeSeconds);
    //}

}
