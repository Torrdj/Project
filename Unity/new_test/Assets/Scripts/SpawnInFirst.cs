using UnityEngine;
using System.Collections;

public class SpawnInFirst : MonoBehaviour
{
    public GameObject perso;

    void Awake()
    {
        if (Network.isClient || Network.isServer)
        {
            Network.Instantiate(perso, new Vector3(-4, 0f, -48.8f), new Quaternion(), 0);
        }
        else
        {
            Instantiate(perso, new Vector3(-4, 0f, -48.8f), Quaternion.identity);
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
