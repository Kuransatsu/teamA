using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    //variables for the movment
    private float x, speed, time;
    private bool left , right;
    private Vector2 dir;

    //variables for dashing
    private bool CanDash;
    private float DashSpeed;
    private Rigidbody2D Dash;


    //variables for jumping
    private bool OnGround, DoubleJump;
    private float force;
    private LayerMask ground;
    private Rigidbody2D jump;

    void Start()
    {
        ground = LayerMask.GetMask("ground");
        speed = 4;
        DashSpeed = 6;
        jump = GetComponent<Rigidbody2D>();
        Dash = GetComponent<Rigidbody2D>();
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
        
    }
    private void DashChecker()
    {

    }
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

    private void JumpChecker()
    {
        OnGround = Physics2D.Raycast(transform.position, Vector2.down,0.6f, ground);
        if (OnGround) DoubleJump = true;
        if (Input.GetKeyDown(KeyCode.Space)&&OnGround) { force = 10;  Jump();}
        else if (Input.GetKeyDown(KeyCode.Space) && DoubleJump) {  force = 7; Jump();}
        
    }
    private void Jump()
    {
        jump.velocity=Vector2.zero;
        jump.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
}
