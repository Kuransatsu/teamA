using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TridentShooting : MonoBehaviour
{
    BulletChoose Bu;
    
    private Transform FirePoint;
    private GameObject BulletPre;
    private GameObject Trident;
    private GameObject Bullet;
    private Rigidbody2D BulletRB;
    [SerializeField]
    private bool canShoot;
    private string BulletName;
    private float UltrasonicWaveForce;

    void Start()
    { 
        Bu = GameObject.Find("yay").GetComponent<BulletChoose>();
        BulletName = Bu.BulletName;
        BulletPre = GameObject.Find("Trident");
        FirePoint = GameObject.Find("FirePoint").GetComponent<Transform>();
        BulletPre.active = false;
        UltrasonicWaveForce = 50;
        canShoot =true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canShoot) { canShoot = false; Shoot(); } 
        
        

    }
    private void Shoot()
    {
        
        Bullet = Instantiate(BulletPre, FirePoint.position, FirePoint.rotation);
        Bullet.active = true;
        BulletRB = Bullet.GetComponent<Rigidbody2D>();
        BulletRB.AddForce(FirePoint.right * UltrasonicWaveForce, ForceMode2D.Impulse);
        Destroy(Bullet, 1f);
        Invoke("relode", 1f);
    }
    public void relode()
    {
        canShoot = true;
        CancelInvoke("relode");
    }
    
}
