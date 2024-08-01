using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Camera Camera;
    private Vector3 MousePos;
    private Vector2 LookDir;
    private float angle;
    

    void Start()
    {
        Camera =GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
        LookDir = (MousePos - transform.position);
        angle = Mathf.Atan2(LookDir.y, LookDir.x)*Mathf.Rad2Deg;
        transform.eulerAngles=new Vector3(0,0, angle);
        
    }
}
