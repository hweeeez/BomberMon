using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private Animator animator;
    public Rigidbody2D rb;
    private SpriteRenderer _spriteRender;
    private bool canMove = true;
    private PlayerLife lifesc;
    private void Start()
    {
        lifesc = GameObject.Find("Player").GetComponent<PlayerLife>();

        _spriteRender = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float h = moveHorizontal * speed;
        float v = moveVertical * speed;
        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        //float newVelocityX = 0f;
        if (canMove == true)
        {
            if (moveHorizontal < 0) // press left and stationary or moving left
            {
                //newVelocityX = -speed;
                rb.AddForce(-Vector2.right * speed);
                animator.SetInteger("DirectionX", -1);
            }
            else if (moveHorizontal > 0) // press right and stationary or movin
            {
                //newVelocityX = speed;
                rb.AddForce(Vector2.right * speed);
                animator.SetInteger("DirectionX", 1);
            }
            else
            {
                //newVelocityX = 0;
                animator.SetInteger("DirectionX", 0);
            }
            //float newVelocityY = 0f;
            if (moveVertical < 0)
            {
                //newVelocityY = -speed;
                rb.AddForce(Vector2.down * speed);
                animator.SetInteger("DirectionY", -1);
            }
            else if (moveVertical > 0)
            {
                //newVelocityY = speed;
                rb.AddForce(Vector2.up * speed);
                animator.SetInteger("DirectionY", 1);
            }
            else
            {
                animator.SetInteger("DirectionY", 0);
            }
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(h, v);
        }
        if (lifesc.numberOfLives == 0)
        {
            animator.SetTrigger("died");
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Fire")
        {
            canMove = false;
            animator.SetTrigger("damage");
            StartCoroutine(moving());
        }
    }
    IEnumerator moving()
    {
        yield return new WaitForSeconds(1f);
        canMove = true;
    }

}
