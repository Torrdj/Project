using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetState : MonoBehaviour {

    public RawImage Profil_Ennemi;

    public Image Vie_ennemi;
    public Image Mana_ennemi;
    public Text Name_ennemi;
    public Text Niveau_ennemi;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        Vie_ennemi.transform.position = new Vector2(Screen.width - 10 - 104 - Vie_ennemi.rectTransform.sizeDelta.x / 2, Screen.height - 10 - 43);

        Profil_Ennemi.enabled = true;
        Profil_Ennemi.rectTransform.sizeDelta = new Vector2(350, 100);
        Profil_Ennemi.transform.position = new Vector2(Screen.width - 10 - 175, Screen.height - 10 - 50);

        Mana_ennemi.transform.position = new Vector2(Screen.width - 10 - 100 - 103, Screen.height - 10 - 52);
        Niveau_ennemi.transform.position = new Vector2(Screen.width - 32 - 82 - 102, Screen.height - 10 - 41);
        Name_ennemi.transform.position = new Vector2(Screen.width - 32 - 82 - 102, Screen.height - 10 - 30);

        Vie_ennemi.enabled = true;
        Mana_ennemi.enabled = true;
        Niveau_ennemi.enabled = true;
        Name_ennemi.enabled = true;
    }
}
