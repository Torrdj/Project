using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    RawImage Load_Bordure;
    Image load;
    float progress;
    string _scene;

    // Use this for initialization
    void Start()
    {
        _scene = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>().SceneToLoad;
        Load_Bordure = GameObject.Find("LoadingBordure").GetComponent<RawImage>();
        load = GameObject.Find("LoadingBar").GetComponent<Image>();
        SceneManager.LoadSceneAsync(_scene);
    }

    // Update is called once per frame
    void Update()
    {
        Load_Bordure.transform.position = new Vector2(Screen.width / 2, 20);
        progress = Application.GetStreamProgressForLevel(_scene);
        load.rectTransform.sizeDelta = new Vector2(490 * progress, load.rectTransform.sizeDelta.y);
        load.transform.position = new Vector2(Screen.width / 2 - 245 + load.rectTransform.sizeDelta.x / 2, 20);

        if (progress == 1)
        {
            SceneManager.LoadScene(_scene);
        }
    }
}
