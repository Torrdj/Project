using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

    public enum TYPES
    {
        Laptop,
        Computer,
        Server
    }

    [SerializeField]
    Transform[] listOfPrefabs;

    public TYPES type;
    public string _name;
    public float vie, vieMax;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnLevelWasLoaded()
    {
        int i;
        switch(type)
        {
            case TYPES.Laptop:
                i = 0;
                break;
            case TYPES.Computer:
                i = 1;
                break;
            case TYPES.Server:
                i = 2;
                break;
            default:
                throw new System.IndexOutOfRangeException();
        }
        GameObject.Find("NetworkHolder").GetComponent<NetworkController>().prefab = listOfPrefabs[i];
    }
}
