using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class Menu : MonoBehaviour
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
    //public List<GameObject> ListePersonnage = new List<GameObject>();
    //public List<GameObject> networkPlayer = new List<GameObject>();

    //button
    public GUIStyle GuiButton;

    private bool menu1 = true;
    private bool menu2 = false;
    private bool menu3 = false;
    private bool menu4 = false;
    private int tourner = 1;
    private bool resolution = false;
    public Text warning;

    public Text DescriptionPersonnage;
    public Text Titre;

    //rotation camera
    Quaternion rot;
    float speed = 1.0f;

    //network
    public string Pseudo_;
    public Text Titre_Pseudo;
    public PlayerInfo player;
    //public NetworkManager network;
    //NetworkManagerHUD ntHUD;

    //option
    private float musicVol = 20;
    private bool fullscreen;

    void Start()
    {
        fullscreen = !Screen.fullScreen;
        //multijoueur
        Cursor.visible = true;
        //ntHUD = FindObjectOfType<NetworkManagerHUD>();
        //ntHUD.enabled = false;
        player = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
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

        #region Menu 1 Principal
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
                if (GUI.Button(new Rect(Screen.width / 2 - ((buttonWidth + 50) / 2), Screen.height / 2 - 150, buttonWidth + 50, buttonHeight), "Nouveau personnage", GuiButton))
                {
                    Titre.enabled = false;
                    menu1 = false;
                    menu2 = true;
                    tourner = 1;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), Screen.height / 2 - 50, buttonWidth, buttonHeight), "Charger", GuiButton))
                {

                }
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), Screen.height / 2 + 50, buttonWidth, buttonHeight), "Options", GuiButton))
                {
                    Titre.enabled = false;
                    menu1 = false;
                    menu4 = true;
                    tourner = -1;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), Screen.height / 2 + 150, buttonWidth, buttonHeight), "Quitter", GuiButton))
                {
                    Application.Quit();
                }
            }



        }
        #endregion

        #region Menu 2 Personage
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
                        //ListePersonnage[2].SetActive(false);
                        //ListePersonnage[0].SetActive(true);
                        DescriptionPersonnage.text = Laptop;
                        break;
                    case 1:
                        //ListePersonnage[0].SetActive(false);
                        //ListePersonnage[1].SetActive(true);
                        DescriptionPersonnage.text = Computer;
                        break;
                    case 2:
                        //ListePersonnage[1].SetActive(false);
                        //ListePersonnage[2].SetActive(true);
                        DescriptionPersonnage.text = Server;
                        break;
                }

                if (GUI.Button(new Rect((Screen.width - 200) - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Valider", GuiButton))
                {
                    PlayerInfo.TYPES type;
                    switch (i)
                    {
                        case 0:
                            type = PlayerInfo.TYPES.Laptop;
                            break;
                        case 1:
                            type = PlayerInfo.TYPES.Computer;
                            break;
                        case 2:
                            type = PlayerInfo.TYPES.Server;
                            break;
                        default:
                            throw new System.IndexOutOfRangeException();
                    }
                    GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>().Type = type;

                    //network.playerPrefab = networkPlayer[i];
                    //network.gameObject.GetComponent<PlayerInfo>().prefab_name = networkPlayer[i].name;
                    Titre.enabled = false;
                    //foreach (GameObject x in ListePersonnage)
                    //    x.SetActive(false);
                    FondTexte.enabled = false;
                    DescriptionPersonnage.enabled = false;
                    menu2 = false;
                    menu3 = true;
                    tourner = 1;
                }
                if (GUI.Button(new Rect((Screen.width - 75) - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retour", GuiButton))
                {
                    Titre.enabled = false;
                    //foreach (GameObject x in ListePersonnage)
                    //    x.SetActive(false);
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

        #region Menu 3 Multijoueur
        if (menu3)
        {
            rot = Quaternion.AngleAxis(225, new Vector3(0, 225, 0));
            if (Camera.main.transform.rotation != rot && tourner == 1)
            {
                transform.Rotate(Vector3.up, speed);
            }
            else
            {

                Titre_Pseudo.enabled = true;
                Titre.enabled = true;
                Titre.text = "Ready ?";
                //ntHUD.enabled = true;
                //ntHUD.offsetX = Screen.width / 2 - 100;
                //ntHUD.offsetY = Screen.height / 3;



                Pseudo_ = GUI.TextArea(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 11, 150, 22), Pseudo_, 200);

                if (Input.GetKey(KeyCode.Return))
                {
                    if (Pseudo_ != "")
                    {
                        warning.enabled = false;
                        player.Name = Pseudo_;
                        PlayerPrefs.SetInt("Son", (int)musicVol);
                        GameObject.Find("NetworkHolder").GetComponent<NetworkController>().enabled = true;

                        //SceneManager.LoadSceneAsync("first");
                        SceneManager.LoadScene("loadingScene");
                    }
                    else
                    {
                        warning.enabled = true;
                    }
                }

                if (GUI.Button(new Rect((Screen.width / 2) - (buttonWidth / 2), (Screen.height / 2) - (buttonHeight / 2) + 150, buttonWidth, buttonHeight), "Start", GuiButton))
                {
                    if (Pseudo_ != "")
                    {
                        warning.enabled = false;
                        player.Name = Pseudo_;
                        PlayerPrefs.SetInt("Son", (int)musicVol);
                        GameObject.Find("NetworkHolder").GetComponent<NetworkController>().enabled = true;

                        //SceneManager.LoadSceneAsync("first");
                        SceneManager.LoadScene("loadingScene");
                    }
                    else
                    {
                        warning.enabled = true;
                    }

                }

                if (GUI.Button(new Rect((Screen.width - 75) - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retour", GuiButton))
                {
                    Titre_Pseudo.enabled = false;
                    Titre.enabled = false;
                    //ntHUD.enabled = false;
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

                //musique
                musicVol = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50 + 50, Screen.height / 2 + 130, 100, 30), musicVol, (float)0.0, (float)100.0);
                GUI.Label(new Rect(Screen.width / 2 - 50 + 110 + 50, Screen.height / 2 + 125, 100, 30), "Musique " + (int)musicVol + " %");

                //Video
                var qualities = QualitySettings.names;


                for (int i = 0; i < qualities.Length; i++)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 50 - 175, Screen.height / 2 - 120 + i * 30 + 75, 100, 30), qualities[i], GuiButton))
                    {
                        QualitySettings.SetQualityLevel(i, true);
                    }
                }

                fullscreen = GUI.Toggle(new Rect(Screen.width / 2 - 50 + 50, Screen.height / 2 + 30, 125, 25), fullscreen, "Plein Ecran");

                Resolution[] resolutions = Screen.resolutions;
                if (GUI.Button(new Rect(Screen.width / 2 - 50 - 75, Screen.height / 2 - 120 + 75, 100, 30), Screen.width + "x" + Screen.height, GuiButton))
                {
                    if (resolution)
                        resolution = false;
                    else
                        resolution = true;
                }
                if (resolution)
                {
                    for (int i = 0; i < resolutions.Length; i++)
                    {
                        if (GUI.Button(new Rect(Screen.width / 2 - 50 - 75, Screen.height / 2 - 120 + i * 30 + 75 + 30, 100, 30), resolutions[i].width + "x" + resolutions[i].height, GuiButton))
                        {
                            if (fullscreen)
                                Screen.SetResolution(resolutions[i].width, resolutions[i].height, true);
                            else
                                Screen.SetResolution(resolutions[i].width, resolutions[i].height, false);
                            resolution = false;
                        }
                    }
                }





                if (GUI.Button(new Rect((Screen.width - 75) - (buttonWidth / 2), (Screen.height - 50) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retour", GuiButton))
                {
                    if (fullscreen)
                        Screen.SetResolution(Screen.width, Screen.height, true);
                    else
                        Screen.SetResolution(Screen.width, Screen.height, false);
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
