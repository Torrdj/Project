using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerState : MonoBehaviour
{
    private int viewID = -1;
    private Personnages info;

    private PlayerInfo playerinfo;

    public RawImage Profil_laptop;
    public RawImage Profil_computer;
    public RawImage Profil_server;
    public Image Vie;
    public Image Mana;
    public Text Name;
    public Text Niveau;

    private List<bool> list_buff = new List<bool>(6);
    public List<GUIStyle> list_GuistyleBuff;

    // Use this for initialization
    void Start()
    {
        playerinfo = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (viewID != -1)
        {
            info = PhotonView.Find(viewID).GetComponent<Personnages>();
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
                    if (GUI.Button(new Rect(5 + (i + 1) * 2 + i * (25), 100, 25, 25), "", list_GuistyleBuff[a]))
                    {

                    }
                    i += 1;
                }
            }
        }


        Vie.rectTransform.sizeDelta = new Vector2(197 * (playerinfo.Vie / playerinfo.VieMax), 6);
        if (Vie.rectTransform.sizeDelta.x >= 0)
            Vie.transform.position = new Vector2(10 + 104 + Vie.rectTransform.sizeDelta.x / 2, Screen.height - 10 - 43);

        //vie
        if (playerinfo.Type == PlayerInfo.TYPES.Computer)
        {
            Profil_computer.enabled = true;
            Profil_computer.rectTransform.sizeDelta = new Vector2(350, 100);
            Profil_computer.transform.position = new Vector2(10 + 175, Screen.height - 10 - 50);
        }
        else if (playerinfo.Type == PlayerInfo.TYPES.Laptop)
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


        Mana.transform.position = new Vector2(10 + 100 + 102, Screen.height - 10 - 52);
        Niveau.transform.position = new Vector2(10 + 82 + 102, Screen.height - 10 - 41);
        Name.transform.position = new Vector2(10 + 82 + 102, Screen.height - 10 - 30);
        Name.text = playerinfo.Name == "" ? "Bob" : playerinfo.Name;
    }

    public int ViewID
    {
        get { return viewID; }
        set { viewID = value; }
    }

}
