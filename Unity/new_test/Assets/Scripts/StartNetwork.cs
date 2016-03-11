using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartNetwork : MonoBehaviour
{

    public bool server;
    public int listenPort = 25000;
    public string remoteIP;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {

        if (server)
        {
            Network.InitializeServer(32, listenPort, !Network.HavePublicAddress()); //le false signifie qu'on utilise pas le Nat punchtrough. Je vous recommande la doc d'Unity pour en savoir plus

            // On préviens tous nos objets que le réseau est lancé
            foreach (var go in FindObjectsOfType<GameObject>())
                go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            Network.Connect(remoteIP, listenPort);
        }

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
        if (SceneManager.GetActiveScene().name == "mainMenu")
            Destroy(this.gameObject);
        // Notify our objects that the level and the network are ready
        foreach (var go in FindObjectsOfType<GameObject>())
            go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
