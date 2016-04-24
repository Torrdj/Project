using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
    Rigidbody body;
    public GameObject perso;
    int speed = 10;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        //perso = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        //float mouveHorizontal = Input.GetAxis("Horizontal");
        //float mouveVertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Z))
        {
            body.transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            body.AddForce(new Vector3(0, 0, -1) * speed * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            body.AddForce(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            body.AddForce(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        }

        //Vector3 mouvment = new Vector3(mouveHorizontal, 0, mouveVertical);
        //GetComponent<Rigidbody>().AddForce(mouvment * speed * Time.deltaTime);
    }
}
