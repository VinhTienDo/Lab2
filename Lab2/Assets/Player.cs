using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("va cham vao: " + other.gameObject.tag);

        if (other.gameObject.tag == "coin")
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("va cham vao: " + other.gameObject.tag);
        if (other.gameObject.tag == "Brick")
        {
            isGrounded = true;
            animator.SetBool("isGround", true);
            animator.SetBool("isRun", false);
        }
        else if (other.gameObject.tag == "door")
        {
            SceneManager.LoadScene("Level1");
        }
    }


    Rigidbody2D rigidbody2D;
    Transform transform;
    SpriteRenderer spriteRenderer;
    bool isGrounded = false;
    Animator animator;


    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        /*transform = this.gameObject.GetComponent<Transform>();*/
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    float movePrefix = 3;
    int JumpCount = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rigidbody2D.AddForce(Vector2.up * movePrefix * 2, ForceMode2D.Impulse);
                isGrounded = false;

                animator.SetBool("isGround", false);
                animator.SetBool("isRun", false);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rigidbody2D.AddForce(Vector2.left * movePrefix, ForceMode2D.Impulse);
            /*transform.localScale = new Vector3(-1, 1, 1);*/
            spriteRenderer.flipX = true;

            animator.SetBool("isRun", true);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rigidbody2D.AddForce(Vector2.right * movePrefix, ForceMode2D.Impulse);
            /*transform.localScale = new Vector3(1, 1, 1);*/
            spriteRenderer.flipX = false;

            animator.SetBool("isRun", true);
        }
    }
    

    private void setToIdle ()
    {
        StartCoroutine(wait(1f));

        animator.SetBool("isRun", false );
        animator.SetBool("isGround", true);
    }

    IEnumerator wait(float timeSeconds)
    {
        yield return new WaitForSeconds(timeSeconds);
    }
   
}
