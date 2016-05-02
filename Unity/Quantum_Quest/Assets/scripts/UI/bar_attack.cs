using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class bar_attack : MonoBehaviour {
    
    
    public Image barattack;
    public GUIStyle style_button;
    public int buttonWidth;
    public int buttonHeight;

	// Use this for initialization
	void Start () {
    }

    


	
	// Update is called once per frame
	void Update () {

        barattack.transform.position = new Vector2(Screen.width / 2, 27);
    }

    void OnGUI ()
    {
        if (GUI.Button(new Rect((Screen.width / 2) - 297.5f, Screen.height - 50, buttonWidth, buttonHeight), "", style_button))
        {

        }
    }



}
