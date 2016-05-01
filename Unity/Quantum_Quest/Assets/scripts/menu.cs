using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class menu : MonoBehaviour
{
    //Description Personnage
    private string Server = "Server :\n" +
        "\nLes Servers, autrefois peu connus du grand public, s'illustrent désormais comme étant les protecteurs de leurs alliés grâce à leur taille imposante et leur robustesse à toute épreuve.\n" +
        "\nCette classe incarne les Tanks à l'état pur. Ces personnages possédent une quantité de vie importante ainsi qu'une bonne défense, mais une attaque et une vitesse réduites. Ils ont un systême d'attaques basé sur la protection de ses alliés et de lui-même, ainsi que sur les effets de contrôles.";
    private string Computer = "Computer :\n" +
        "\nLes Computers, contrairement aux Servers, étaient connus de tous et quasiment indispensables pour chaque personne. Aujourd'hui, frustrés qu'on les compare sans cesse aux Laptops plus \"passe-partout\" qu'eux, ils se sont renfermés sur eux même afin d'accroître leur puissance et de peaufiner des scripts destructeurs.\n" +
        "\nLa classe Computer représentent les DPS \"lourds\". Ils infligent des dégâts élevés et maîtrisent des attaques puissantes pour achever l'ennemi.";
    private string Laptop = "Laptop :\n"+
        "\nLes Laptops, petits derniers de l'industrie des ordinateurs encore présents aujourd'hui, représentent les \"blonds\" des Intelligences Artificielles.Toujours plus fins et plus rapides, ils tailladent leurs ennemis, ne se souciant que d'eux même.\n" +
        "\nLa classe Laptop correspond aux DPS \"légers\". Ils ont moins de vie que les autres classes, mais foudroient leurs ennemis en enchaînant les attaques à la même vitesse que l'électricité parcourt leurs circuits.";
    private int i = 0;

    //personnage
    public List<GameObject> ListePersonnage = new List<GameObject>();

    private bool menu1 = true;
    private bool menu2 = false;
    private bool menu3 = false;
    private bool menu4 = false;
    private int tourner = 1;
    
    public Text DescriptionPersonnage;

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
                if (GUI.Button(new Rect(Screen.width / 2 - ((buttonWidth + 50) / 2), (2 * Screen.height / 5) - (buttonHeight / 2), buttonWidth + 50, buttonHeight), "Nouveau personnage"))
                {
                    menu1 = false;
                    menu2 = true;
                    tourner = 1;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (3 * Screen.height / 5) - (buttonHeight / 2), buttonWidth, buttonHeight), "Charger"))
                {

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
                
                DescriptionPersonnage.enabled = true;
                DescriptionPersonnage.transform.position = new Vector2(Screen.width - (225), Screen.height / 5);
                //actualisation Description Personnage
                switch (i)
                {
                    case 0:
                        //ListePersonnage[2].SetActive(false);
                        ListePersonnage[0].SetActive(true);
                        DescriptionPersonnage.text = Laptop;
                        break;
                    case 1:
                        ListePersonnage[0].SetActive(false);
                        //ListePersonnage[1].SetActive(true);
                        DescriptionPersonnage.text = Computer;
                        break;
                    case 2:
                        //ListePersonnage[1].SetActive(false);
                        //ListePersonnage[2].SetActive(true);
                        DescriptionPersonnage.text = Server;
                        break;
                }
                
                if (GUI.Button(new Rect((Screen.width - 200) - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Valider"))
                {
                    ListePersonnage[0].SetActive(false);
                    //foreach(GameObject x in ListePersonnage)
                    //    x.SetActive(false);

                    DescriptionPersonnage.enabled = false;
                    menu2 = false;
                    menu3 = true;
                    tourner = 1;
                }
                if (GUI.Button(new Rect((Screen.width - 75)  - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retour"))
                {
                    ListePersonnage[0].SetActive(false);
                    //foreach (GameObject x in ListePersonnage)
                    //    x.SetActive(false);


                    DescriptionPersonnage.enabled = false;
                    menu2 = false;
                    menu1 = true;
                    tourner = -1;
                }
                if (GUI.Button(new Rect(75 - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Changer"))
                {
                    i += 1;
                    i = i % 3;
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
                NetworkManagerHUD ntHUD = FindObjectOfType<NetworkManagerHUD>();
                ntHUD.enabled = true;

                if (GUI.Button(new Rect((Screen.width - 75) - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retour"))
                {
                    ntHUD.enabled = false;
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
                NetworkManagerHUD ntHUD = FindObjectOfType<NetworkManagerHUD>();
                ntHUD.enabled = true;

                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (5 * Screen.height / 6) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retour"))
                {
                    ntHUD.enabled = false;
                    menu4 = false;
                    menu1 = true;
                    tourner = 1;
                }
            }
        }
        #endregion


    }
}
