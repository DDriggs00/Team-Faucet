using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MR_Health : MonoBehaviour {
    LH_Health playerhealth;
    public float CurrentHealth{ get; set; }
    public float MaxHealth { get; set; }
    public Slider healthbar;
     
	// Use this for initialization
	void Start ()
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
    void Update () {
       

    }
    void FixedUpdate()
    {
        playerhealth.getHP();
        Debug.Log("health bar is displaying" + playerhealth.getHP());
    }
    public class PlayerHealth : MonoBehaviour
    {
        private int hp;

        public int HP
        {
            get { return hp; }
            set
            {
                if (value != hp)
                {
                    hp = value;
                    Debug.Log("Player health set to: " + hp);
                }
            }
        }
    }

    public class SomeClass : MonoBehaviour
    {
        public PlayerHealth playerHealth;

        private int lastHP;

        private void Update()
        {
            if (playerHealth.HP != lastHP)
                Debug.Log("Player health has changed to: " + playerHealth.HP);

            lastHP = playerHealth.HP;
        }
    }
}
