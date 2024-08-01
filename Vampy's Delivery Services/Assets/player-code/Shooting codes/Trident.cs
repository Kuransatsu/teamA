using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident : MonoBehaviour
{
    private Camera Camera;
    private Vector3 MousePos;
    private Vector2 LookDir;
    private float angle;
    private Transform AimTransform;


    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        AimTransform = GameObject.Find("Aim").GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
        LookDir = (MousePos - AimTransform.position);
        print(LookDir.x);
        if (LookDir.x > 0)
        {
            Quaternion target = Quaternion.Euler(0, 0, -90);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 0.8f*Time.deltaTime);
        }
        else
        {
            Quaternion target = Quaternion.Euler(0, 0, 90);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 0.8f*Time.deltaTime);
        }
     
        
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
