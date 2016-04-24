using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class to1 : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        SceneManager.LoadScene("1");
    }
}
