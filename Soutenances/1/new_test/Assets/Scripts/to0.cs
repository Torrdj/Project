using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class to0 : MonoBehaviour
{

    void OnTriggerEnter(Collider coll)
    {
        SceneManager.LoadScene("0");
    }
}
