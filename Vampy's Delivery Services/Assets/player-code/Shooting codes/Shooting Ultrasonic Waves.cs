using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingUltrasonicWaves : MonoBehaviour
{
    
    BulletChoose Bu;
    private Transform FirePoint;
    private GameObject BulletPre;
    private GameObject Trident;
    private GameObject Bullet;
    private Rigidbody2D BulletRB;

    private string BulletName;
    private float UltrasonicWaveForce;
   
    void Start()
    {
        Bu = GameObject.Find("yay").GetComponent<BulletChoose>();
        BulletName = Bu.BulletName;
        if (BulletName == "Trident")
            BulletPre = GameObject.Find("Trident");
        if (BulletName== "UltrasonicWave")
            BulletPre = GameObject.Find("UltrasonicWave");
        FirePoint = GameObject.Find("FirePoint").GetComponent<Transform>();
        BulletPre.active = false;
        UltrasonicWaveForce = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) Shoot();
    }
    private void Shoot()
    { 
        Bullet = Instantiate(BulletPre,FirePoint.position,FirePoint.rotation);
        Bullet.active=true;
        BulletRB = Bullet.GetComponent<Rigidbody2D>();
        BulletRB.AddForce(FirePoint.right *UltrasonicWaveForce,ForceMode2D.Impulse);
        //Destroy(Bullet,1f);
    }
}
