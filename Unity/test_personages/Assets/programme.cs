using UnityEngine;
using System.Collections;

public class programme : MonoBehaviour {
    public GameObject Perso;
	// Use this for initialization
	void Awake () {
        Instantiate(Perso, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
