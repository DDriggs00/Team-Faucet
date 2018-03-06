using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MR_Health : MonoBehaviour {
    LH_Health playerhealth;
   
	// Use this for initialization
	void Start () {
        playerhealth = FindObjectOfType<LH_Health>();
        int x = -1; 
	}
    public GUIStyle progress_empty;
    public GUIStyle progress_full;

    //current progress
    public float barDisplay;

    Vector2 pos = new Vector2(10, 50);
    Vector2 size = new Vector2(250, 50);

    public Texture2D emptyTex;
    public Texture2D fullTex;

    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y), emptyTex, progress_empty);

        GUI.Box(new Rect(pos.x, pos.y, size.x, size.y), fullTex, progress_full);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));

        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex, progress_full);

        GUI.EndGroup();
        GUI.EndGroup();
    }
     
    // Update is called once per frame
    void Update () {
       

    }
    void FixedUpdate()
    {
        playerhealth.getHP();
        Debug.Log("health bar is displaying" + playerhealth.getHP());
    }
}
