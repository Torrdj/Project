using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerState : MonoBehaviour {

    private PlayerInfo playerinfo;

    public RawImage Profil_laptop;
    public RawImage Profil_computer;
    public RawImage Profil_server;
    public Image Vie;
    public Image Mana;
    public Text Name;
    public Text Niveau;

    // Use this for initialization
    void Start () {
        playerinfo = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {

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
}
