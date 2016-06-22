using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetState : MonoBehaviour
{
    private int viewID;

    public RawImage Profil_laptop;
    public RawImage Profil_computer;
    public RawImage Profil_server;

    private GameObject cible;
    private Personnages info;

    public Image Vie_ennemi;
    public Image Mana_ennemi;
    public Text Name_ennemi;
    public Text Niveau_ennemi;

    // Use this for initialization
    void Start()
    {
        if (viewID != 0)
        {
            cible = PhotonView.Find(viewID).gameObject;
            info = cible.GetComponent<Personnages>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (viewID != 0 && info != null)
        {
            Vie_ennemi.rectTransform.sizeDelta = new Vector2(197 * (info.Vie / info.VieMax), 6);
            if (Vie_ennemi.rectTransform.sizeDelta.x >= 0)
                Vie_ennemi.transform.position = new Vector2(Screen.width - 10 - 104 - Vie_ennemi.rectTransform.sizeDelta.x / 2, Screen.height - 10 - 43);

            if (info.Types == PlayerInfo.TYPES.Computer)
            {
                Profil_computer.enabled = true;
                Profil_computer.rectTransform.sizeDelta = new Vector2(350, 100);
                Profil_computer.transform.position = new Vector2(Screen.width - 10 - 175, Screen.height - 10 - 50);
            }
            else if (info.Types == PlayerInfo.TYPES.Laptop)
            {
                Profil_laptop.enabled = true;
                Profil_laptop.rectTransform.sizeDelta = new Vector2(350, 100);
                Profil_laptop.transform.position = new Vector2(Screen.width - 10 - 175, Screen.height - 10 - 50);
            }
            else
            {
                Profil_server.enabled = true;
                Profil_server.rectTransform.sizeDelta = new Vector2(350, 100);
                Profil_server.transform.position = new Vector2(Screen.width - 10 - 175, Screen.height - 10 - 50);
            }

            Vie_ennemi.enabled = true;
            Mana_ennemi.enabled = true;
            Niveau_ennemi.enabled = true;
            Name_ennemi.enabled = true;

            Mana_ennemi.transform.position = new Vector2(Screen.width - 10 - 100 - 103, Screen.height - 10 - 52);
            Niveau_ennemi.transform.position = new Vector2(Screen.width - 32 - 82 - 102, Screen.height - 10 - 41);
            Name_ennemi.transform.position = new Vector2(Screen.width - 32 - 82 - 102, Screen.height - 10 - 30);
            Name_ennemi.text = info.m_name == "" ? "Bob" : info.m_name;
        }


    }

    public int ViewID
    {
        set { viewID = value; }
    }

}
