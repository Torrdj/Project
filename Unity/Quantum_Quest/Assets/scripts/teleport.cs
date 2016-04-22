using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class teleport : NetworkBehaviour {
    NetworkManager nt;

    void Start()
    {
        nt = FindObjectOfType<NetworkManager>();
    }

    void OnTriggerEnter(Collider coll)
    {
        //nt.onlineScene = "Game2";
        nt.ServerChangeScene("Game2");
        //SceneManager.LoadScene("Game2");
    }
}
