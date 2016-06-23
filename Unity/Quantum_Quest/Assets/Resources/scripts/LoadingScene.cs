using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    RawImage Load_Bordure;
    Image load;
    float progress;

    // Use this for initialization
    void Start()
    {
        Load_Bordure = GameObject.Find("LoadingBordure").GetComponent<RawImage>();
        load = GameObject.Find("LoadingBar").GetComponent<Image>();
        SceneManager.LoadSceneAsync("first");
    }

    // Update is called once per frame
    void Update()
    {
        Load_Bordure.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 - 178);
        progress = Application.GetStreamProgressForLevel("first");
        load.rectTransform.sizeDelta = new Vector2(490 * progress, load.rectTransform.sizeDelta.y);
        load.transform.position = new Vector2(Screen.width / 2 - 245 + load.rectTransform.sizeDelta.x / 2, Screen.height / 2 - 178);

        if (progress == 1)
        {
            SceneManager.LoadScene("first");
        }
    }
}
