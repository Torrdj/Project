using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour
{
    public GameObject perso;

    void Awake()
    {
        if (Network.isClient || Network.isServer)
        {
            Network.Instantiate(perso, new Vector3(28, 1.38f, 28.9f), new Quaternion(), 0);
        }
        else
        {
            Instantiate(perso, new Vector3(28, 1.38f, 28.9f), new Quaternion());
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
