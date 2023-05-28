using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    private bool isJumping;
    private bool isGrounded;
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    private BoxCollider2D myfeet;
    private int jumpCount;
    private bool jumpPressed;
    Animator an;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        myfeet = GetComponent<BoxCollider2D>();
    }
   
    // Update is called once per frame
    

    void Update()
    {
        if(Input.GetButtonDown("Jump")&& jumpCount > 0){
            jumpPressed = true;
        }
        //Attack();
        Flip();
        SwitchAnimation();
    }
     private void FixedUpdate() {
         
        isGrounded = myfeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        Run();
        Jump();
       
    }
    void Flip(){
        bool playerHasXAxisSpeed = Mathf.Abs(rb.velocity.x)>Mathf.Epsilon;
        if(playerHasXAxisSpeed){
            if(rb.velocity.x > 0.1f){
                transform.localRotation = Quaternion.Euler(0,0,0);
            }
            if(rb.velocity.x < -0.1f){
                 transform.localRotation = Quaternion.Euler(0,180,0);
            }
        }
    }
    void Run(){
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * moveSpeed,rb.velocity.y);
         rb.velocity = playerVel;
         bool playerHasXAxisSpeed = Mathf.Abs(rb.velocity.x)>Mathf.Epsilon;
        an.SetBool("Run", playerHasXAxisSpeed);
    }
    /*void Attack(){
        if(Input.GetButtonDown("Attack")){
            an.SetTrigger("Attack");
        }
    }*/
    /*void IsGrounded(){
        isGrounded = myfeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }*/
    void Jump(){
        {
            if(isGrounded){
                jumpCount =2;
                isJumping = false;
        }
        if(jumpPressed && isGrounded){
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if(jumpPressed && jumpCount > 0 && isJumping){
             rb.velocity = new Vector2(rb.velocity.x,jumpForce);
             jumpCount--;
             jumpPressed = false;
        }
        }
        
        }
    void SwitchAnimation(){
        an.SetBool("Idle",false);
        if(an.GetBool("Jump")){
            if(rb.velocity.y < 0.0f){
                an.SetBool("Jump",false);
                an.SetBool("Fall",true);
            }
        }
        else if(isGrounded){
            an.SetBool("Fall",false);
            an.SetBool("Idle",true);
        }
    }

    }
       
    

