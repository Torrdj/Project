using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class changeScene : NetworkBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<Personnage>().isLocalPlayer)
        {
            Transform scene = gameObject.transform.parent;
            Vector3 pos = player.transform.position;
            if (scene.name == "Map1")
            {
                pos.y -= 0.5f;
                pos.z -= 15;
            }
            else
            {
                pos.y += 1;
                pos.z += 15;
            }
            player.transform.position = pos;
        }
    }
}
