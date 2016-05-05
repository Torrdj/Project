using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class selectPrefab : NetworkBehaviour
{
    // Use this for initialization
    void Start()
    {
        Debug.Log(GameObject.Find("NetworkManager").GetComponent<PlayerInfo>().prefab_name);
        switch (GameObject.Find("NetworkManager").GetComponent<PlayerInfo>().prefab_name)
        {
            case "Laptop_player":
                activePrefab(GameObject.FindGameObjectsWithTag("Player"), "Laptop_player");
                break;
            case "pc_player":
                activePrefab(GameObject.FindGameObjectsWithTag("Player"), "pc_player");
                break;
            case "Server1_player":
                activePrefab(GameObject.FindGameObjectsWithTag("Player"), "Server1_player");
                break;
            default:
                break;
        }
    }

    void activePrefab(GameObject[] aux, string pref_name)
    {
        foreach (GameObject a in aux)
        {
            if (isLocalPlayer
                && a.name != "Player(Clone)"
                && a.name != pref_name)
                a.gameObject.SetActive(false);
        }
    }
}
