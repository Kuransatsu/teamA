using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChoose : MonoBehaviour
{
    public bool T, U;
    public string BulletName;
    private void Awake()
    {
        BulletName = "Trident";
    }
    private void Update()
    {
        if (T)
        {
            BulletName = "Trident";
        }
        if (U)
        {
            BulletName = "UltrasonicWave";
        }
    }
}
