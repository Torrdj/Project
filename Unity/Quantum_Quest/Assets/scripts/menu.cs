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
    private string Laptop = "Laptop :\n" +
        "\nLes Laptops, petits derniers de l'industrie des ordinateurs encore présents aujourd'hui, représentent les \"blonds\" des Intelligences Artificielles.Toujours plus fins et plus rapides, ils tailladent leurs ennemis, ne se souciant que d'eux même.\n" +
        "\nLa classe Laptop correspond aux DPS \"légers\". Ils ont moins de vie que les autres classes, mais foudroient leurs ennemis en enchaînant les attaques à la même vitesse que l'électricité parcourt leurs circuits.";
    private int i = 0;
    public Image FondTexte;
    public Image Options;

    //personnage
    public List<GameObject> ListePersonnage = new List<GameObject>();
    public List<GameObject> networkPlayer = new List<GameObject>();

    //button
    public GUIStyle GuiButton;
    public List<GUIStyle> OptionButton;

    private bool menu1 = true;
    private bool menu2 = false;
    private bool menu3 = false;
    private bool menu4 = false;
    private int tourner = 1;

    public Text DescriptionPersonnage;
    public Text Titre;

    //rotation camera
    Quaternion rot;
    float speed = 1.0f;

    //network
    public NetworkManager network;
    NetworkManagerHUD ntHUD;

    //option
    public float sfxVol = 6;
    public float musicVol = 6;
    public float fieldOfView = 80;

    void Start()
    {
        Cursor.visible = true;
        ntHUD = FindObjectOfType<NetworkManagerHUD>();
        ntHUD.enabled = false;
    }

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
                Titre.enabled = true;
                Titre.text = "Quantum Quest";
                if (GUI.Button(new Rect(Screen.width / 2 - ((buttonWidth + 50) / 2), (2 * Screen.height / 6) - (buttonHeight / 2), buttonWidth + 50, buttonHeight), "Nouveau personnage", GuiButton))
                {
                    Titre.enabled = false;
                    menu1 = false;
                    menu2 = true;
                    tourner = 1;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (3 * Screen.height / 6) - (buttonHeight / 2), buttonWidth, buttonHeight), "Charger", GuiButton))
                {

                }
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (4 * Screen.height / 6) - (buttonHeight / 2), buttonWidth, buttonHeight), "Options", GuiButton))
                {
                    Titre.enabled = false;
                    menu1 = false;
                    menu4 = true;
                    tourner = -1;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (5 * Screen.height / 6) - (buttonHeight / 2), buttonWidth, buttonHeight), "Quitter", GuiButton))
                {
                    Application.Quit();
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
                Titre.enabled = true;
                Titre.text = "Choix des Personnages";
                FondTexte.enabled = true;
                DescriptionPersonnage.enabled = true;
                FondTexte.transform.position = new Vector2(Screen.width - (225), Screen.height / 2 - 25);
                DescriptionPersonnage.transform.position = new Vector2(Screen.width - (225), Screen.height / 2 - 25);
                //actualisation Description Personnage
                switch (i)
                {
                    case 0:
                        ListePersonnage[2].SetActive(false);
                        ListePersonnage[0].SetActive(true);
                        DescriptionPersonnage.text = Laptop;
                        break;
                    case 1:
                        ListePersonnage[0].SetActive(false);
                        ListePersonnage[1].SetActive(true);
                        DescriptionPersonnage.text = Computer;
                        break;
                    case 2:
                        ListePersonnage[1].SetActive(false);
                        ListePersonnage[2].SetActive(true);
                        DescriptionPersonnage.text = Server;
                        break;
                }

                if (GUI.Button(new Rect((Screen.width - 200) - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Valider", GuiButton))
                {
                    network.playerPrefab = networkPlayer[i];
                    network.gameObject.GetComponent<PlayerInfo>().prefab_name = networkPlayer[i].name;
                    Titre.enabled = false;
                    foreach (GameObject x in ListePersonnage)
                        x.SetActive(false);
                    FondTexte.enabled = false;
                    DescriptionPersonnage.enabled = false;
                    menu2 = false;
                    menu3 = true;
                    tourner = 1;
                }
                if (GUI.Button(new Rect((Screen.width - 75) - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retour", GuiButton))
                {
                    Titre.enabled = false;
                    foreach (GameObject x in ListePersonnage)
                        x.SetActive(false);
                    FondTexte.enabled = false;
                    DescriptionPersonnage.enabled = false;
                    menu2 = false;
                    menu1 = true;
                    tourner = -1;
                }
                if (GUI.Button(new Rect(75 - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Changer", GuiButton))
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
                Titre.enabled = true;
                Titre.text = "Serveur";
                ntHUD.enabled = true;
                ntHUD.offsetX = Screen.width / 2 - 100;
                ntHUD.offsetY = Screen.height / 3;

                if (GUI.Button(new Rect((Screen.width - 75) - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retour", GuiButton))
                {
                    Titre.enabled = false;
                    ntHUD.enabled = false;
                    menu3 = false;
                    menu2 = true;
                    tourner = -1;
                }
            }
        }
        #endregion

        #region Menu 4 Option
        if (menu4)
        {
            rot = Quaternion.AngleAxis(-45, new Vector3(0, 135, 0));

            if (Camera.main.transform.rotation != rot && tourner == -1)
            {
                transform.Rotate(Vector3.down, speed);
            }
            else
            {
                Options.enabled = true;
                Titre.enabled = true;
                Titre.text = "Options";

                //Audio
                sfxVol = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50 + 50, Screen.height / 2, 100, 30), sfxVol, (float)0.0, (float)10.0);
                GUI.Label(new Rect(Screen.width / 2 - 50 + 110 + 50, Screen.height / 2 - 5, 110, 30), "Effets sonores " + (int)sfxVol);

                musicVol = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50 + 50, Screen.height / 2 + 30, 100, 30), musicVol, (float)0.0, (float)10.0);
                GUI.Label(new Rect(Screen.width / 2 - 50 + 110 + 50, Screen.height / 2 + 25, 100, 30), "Musique " + (int)musicVol);

                //Video
                var qualities = QualitySettings.names;

                GUILayout.BeginVertical();

                for (int i = 0; i < qualities.Length; i++)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 50 - 125, Screen.height / 2 - 120 + i * 30 + 75, 100, 30), qualities[i], GuiButton))
                    {
                        QualitySettings.SetQualityLevel(i, true);
                    }
                }

                GUILayout.EndVertical();

                fieldOfView = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50 + 50, Screen.height / 2 - 30, 100, 20), fieldOfView, 30, 120);
                GUI.Label(new Rect(Screen.width / 2 - 50 + 110 + 50, Screen.height / 2 - 35, 130, 30), "Champ de vision " + (int)fieldOfView);



                if (GUI.Button(new Rect((Screen.width - 75) - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retour", GuiButton))
                {
                    Options.enabled = false;
                    Titre.enabled = false;
                    menu4 = false;
                    menu1 = true;
                    tourner = 1;
                }
            }
        }

        #endregion





    }
}
