using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Interface_Jeu : MonoBehaviour
{
    public RawImage Menu_Image;
    public RawImage Bar;
    public List<GUIStyle> ListGuiButtonAttack = new List<GUIStyle>(10);
    public List<GUIStyle> ListGuiButton;
    public AudioSource Music_Ambiance;

    private bool Game = true;
    private bool Menu_ = false;
    private bool Option = false;

    //Option
    private float sfxVol = 20;
    private float musicVol = 20;
    private bool fullscreen;
    private bool resolution = false;
    public Text Option_Titre;

    //Profil
    public RawImage Profil_laptop;
    public RawImage Profil_computer;
    public RawImage Profil_server;
    public Image Vie;
    public Image Mana;
    public Text Name;
    public Text Niveau;

    bool canEscape = true;


    void OnGUI()
    {

        #region Initialization
        //bar button
        Bar.rectTransform.sizeDelta = new Vector2(562, 104);
        Bar.transform.position = new Vector2(Screen.width / 2, 54);
        Bar.enabled = true;

        //menu_image
        Menu_Image.rectTransform.sizeDelta = new Vector2(500, 300);
        Menu_Image.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        //vie
        if (PlayerPrefs.GetString("Classe") == "Computer")
        {
            Profil_computer.enabled = true;
            Profil_computer.rectTransform.sizeDelta = new Vector2(350, 100);
            Profil_computer.transform.position = new Vector2(10 + 175, Screen.height - 10 - 50);
        }
        else if (PlayerPrefs.GetString("Classe") == "Laptop")
        {
            Profil_laptop.enabled = true;
            Profil_laptop.rectTransform.sizeDelta = new Vector2(350, 100);
            Profil_laptop.transform.position = new Vector2(10 + 175, Screen.height - 10 - 50);
        }
        else
        {
            Profil_server.enabled = true;
            Profil_server.rectTransform.sizeDelta = new Vector2(350, 100);
            Profil_server.transform.position = new Vector2(10 + 175, Screen.height - 10 - 50);
        }
        Vie.enabled = true;
        Mana.enabled = true;
        Niveau.enabled = true;
        Name.enabled = true;
        Vie.transform.position = new Vector2(10 + 100 + 102, Screen.height - 10 - 44);
        Mana.transform.position = new Vector2(10 + 100 + 102, Screen.height - 10 - 52);
        Niveau.transform.position = new Vector2(10 + 82 + 102, Screen.height - 10 - 41);
        Name.transform.position = new Vector2(10 + 82 + 102, Screen.height - 10 - 30);
        Name.text = PlayerPrefs.GetString("Pseudo") == "" ? "Bob" : PlayerPrefs.GetString("Pseudo");

        //button
        for (int i = 0; i < ListGuiButtonAttack.Count; i++)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 261 + (i + 1) * 2 + i * (50), Screen.height - 59, 50, 50), "") && Game) // ajouter GUIStyle des button
            {
                //faire les attaque en fonction de i
            }
        }
        #endregion



        if (Game)
        {
            Menu_Image.enabled = false;
            if (GUI.Button(new Rect(Screen.width / 2 + 261 + 2, Screen.height - 27, 25, 25), "", ListGuiButton[1]))
            {
                Game = false;
                Menu_ = true;
            }
            if (canEscape && Input.GetKey(KeyCode.Escape))
            {
                Game = false;
                Menu_ = true;
                StartCoroutine(WaitForEscape());
            }
        }


        if (Menu_)
        {
            Cursor.visible = true;
            Menu_Image.enabled = true;
            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 100, 120, 50), "Menu Principal", ListGuiButton[0]))
            {

            }
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 40, 100, 50), "Retour", ListGuiButton[0]))
            {
                Menu_ = false;
                Game = true;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 20, 100, 50), "Option", ListGuiButton[0]))
            {
                Menu_ = false;
                Option = true;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 80, 100, 50), "Quitter", ListGuiButton[0]))
            {
                Application.Quit();
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 261 + 2, Screen.height - 27, 25, 25), "", ListGuiButton[1]))
            {
                Game = true;
                Menu_ = false;
            }

            if(canEscape && Input.GetKey(KeyCode.Escape))
            {
                Menu_ = false;
                Game = true;
                StartCoroutine(WaitForEscape());
            }
        }

        if (Option)
        {
            Option_Titre.enabled = true;

            //Audio
            sfxVol = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50 + 60, Screen.height / 2, 100, 30), sfxVol, (float)0.0, (float)100.0);
            GUI.Label(new Rect(Screen.width / 2 - 50 + 110 + 60, Screen.height / 2 - 5, 130, 30), "Effets sonores " + (int)sfxVol + " %");

            musicVol = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50 + 60, Screen.height / 2 + 30, 100, 30), musicVol, (float)0.0, (float)100.0);
            GUI.Label(new Rect(Screen.width / 2 - 50 + 110 + 60, Screen.height / 2 + 25, 100, 30), "Musique " + (int)musicVol + " %");
            Music_Ambiance.volume = musicVol / 100;



            //Video
            var qualities = QualitySettings.names;

            GUILayout.BeginVertical();

            for (int i = 0; i < qualities.Length; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 50 - 175, Screen.height / 2 - 120 + i * 30 + 75 - 50 + 20, 100, 30), qualities[i], ListGuiButton[0]))
                {
                    QualitySettings.SetQualityLevel(i, true);
                }
            }

            GUILayout.EndVertical();


            fullscreen = GUI.Toggle(new Rect(Screen.width / 2 - 50 + 60, Screen.height / 2 - 30, 125, 25), fullscreen, "Plein Ecran");


            Resolution[] resolutions = Screen.resolutions;
            if (GUI.Button(new Rect(Screen.width / 2 - 50 - 75, Screen.height / 2 - 120 + 25 + 20, 100, 30), Screen.width + "x" + Screen.height, ListGuiButton[0]))
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
                    if (GUI.Button(new Rect(Screen.width / 2 - 50 - 75, Screen.height / 2 - 120 + i * 30 + 55 + 20, 100, 30), resolutions[i].width + "x" + resolutions[i].height, ListGuiButton[0]))
                    {
                        if (fullscreen)
                            Screen.SetResolution(resolutions[i].width, resolutions[i].height, true);
                        else
                            Screen.SetResolution(resolutions[i].width, resolutions[i].height, false);
                        resolution = false;
                    }
                }
            }




            if (GUI.Button(new Rect(Screen.width / 2 + 100, Screen.height / 2 + 75, 100, 50), "Retour", ListGuiButton[0]))
            {
                PlayerPrefs.SetInt("Son", (int)musicVol);
                Option_Titre.enabled = false;
                if (fullscreen)
                    Screen.SetResolution(Screen.width, Screen.height, true);
                else
                    Screen.SetResolution(Screen.width, Screen.height, false);
                Menu_ = true;
                Option = false;
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 261 + 2, Screen.height - 27, 25, 25), "", ListGuiButton[1]))
            {
                Game = true;
                Option = false;
            }
        }



    }

    IEnumerator WaitForEscape()
    {
        canEscape = false;
        yield return new WaitForSeconds(0.2f);
        canEscape = true;
    }

    public bool MenuOpen
    {
        get { return Menu_; }
    }


    // Use this for initialization
    void Start()
    {
        fullscreen = Screen.fullScreen;
        musicVol = PlayerPrefs.GetInt("Son");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
