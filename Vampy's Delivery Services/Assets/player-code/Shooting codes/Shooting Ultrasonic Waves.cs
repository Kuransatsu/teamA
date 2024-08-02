using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingUltrasonicWaves : MonoBehaviour
{
    
    
    private Transform FirePoint;
    [SerializeField]
    private GameObject UltrasonicWave;
    private GameObject Bullet;
    private Rigidbody2D BulletRB;
    private float UltrasonicWaveForce;
   
    void Start()
    {
        FirePoint = GameObject.Find("FirePoint").GetComponent<Transform>();
        UltrasonicWaveForce = 50;
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Mouse0)) Shoot();
    }
    private void Shoot()
    { 
        Bullet = Instantiate(UltrasonicWave,FirePoint.position,FirePoint.rotation);
        BulletRB = Bullet.GetComponent<Rigidbody2D>();
        BulletRB.AddForce(FirePoint.right *UltrasonicWaveForce,ForceMode2D.Impulse);
        Destroy(Bullet,1f);
    }
}
