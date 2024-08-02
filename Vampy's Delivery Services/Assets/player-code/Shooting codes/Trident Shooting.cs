using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TridentShooting : MonoBehaviour
{
    
    
    private Transform FirePoint;
    [SerializeField]
    private GameObject Trient;
    private GameObject Bullet;
    private Rigidbody2D BulletRB;
    private bool canShoot;
    private float TridentForce;

    void Start()
    { 
        FirePoint = GameObject.Find("FirePoint").GetComponent<Transform>();
        TridentForce = 50;
        canShoot =true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canShoot) { canShoot = false; Shoot(); } 
        
        

    }
    private void Shoot()
    {
        
        Bullet = Instantiate(Trient, FirePoint.position, FirePoint.rotation);
        BulletRB = Bullet.GetComponent<Rigidbody2D>();
        BulletRB.AddForce(FirePoint.right * TridentForce, ForceMode2D.Impulse);
        Destroy(Bullet, 1f);
        Invoke("relode", 1f);
    }
    public void relode()
    {
        canShoot = true;
        CancelInvoke("relode");
    }
    
}
