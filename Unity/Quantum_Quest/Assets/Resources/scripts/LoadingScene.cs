using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{

    RectTransform load;

    // Use this for initialization
    void Start()
    {
        load = GameObject.Find("LoadingBar").GetComponent<RectTransform>();
        SceneManager.LoadSceneAsync("first");
    }

    // Update is called once per frame
    void Update()
    {
        float progress = Application.GetStreamProgressForLevel("first");
        load.sizeDelta = new Vector2(load.sizeDelta.x * progress, load.sizeDelta.y);

        if (progress == 1)
        {
            SceneManager.LoadScene("first");
        }
    }
}
