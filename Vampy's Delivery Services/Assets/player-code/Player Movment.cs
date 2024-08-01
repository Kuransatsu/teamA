using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovment : MonoBehaviour
{
    //variables for the movment
    private float x, speed, time;
    private bool left , right;
    private Vector2 dir;

    //variables for dashing
    private int NumOfDash;
    private Vector2 DashDir;
    private Rigidbody2D dash;
    private TrailRenderer DashTrail;


    //variables for jumping
    private bool OnGround, DoubleJump;
    private float force;
    private LayerMask ground;
    private Rigidbody2D jump;

    //variables for climping
    private bool CanClimp;
    private float y;

    void Start()
    {
        NumOfDash = 2;
        ground = LayerMask.GetMask("ground");
        speed = 4;
        jump = GetComponent<Rigidbody2D>();
        dash = GetComponent<Rigidbody2D>();
        DashTrail = GetComponent<TrailRenderer>();
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
        ClimpChecker();
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
        dir = transform.right * x;
        move();
    }
    private void move()
    {
        transform.Translate(dir.normalized *time* speed);
    }
//==================================================================================================

//======================================Dash========================================================
    private void DashChecker()
    {
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
        OnGround = Physics2D.Raycast(transform.position, Vector2.down,0.6f, ground);
        if (OnGround) DoubleJump = true;
        if (Input.GetKeyDown(KeyCode.Space)&&OnGround) { force = 10;  Jump();}
        else if (Input.GetKeyDown(KeyCode.Space) && DoubleJump) {  force = 7; Jump();DoubleJump =false; }
        
    }
    private void Jump()
    {
        jump.velocity=new Vector2(jump.velocity.x,0);
        jump.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
//==================================================================================================

//=========================================Climp====================================================
    private void ClimpChecker()
    {

    }
//==================================================================================================
}

