using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour
{
    public GameObject perso;

    void Awake()
    {
        if (Network.isClient || Network.isServer)
        {
            Network.Instantiate(perso, new Vector3(35, 0.13f, 11.6f), new Quaternion(), 0);
        }
        else
        {
            Instantiate(perso, new Vector3(35, 0.13f, 11.6f), Quaternion.identity);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
