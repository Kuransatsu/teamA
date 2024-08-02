using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildboarMovment : MonoBehaviour
{
    [SerializeField]
    private float speed, fliper, time,DashTimer;
    private Vector2 dir;
    private BoxCollider2D groundChecker;
    private bool playerChecekr, WallChecker, DashMode,Dash,behaindChecker;
    private LayerMask wall,Player;
    private Rigidbody2D Rdash;
    void Start()
    {
        playerChecekr = false;
        WallChecker = false;
        Dash=false;
        Rdash = GetComponent<Rigidbody2D>();
        speed = 3;
        wall = LayerMask.GetMask("ground");
        Player = LayerMask.GetMask("Player");
        groundChecker =GetComponentInChildren<BoxCollider2D>();
        fliper = 1;
    }

    // Update is called once per frame
    void Update()
    { 
        time=Time.deltaTime;
        if (DashTimer >0)DashTimer-=time;
        WallChecker = Physics2D.Raycast(transform.position, Vector2.right , 0.6f*fliper, wall);
        playerChecekr = Physics2D.Raycast(transform.position, Vector2.right , 12 * fliper, Player);
        Dash = Physics2D.Raycast(transform.position, Vector2.right , 10 * fliper, Player);
        behaindChecker = Physics2D.Raycast(transform.position, Vector2.left, 2 * fliper, Player);
        Debug.DrawRay(transform.position,Vector2.right*12 * fliper, Color.red);
        Debug.DrawRay(transform.position, Vector2.right * 10 * fliper, Color.yellow);
        Debug.DrawRay(transform.position, Vector2.right*0.6f * fliper, Color.blue);
        Debug.DrawRay(transform.position, Vector2.left * 2 * fliper, Color.black);
        dir = transform.right * fliper;
        if (WallChecker||behaindChecker) { print("a"); flip(); }
        if (playerChecekr) speed = 5;
        else { speed=3; }
        if (!DashMode)Rdash.velocity = Vector2.right*dir*speed;
        if (Dash&&DashTimer<=0)dash();
    }
    private void dash()
    {
        DashMode = true;
        DashTimer = 3;
        Rdash.AddForce(Vector2.right*dir*20, ForceMode2D.Impulse);
        Invoke("StopDash", 1f);
    }
    private void StopDash()
    {
        DashMode=false;
        Rdash.velocity = Vector2.zero;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ground")) { print("s"); Invoke("flip", 0.1f); }
    }
    private void flip()
    {
        transform.localScale = new Vector3(-1* transform.localScale.x, 1,1);
        fliper = transform.localScale.x;
        CancelInvoke("filp");
    }
}
