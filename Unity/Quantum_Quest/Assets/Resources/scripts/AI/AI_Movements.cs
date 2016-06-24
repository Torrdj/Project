using UnityEngine;
using System.Collections;

public class AI_Movements : MonoBehaviour
{
    float t;
    // Use this for initialization
    void Start()
    {
        t = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
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
            if(hit.distance < 2)
            {
                transform.Rotate(transform.rotation.x, new System.Random().Next(45, 316), transform.rotation.z);
            }
        }
        transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
