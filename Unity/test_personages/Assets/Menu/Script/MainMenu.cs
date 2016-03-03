using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    void OnGUI()
    {
        //taille par defaut des boutons
        const int buttonWidth = 100;
        const int buttonHeight = 50;

        //bouton demarrage du jeu
        if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 3) - (buttonHeight / 2), buttonWidth, buttonHeight), "Start"))
        {
            Application.LoadLevel("test0");
        }



    }
}
