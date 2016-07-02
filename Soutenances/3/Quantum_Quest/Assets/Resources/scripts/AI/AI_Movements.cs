using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_Movements : MonoBehaviour
{
    float t;
    public int attaquant = -1;

    // Use this for initialization
    void Start()
    {
        t = Time.fixedTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GetComponent<AIPersonnages>().isDead && attaquant == -1)
        {
            float nt;
            if ((nt = Time.fixedTime) - t >= 5)
            {
                transform.Rotate(transform.rotation.x, new System.Random().Next(45, 316), transform.rotation.z);
                t = nt;
            }

            RaycastHit hit;

            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.distance < 2)
                {
                    transform.Rotate(transform.rotation.x, new System.Random().Next(45, 316), transform.rotation.z);
                }
            }
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        else if(attaquant != -1)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, PhotonView.Find(attaquant).transform.position);

            //transform.Rotate(PhotonView.Find(attaquant).transform.rotation.eulerAngles - transform.rotation.eulerAngles);

            if(Physics.Raycast(ray, out hit))
            {
                //if (hit.distance > 2)
                    //transform.Translate(Vector3.forward * Time.deltaTime * 7);
            }
        }
    }
    
}
