using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class toFirst : MonoBehaviour
{

    void OnTriggerEnter(Collider coll)
    {
        SceneManager.LoadScene("first");
    }
}
