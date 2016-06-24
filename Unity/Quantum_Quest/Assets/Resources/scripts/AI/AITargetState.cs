using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AITargetState : MonoBehaviour
{
    private int viewID = -1;

    public RawImage Profil_laptop;
    public RawImage Profil_computer;
    public RawImage Profil_server;

    private GameObject cible;
    private AIPersonnages info;

    public Image Vie_ennemi;
    public Image Mana_ennemi;
    public Text Name_ennemi;
    public Text Niveau_ennemi;

    private List<bool> list_buff = new List<bool>(6);
    public List<GUIStyle> list_GuistyleBuff;

    // Use this for initialization
    void FixedUpdate()
    {
        if (viewID != -1)
        {
            info = PhotonView.Find(viewID).GetComponent<AIPersonnages>();
        }
    }

    void OnGUI()
    {
        if (viewID != -1 && info != null)
        {
            list_buff = new List<bool>(6);
            list_buff.Add(info.turbo_boost_isactiavte);
            list_buff.Add(info.Dot_isactive);
            list_buff.Add(info.def_isreduced);
            list_buff.Add(info.speed_isreduced);
            list_buff.Add(info.m_isParalyzed);
            list_buff.Add(info.def_isincrese);
            int i = 0;
            for (int a = 0; a < list_buff.Count; a += 1)
            {
                if (list_buff[a])
                {
                    if (GUI.Button(new Rect(Screen.width - 350 + (i + 1) * 2 + i * (25), 110, 25, 25), "", list_GuistyleBuff[a]))
                    {

                    }
                    i += 1;
                }
            }

            //vie
            Vie_ennemi.rectTransform.sizeDelta = new Vector2(197 * (info.Vie / info.VieMax), 6);
            if (Vie_ennemi.rectTransform.sizeDelta.x >= 0)
                Vie_ennemi.transform.position = new Vector2(Screen.width - 10 - 104 - Vie_ennemi.rectTransform.sizeDelta.x / 2, Screen.height - 10 - 43);

            //mana
            Mana_ennemi.rectTransform.sizeDelta = new Vector2(197 * (info.Mana / info.ManaMax), 6);
            if (Mana_ennemi.rectTransform.sizeDelta.x >= 0)
                Mana_ennemi.transform.position = new Vector2(Screen.width - 10 - 104 - Mana_ennemi.rectTransform.sizeDelta.x / 2, Screen.height - 10 - 52);

            if (info.Type == PlayerInfo.TYPES.Computer)
            {
                Profil_computer.enabled = true;
                Profil_computer.rectTransform.sizeDelta = new Vector2(350, 100);
                Profil_computer.transform.position = new Vector2(Screen.width - 10 - 175, Screen.height - 10 - 50);
            }
            else if (info.Type == PlayerInfo.TYPES.Laptop)
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

            //Mana_ennemi.transform.position = new Vector2(Screen.width - 10 - 100 - 103, Screen.height - 10 - 52);
            Niveau_ennemi.transform.position = new Vector2(Screen.width - 32 - 82 - 102, Screen.height - 10 - 41);
            Name_ennemi.transform.position = new Vector2(Screen.width - 32 - 82 - 102, Screen.height - 10 - 30);
            Name_ennemi.text = info.m_name == "" ? "Bob" : info.m_name;
        }


    }

    public int ViewID
    {
        get { return viewID; }
        set { viewID = value; }
    }

}
