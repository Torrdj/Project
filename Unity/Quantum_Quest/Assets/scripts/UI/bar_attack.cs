using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class bar_attack : MonoBehaviour {
    
    
    public Image barattack;
    public List<GUIStyle> style_buttons;
    public int buttonWidth;
    public int buttonHeight;

    // Use this for initialization
    void Start ()
    {
    }

    


	
	// Update is called once per frame
	void Update () {

        barattack.transform.position = new Vector2(Screen.width / 2, 27);
    }

    void OnGUI ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

        }
        if (GUI.Button(new Rect((Screen.width / 2) - 297.5f, Screen.height - 50, buttonWidth, buttonHeight), "", style_buttons[0]))
        {

        }

        if (GUI.Button(new Rect((Screen.width / 2) - 247.5f, Screen.height - 50, buttonWidth, buttonHeight), ""))
        {

        }

        if (GUI.Button(new Rect((Screen.width / 2) - 197.5f, Screen.height - 50, buttonWidth, buttonHeight), ""))
        {

        }
        if (GUI.Button(new Rect((Screen.width / 2) - 147.5f, Screen.height - 50, buttonWidth, buttonHeight), ""))
        {

        }
        if (GUI.Button(new Rect((Screen.width / 2) - 97.5f, Screen.height - 50, buttonWidth, buttonHeight), ""))
        {

        }
        if (GUI.Button(new Rect((Screen.width / 2) - 47.5f, Screen.height - 50, buttonWidth, buttonHeight), ""))
        {

        }
        if (GUI.Button(new Rect((Screen.width / 2) + 2.5f, Screen.height - 50, buttonWidth, buttonHeight), ""))
        {

        }
        if (GUI.Button(new Rect((Screen.width / 2) + 52.5f, Screen.height - 50, buttonWidth, buttonHeight), ""))
        {

        }
        if (GUI.Button(new Rect((Screen.width / 2) + 102.5f, Screen.height - 50, buttonWidth, buttonHeight), ""))
        {

        }
        if (GUI.Button(new Rect((Screen.width / 2) + 152.5f, Screen.height - 50, buttonWidth, buttonHeight), ""))
        {

        }
        if (GUI.Button(new Rect((Screen.width / 2) + 202.5f, Screen.height - 50, buttonWidth, buttonHeight), ""))
        {

        }
        if (GUI.Button(new Rect((Screen.width / 2) + 252.5f, Screen.height - 50, buttonWidth, buttonHeight), ""))
        {

        }
    }



}
