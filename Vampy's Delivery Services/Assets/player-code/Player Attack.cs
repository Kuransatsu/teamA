using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    private PolygonCollider2D RightAttack, LeftAttack;
    private bool right, left;
    [SerializeField]
    private int NumOfAttack;
    void Start()
    {
        NumOfAttack = 2;
        RightAttack = GameObject.Find("RAttackRange").GetComponent<PolygonCollider2D>();
        LeftAttack = GameObject.Find("LAttackRange").GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            right = true;
            left = false;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            left = true;
            right = false;
        }

        
        if (Input.GetKeyDown(KeyCode.Mouse1) && NumOfAttack > 0)
        {
            NumOfAttack--;
            if (right) { LeftAttack.enabled = false; RightAttack.enabled = false; RightAttack.enabled = true; }
            if (left) { RightAttack.enabled = false; LeftAttack.enabled = false; LeftAttack.enabled = true; }
            Invoke("stopAttack", 0.2f);
            Invoke("AttackRelode", 0.5f);
        }
    }
    private void stopAttack()
    {
        RightAttack.enabled = false;
        LeftAttack.enabled = false;
    }
    private void AttackRelode()
    {
        NumOfAttack = 2;
        CancelInvoke("AttackRelode");
    }
}
