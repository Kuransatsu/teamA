using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChoicer : MonoBehaviour
{
    [SerializeField]
    private bool Trident, Sowrd;
    private TridentShooting TridentAttack;
    private PlayerAttack SowrdAttack;
    private void Awake()
    {
        TridentAttack =GetComponent<TridentShooting>();
        SowrdAttack =GetComponent<PlayerAttack>();
    }
    private void Update()
    {
        if (Sowrd){ Trident = false;Sowrd = true; }
        else { Trident = true;Sowrd = false; }
        
        TridentAttack.enabled = Trident;
        SowrdAttack.enabled = Sowrd;
    }

}
