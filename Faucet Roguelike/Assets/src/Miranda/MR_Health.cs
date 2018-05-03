using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MR_Health : MonoBehaviour
{
    public Slider healthSlider;
    private LH_Health healthscript;
    private int mHealth;
    [SerializeField]
    Image sliderImage;

    // Use this for initialization
    void Start()
    {
        healthscript = FindObjectOfType<LH_Health>();
        //healthscript = this.gameObject.GetComponent<LH_Health>();

    }

    // Update is called once per frame
    void Update()
    {
        mHealth = healthscript.getHP();

        //GameObject.Find("heath").GetComponent<LH_Health>().getHP();
        //Debug.Log("Health bar: " + mHealth);
        //        healthSlider.value = healthscript.getHP();
        UpdateSlider();
    }

    void UpdateSlider()
    {
        sliderImage.fillAmount = mHealth / 100f;
    }
    void SliderChangedFunctionName()
    {

        healthscript.getHP();
    }
}