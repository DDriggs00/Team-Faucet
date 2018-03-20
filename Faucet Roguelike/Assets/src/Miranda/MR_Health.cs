using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MR_Health : MonoBehaviour
{
    LH_Health playerhealth;
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public Slider healthbar;

    // Use this for initialization
    void Start()
    {
        CurrentHealth = MaxHealth;
        playerhealth = FindObjectOfType<LH_Health>();
        healthbar.value = Calculatehealth();
        int x = -1;
    }

    float Calculatehealth()
    {
        return CurrentHealth / MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = Calculatehealth();

    }
    void FixedUpdate()
    {
        playerhealth.getHP();
        
    }
}