using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class StartNetwork : NetworkBehaviour
{

    public bool server;
    public int listenPort = 25000;
    public string remoteIP;

    [SerializeField]
    GameObject player;
    GameObject playerInst;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {

        if (server)
        {
            Network.InitializeServer(32, listenPort, !Network.HavePublicAddress());

            // On préviens tous nos objets que le réseau est lancé
            foreach (var go in FindObjectsOfType<GameObject>())
                go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            Network.Connect(remoteIP, listenPort);
        }

        SceneManager.LoadScene("Game");
    }


    void OnPlayerConnected(NetworkPlayer player)
    {
        if (server)
        {
            print("Connecté !");
        }
    }

    void OnLevelWasLoaded()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
            Destroy(this.gameObject);
        // Notify our objects that the level and the network are ready
        foreach (var go in FindObjectsOfType<GameObject>())
            go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);

        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawn");
        GameObject spawn = spawners[new System.Random().Next(0, spawners.Length)];

        Debug.Log("Here ?");
        if (!isServer)
        {
            new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.Sleep(3000);
                    playerInst = Network.Instantiate(player, spawn.transform.position, Quaternion.identity, 0) as GameObject;
                }).Start();
            Debug.Log("Server");
        }
        else
        {
            playerInst = Network.Instantiate(player, spawn.transform.position, Quaternion.identity, 0) as GameObject;
            Debug.Log("Not Server");
        }
        Debug.Log("or not ?");
    }


    // Update is called once per frame
    void Update()
    {

    }
}
