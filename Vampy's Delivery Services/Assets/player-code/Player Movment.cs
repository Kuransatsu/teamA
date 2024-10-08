using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovment : MonoBehaviour
{
    //variables for the movment
    private float x, speed, time;
    private bool left , right;
    private Vector2 DirHorizontal;

    //variables for dashing
    private bool Dright,Dleft;
    private int NumOfDash;
    private CapsuleCollider2D DashCollider;
    private Vector2 DashDir;
    private Rigidbody2D dash;
    private TrailRenderer DashTrail;
    private LayerMask Enemy;


    //variables for jumping
    private bool OnGround, DoubleJump;
    private float force;
    private LayerMask ground;
    private Rigidbody2D jump;

    //variables for climbing
    private bool up,down,OnLadder;
    private float y,ClimbSpeed,Climbing;
    private Rigidbody2D ClimbGravity;
    private Vector2 DirVertical;

    void Start()
    {
        
        ground = LayerMask.GetMask("ground");
        speed = 10;


        ClimbSpeed = 7;
        ClimbGravity = GetComponent<Rigidbody2D>();

        jump = GetComponent<Rigidbody2D>();


        dash = GetComponent<Rigidbody2D>();
        DashTrail = GetComponent<TrailRenderer>();
        NumOfDash = 2;
    }

   
    void Update()
    {
        time = Time.deltaTime;
        InputChecker();
    }
    
    private void InputChecker()
    {
        MovmentChecker();
        DashChecker();
        JumpChecker();
        ClimbChecker();
    }


//=====================================Movement=====================================================    
    private void MovmentChecker()
    {
        if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow)) right=true;
        else right = false;
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow)) left=true;
        else left = false;

        if (right && left) x = 0;
        else if (right) x = 1;
        else if (left) x = -1;
        else x = 0;
        DirHorizontal = transform.right * x;
        
        move();
    }
    private void move()
    {
        transform.Translate(DirHorizontal.normalized *time* speed);
    }
//==================================================================================================

//======================================Dash========================================================
    private void DashChecker()
    {
        if (!DashTrail.enabled) dash.velocity = new Vector2(0, dash.velocity.y);

        if (Input.GetKeyDown(KeyCode.LeftShift)&&NumOfDash>0)
        {
            if (right && left) return;
            else if (right) DashDir = Vector2.right;
            else if (left) DashDir = Vector2.left;
            Dash();
            NumOfDash--;
            if (NumOfDash < 2&&NumOfDash!=0) Invoke("DashReset", 1);
        }

        
    }
    private void Dash()
    {
        
        DashTrail.enabled=true;
        dash.AddForce(DashDir*100,ForceMode2D.Impulse);
        Invoke("StopDash", 0.1f);
    }
    private void DashReset() { NumOfDash = 2; }
    
    private void StopDash(){ dash.velocity=Vector2.zero; DashTrail.enabled = false; }
//==================================================================================================

//========================================Jump======================================================
    private void JumpChecker()
    {
        OnGround = Physics2D.Raycast(transform.position, Vector2.down,1.1f, ground);
        if (OnGround||OnLadder) DoubleJump = true;
        if (Input.GetKeyDown(KeyCode.Space)&&OnGround) { force = 10;  Jump();}
        else if (Input.GetKeyDown(KeyCode.Space) && DoubleJump) {  force = 7; Jump();DoubleJump =false; }
        
    }
    private void Jump()
    {
        jump.velocity=new Vector2(jump.velocity.x,0);
        jump.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
//==================================================================================================

//=========================================Climb====================================================
    private void ClimbChecker()
    {
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.DownArrow)) up = true;   else up = false;
        if (Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.UpArrow)) down = true; else down = false;

        if (OnLadder)
        {
            if (up && down) y = 0;
            else if (up) y = 1;
            else if (down) y = -1;
            else y = 0;
            DirVertical = transform.up * y;
            if (Mathf.Abs(y)>0) Climb();
        }
        if (!OnLadder||Input.GetKeyDown(KeyCode.Space)) ClimbGravity.gravityScale = 2;
    }
    private void Climb()
    {
        ClimbGravity.gravityScale = 0;
        ClimbGravity.velocity = Vector2.zero;
        transform.Translate(DirVertical.normalized * ClimbSpeed * time);
    }
    //==================================================================================================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder")) OnLadder = true;
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder")) OnLadder = false;
    }
}

