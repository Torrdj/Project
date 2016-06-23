using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayMovie : MonoBehaviour {

    public MovieTexture movie;
    public GameObject canvas;
    Vector2 oldSize;

	void Start () {
        oldSize = GetComponent<RectTransform>().sizeDelta;
        GetComponent<RectTransform>().sizeDelta = new Vector2(oldSize.x * 0.96f, oldSize.y * 0.54f);

        GetComponent<RawImage>().texture = movie as MovieTexture;
        movie.Play();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)
            || Input.GetKeyDown(KeyCode.Space) 
            || !movie.isPlaying)
        {
            gameObject.SetActive(false);
            canvas.SetActive(true);
            GameObject.Find("Main Camera").GetComponent<Menu>().enabled = true;
        }
	}
}
