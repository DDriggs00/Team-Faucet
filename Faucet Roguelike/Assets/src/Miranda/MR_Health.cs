using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MR_Health : MonoBehaviour
{
    public Slider healthSlider;
    private LH_Health healthscript;
    // Use this for initialization
    void Start()
    {
        healthscript = this.gameObject.GetComponent<LH_Health>();

    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("heath").GetComponent<LH_Health>().getHP();

        healthSlider.value = healthscript.getHP() / 100;
    }

    void SliderChangedFunctionName()
    {

        healthscript.getHP();
    }
}
