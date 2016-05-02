using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float damages;

    void OnTriggerEnter(Collider coll)
    {
        coll.GetComponent<Movements>().receiveDamages(50);
        Destroy(this.gameObject);
    }
}
