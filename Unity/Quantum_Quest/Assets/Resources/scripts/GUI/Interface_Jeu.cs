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

    bool canEscape = true;


    void OnGUI()
    {
        //bar button
        Bar.rectTransform.sizeDelta = new Vector2(562, 104);
        Bar.transform.position = new Vector2(Screen.width / 2, 54);
        Bar.enabled = true;

        //button
        for (int i = 0; i < ListGuiButtonAttack.Count; i++)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 261 + (i + 1) * 2 + i * (50), Screen.height - 59, 50, 50), "") && Game) // ajouter GUIStyle des button
            {
                //faire les attaque en fonction de i
            }
        }

        if (Game)
        {
            Menu_Image.enabled = false;
            Option_Titre.enabled = false;
            if (GUI.Button(new Rect(Screen.width / 2 + 261 + 2 + 10, Screen.height - 27, 25, 25), "", ListGuiButton[1]))
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
            Option_Titre.text = "Menu";
            Option_Titre.enabled = true;
            Cursor.visible = true;
            Menu_Image.enabled = true;
            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 60, 120, 30), "Menu Principal", ListGuiButton[0]))
            {

            }
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 20, 100, 30), "Retour", ListGuiButton[0]))
            {
                Option_Titre.enabled = false;
                Menu_ = false;
                Game = true;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 20, 100, 30), "Option", ListGuiButton[0]))
            {
                Menu_ = false;
                Option = true;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 60, 100, 30), "Quitter", ListGuiButton[0]))
            {
                Application.Quit();
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 261 + 2 + 10, Screen.height - 27, 25, 25), "", ListGuiButton[1]))
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
            Option_Titre.text = "Options";

            //Audio
            sfxVol = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50 + 45, Screen.height / 2, 100, 30), sfxVol, (float)0.0, (float)100.0);
            GUI.Label(new Rect(Screen.width / 2 - 50 + 110 + 45, Screen.height / 2 - 5, 130, 30), "Effets sonores " + (int)sfxVol + " %");

            musicVol = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50 + 45, Screen.height / 2 + 30, 100, 30), musicVol, (float)0.0, (float)100.0);
            GUI.Label(new Rect(Screen.width / 2 - 50 + 110 + 45, Screen.height / 2 + 25, 100, 30), "Musique " + (int)musicVol + " %");
            Music_Ambiance.volume = musicVol / 100;



            //Video
            var qualities = QualitySettings.names;

            GUILayout.BeginVertical();

            for (int i = 0; i < qualities.Length; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 50 - 175 + 10, Screen.height / 2 - 120 + i * 30 + 75 - 50 + 10, 100, 30), qualities[i], ListGuiButton[0]))
                {
                    QualitySettings.SetQualityLevel(i, true);
                }
            }

            GUILayout.EndVertical();


            fullscreen = GUI.Toggle(new Rect(Screen.width / 2 - 50 + 55, Screen.height / 2 - 30, 125, 25), fullscreen, "Plein Ecran");


            Resolution[] resolutions = Screen.resolutions;
            if (GUI.Button(new Rect(Screen.width / 2 - 50 - 75 + 10, Screen.height / 2 - 120 + 25 + 10, 100, 30), Screen.width + "x" + Screen.height, ListGuiButton[0]))
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
                    if (GUI.Button(new Rect(Screen.width / 2 - 50 - 75+10, Screen.height / 2 - 120 + i * 30 + 55 + 10, 100, 30), resolutions[i].width + "x" + resolutions[i].height, ListGuiButton[0]))
                    {
                        if (fullscreen)
                            Screen.SetResolution(resolutions[i].width, resolutions[i].height, true);
                        else
                            Screen.SetResolution(resolutions[i].width, resolutions[i].height, false);
                        resolution = false;
                    }
                }
            }




            if (GUI.Button(new Rect(Screen.width / 2 + 100, Screen.height / 2 + 60, 100, 30), "Retour", ListGuiButton[0]))
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
            if (GUI.Button(new Rect(Screen.width / 2 + 261 + 2 + 10, Screen.height - 27, 25, 25), "", ListGuiButton[1]))
            {
                Game = true;
                Option = false;
            }

            if (canEscape && Input.GetKey(KeyCode.Escape))
            {
                Option = false;
                Game = true;
                StartCoroutine(WaitForEscape());
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
        get { return Menu_ || Option; }
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
