using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void OnGUI()
    {
        if (GUI.Button(Rect.MinMaxRect(Screen.width/2 - 100, Screen.height/2 - 50, Screen.width/2 + 200, Screen.height/2 + 100), "Start"))
        {
            Application.LoadLevel("test0");
        }
    }
}
