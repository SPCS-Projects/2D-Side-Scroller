using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour {
    private Rigidbody rb;
    private float moveSpeed;
    private float jumpForce;
    private bool justJumped;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 300f;
        jumpForce = 10000f;
        justJumped = false;
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.AddForce(new Vector3(moveSpeed, 0, 0) * Input.GetAxis("Horizontal"));
        }
        if (Input.GetButtonDown("Jump") || Input.GetButton("Jump"))
        {
            if (justJumped == false)
            {
                rb.AddForce(new Vector3(0, (jumpForce * Random.Range(0.8f, 1.3f)), 0));
                //rb.AddForce(new Vector3(0, jumpForce, 0));
                justJumped = true;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        justJumped = false;
        if (collision.collider.gameObject.tag == "EnemyHead")
        {
            if (transform.position.y >= collision.collider.transform.position.y + 0.9f)
            {
                rb.AddForce(new Vector3(0, 1, 0) * jumpForce / 2);
                Destroy(collision.collider.transform.parent.gameObject);
                Destroy(collision.collider.gameObject);
                GameManager.score += 100;
                GameManager.Enemies += 1;
                if(GameManager.Enemies == 3)
                {
                    GameManager.win = true;
                }
            }
        }
        else if (collision.collider.gameObject.tag == "EnemyBody")
        {
            GameManager.lives -= 1;
            Debug.Log(GameManager.lives);
            if (GameManager.lives == 0)
            {
                GameManager.over = true;
                Time.timeScale = 0;
            }
            else
            {
                SceneManager.LoadScene("Level 1");
            }

        }
    }
    void OnGUI()
    {
        if (GameManager.over == true)
        {
            Vector2 dimensions = new Vector2(400f, 300f);
            Vector2 position = new Vector2((Screen.width - dimensions.x) / 2, (Screen.height - dimensions.y) / 2);
            GUI.Label(new Rect(position, dimensions), "Game Over");
        }
        if (GameManager.win == true)
        {
            Time.timeScale = 0;
            Vector2 dimensions = new Vector2(400f, 300f);
            Vector2 position = new Vector2((Screen.width - dimensions.x) / 2, (Screen.height - dimensions.y) / 2);
            GUI.Label(new Rect(position, dimensions), "You Won!");
        }
    }
}
