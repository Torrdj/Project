using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    //GameObject inventory;
	// Use this for initialization
	void Start () {
        //inventory = GameObject.Find("Inventaire");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ToogleInventory()
    {
        if (gameObject.GetComponent<Image>().enabled)
        {
            gameObject.GetComponent<Image>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
