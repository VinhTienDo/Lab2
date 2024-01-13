using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("va cham vao: " + collision.gameObject.tag);

        if (collision.gameObject.tag == "coin")
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("va cham vao: " + collision.gameObject.tag);
    }

    Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    int jumpCount = 1;
    float movePrefix = 3;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Key was pressed");
            jumpCount++;

            rigidbody2D.AddForce(Vector2.up * movePrefix, ForceMode2D.Impulse);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rigidbody2D.AddForce(Vector2.left * movePrefix, ForceMode2D.Impulse);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            rigidbody2D.AddForce(Vector2.right * movePrefix, ForceMode2D.Impulse);
        }
        
            
        
    }
}
