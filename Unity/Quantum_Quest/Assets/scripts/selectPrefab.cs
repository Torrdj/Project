using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class selectPrefab : NetworkBehaviour
{/*
    // Use this for initialization
    void Start()
    {
        BoxCollider[] bo = GetComponents<BoxCollider>();
        switch (GameObject.Find("NetworkManager").GetComponent<PlayerInfo>().prefab_name)
        {
            case "Laptop_player":
                bo[0].enabled = true;
                activePrefab(GetComponentsInChildren<Transform>(), "Laptop_player");
                break;
            case "pc_player":
                bo[1].enabled = true;
                activePrefab(GetComponentsInChildren<Transform>(), "pc_player");
                break;
            case "Server1_player":
                bo[2].enabled = true;
                activePrefab(GetComponentsInChildren<Transform>(), "Server1_player");
                break;
            default:
                break;
        }
    }

    void activePrefab(Transform[] trs, string pref_name)
    {
        foreach (Transform tr in trs)
        {
            if (isLocalPlayer
                && tr.name != "Player(Clone)"
                && tr.name != "Camera"
                && tr.name != pref_name)
                tr.gameObject.SetActive(false);
        }
    }

    string GetPrefabName
    {
        get { return GameObject.Find("NetworkManager").GetComponent<PlayerInfo>().prefab_name; }
    }*/
}
