using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu0 : MonoBehaviour {

    public GameObject networkMaster;

    private GameObject instantiatedMaster;
    private StartNetwork scriptStartNet;

    private string serverIP = "127.0.0.1";
    private int serverPort = 25000;

	// Use this for initialization
	void OnGUI()
    {
        //int menuSizeX = 450;
        //int menuSizeY = 115;
        //float menuPosX = 20;
        //float menuPosY = Screen.height / 2 - menuSizeY / 2;
        //Rect mainMenu = new Rect(menuPosX, menuPosY, menuSizeX, menuSizeY);
        int sizeButtonX = 250;
        //int sizeButtonY = 30;

        //Le menu de base
        //GUI.BeginGroup(mainMenu, "");
        //GUI.Box(Rect.MinMaxRect(0, 0, menuSizeX, menuSizeY), "");

        //taille par defaut des boutons
        const int buttonWidth = 100;
        const int buttonHeight = 50;

        //La demande de champs d'ip pour rejoindre un serveur
        serverIP = GUI.TextField(new Rect(Screen.width / 2 - (120 / 2), (2 *Screen.height / 6) - (30 / 2), 120, 30), serverIP, 40);

        if (GUI.Button(new Rect(Screen.width / 2 - ((buttonWidth + 20) / 2), (3 * Screen.height / 6) - (buttonHeight / 2), buttonWidth + 20, buttonHeight), "Créer serveur"))
        {
            //Création du serveur
            instantiatedMaster = (GameObject)Instantiate(networkMaster, Vector3.zero, Quaternion.identity);
            scriptStartNet = (StartNetwork)instantiatedMaster.GetComponent("StartNetwork");
            scriptStartNet.server = true;
            scriptStartNet.listenPort = serverPort;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - ((buttonWidth + 50) / 2), (4 * Screen.height / 6) - (buttonHeight / 2), buttonWidth + 50, buttonHeight), "Rejoindre serveur"))
        {
            //Rejoindre serveur
            instantiatedMaster = (GameObject)Instantiate(networkMaster, Vector3.zero, Quaternion.identity);
            scriptStartNet = (StartNetwork)instantiatedMaster.GetComponent("StartNetwork");
            scriptStartNet.server = false;
            scriptStartNet.remoteIP = serverIP;
            scriptStartNet.listenPort = serverPort;
        }
        //GUI.EndGroup();

        
        //bouton demarrage du jeu
        if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (5 * Screen.height / 6) - (buttonHeight / 2), buttonWidth, buttonHeight), "Start"))
        {
            //Application.LoadLevel("test0");
            SceneManager.LoadScene("first");
        }
    }
}
