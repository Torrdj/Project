using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class selectPrefab : NetworkBehaviour
{
    [SerializeField]
    GameObject[] listOfPrefab;

    // Use this for initialization
    void Start()
    {
        if(isLocalPlayer)
        {
            selectMyPrefab();
        }
    }
    public void selectMyPrefab()
    {
        GameObject player;
        //BoxCollider[] bo = GetComponents<BoxCollider>();
        switch (GameObject.Find("NetworkManager").GetComponent<PlayerInfo>().prefab_name)
        {
            case "Laptop_player":
                player = (GameObject)Instantiate(listOfPrefab[0], transform.position, transform.rotation);
                //player.transform.parent = transform;
                NetworkServer.Spawn(player);
                //bo[0].enabled = true;
                //activeMyPrefab(GetComponentsInChildren<Transform>(), "Laptop_player");
                break;
            case "pc_player":
                player = (GameObject)Instantiate(listOfPrefab[1], transform.position, transform.rotation);
                //player.transform.parent = transform;
                NetworkServer.Spawn(player);
                //bo[1].enabled = true;
                //activeMyPrefab(GetComponentsInChildren<Transform>(), "pc_player");
                break;
            case "Server1_player":
                player = (GameObject)Instantiate(listOfPrefab[2], transform.position, transform.rotation);
                //player.transform.parent = transform;
                NetworkServer.Spawn(player);
                //bo[2].enabled = true;
                //activeMyPrefab(GetComponentsInChildren<Transform>(), "Server1_player");
                break;
            default:
                break;
        }
    }

    [Command]
    public void CmdSelectEnnemiPrefab()
    {
        GameObject player;
        BoxCollider[] bo = GetComponents<BoxCollider>();
        switch (GameObject.Find("NetworkManager").GetComponent<PlayerInfo>().prefab_name)
        {
            case "Laptop_player":
                player = (GameObject)Instantiate(listOfPrefab[0], transform.position, transform.rotation);
                player.transform.parent = transform;
                NetworkServer.Spawn(player);
                bo[0].enabled = true;
                activePrefabEnnemi(GetComponentsInChildren<Transform>(), "Laptop_player");
                break;
            case "pc_player":
                player = (GameObject)Instantiate(listOfPrefab[1], transform.position, transform.rotation);
                player.transform.parent = transform;
                NetworkServer.Spawn(player);
                bo[1].enabled = true;
                activePrefabEnnemi(GetComponentsInChildren<Transform>(), "pc_player");
                break;
            case "Server1_player":
                player = (GameObject)Instantiate(listOfPrefab[2], transform.position, transform.rotation);
                player.transform.parent = transform;
                NetworkServer.Spawn(player);
                bo[2].enabled = true;
                activePrefabEnnemi(GetComponentsInChildren<Transform>(), "Server1_player");
                break;
            default:
                break;
        }
    }

    public void activeMyPrefab(Transform[] trs, string pref_name)
    {
        foreach (Transform tr in trs)
        {
            if (tr.name != "Player(Clone)"
                && tr.name != "Camera"
                && tr.name != pref_name)
                tr.gameObject.SetActive(false);
        }
    }
    public void activePrefabEnnemi(Transform[] trs, string pref_name)
    {
        foreach (Transform tr in trs)
        {
            if (tr.name != "Player(Clone)"
                && tr.name != pref_name)
                tr.gameObject.SetActive(false);
        }
    }
}
