using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        coll.GetComponent<Personnage>().receiveDamages(50);
        Destroy(this.gameObject);
    }
}
