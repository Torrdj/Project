using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu0 : MonoBehaviour
{

    public GameObject networkMaster;

    private GameObject instantiatedMaster;
    private StartNetwork scriptStartNet;

    private string serverIP = "127.0.0.1";
    private int serverPort = 25000;

    private bool menu1 = true;
    private bool menu2 = false;
    private bool menu3 = false;
    private bool menu4 = false;
    private int tourner = 1;

    //rotation camera
    Quaternion rot;
    float speed = 1.0f;

    // Use this for initialization
    void OnGUI()
    {
        //int sizeButtonX = 250;

        //taille par defaut des boutons
        const int buttonWidth = 100;
        const int buttonHeight = 50;

        //Application.LoadLevel("test0");
        //SceneManager.LoadScene("first");

        #region Menu 1
        if (menu1)
        {
            rot = Quaternion.AngleAxis(45, new Vector3(0, 135, 0));
            if (Camera.main.transform.rotation != rot && tourner == 1)
            {
                transform.Rotate(Vector3.up, speed);
            }
            else if (Camera.main.transform.rotation != rot && tourner == -1)
            {
                transform.Rotate(Vector3.down, speed);
            }
            else
            {
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 5) - (buttonHeight / 2), buttonWidth, buttonHeight), "Démarer"))
                {
                    menu1 = false;
                    menu2 = true;
                    tourner = 1;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (3 * Screen.height / 5) - (buttonHeight / 2), buttonWidth, buttonHeight), "Multijoueur"))
                {
                    menu1 = false;
                    menu4 = true;
                    tourner = -1;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (4 * Screen.height / 5) - (buttonHeight / 2), buttonWidth, buttonHeight), "Quitter"))
                {
                    //fermeture tu jeu
                }
            }



        }
        #endregion

        #region Menu 2
        if (menu2)
        {
            rot = Quaternion.AngleAxis(135, new Vector3(0, 135, 0));
            if (Camera.main.transform.rotation != rot && tourner == 1)
            {
                transform.Rotate(Vector3.up, speed);
            }
            else if (Camera.main.transform.rotation != rot && tourner == -1)
            {
                transform.Rotate(Vector3.down, speed);
            }
            else
            {
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 6) - (buttonHeight / 2), buttonWidth, buttonHeight), "Continuer"))
                {
                    menu2 = false;
                    menu3 = true;
                    tourner = 1;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (5 * Screen.height / 6) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retour"))
                {
                    menu2 = false;
                    menu1 = true;
                    tourner = -1;
                }
            }
        }
        #endregion

        #region Menu 3
        if (menu3)
        {
            rot = Quaternion.AngleAxis(225, new Vector3(0, 225, 0));
            if (Camera.main.transform.rotation != rot && tourner == 1)
            {
                transform.Rotate(Vector3.up, speed);
            }
            else
            {
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (5 * Screen.height / 6) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retour"))
                {
                    menu3 = false;
                    menu2 = true;
                    tourner = -1;
                }
            }
        }
        #endregion

        #region Menu 4
        if (menu4)
        {
            rot = Quaternion.AngleAxis(-45, new Vector3(0, 135, 0));

            if (Camera.main.transform.rotation != rot && tourner == -1)
            {
                transform.Rotate(Vector3.down, speed);
            }
            else
            {
                //La demande de champs d'ip pour rejoindre un serveur
                serverIP = GUI.TextField(new Rect(Screen.width / 2 - (120 / 2), (2 * Screen.height / 6) - (30 / 2), 120, 30), serverIP, 40);

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
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (5 * Screen.height / 6) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retour"))
                {
                    menu4 = false;
                    menu1 = true;
                    tourner = 1;
                }
            }
        }
        #endregion


    }
}