using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour
{

    public enum TYPES
    {
        Laptop,
        Computer,
        Server
    }

    [SerializeField]
    Transform[] listOfPrefabs;

    TYPES _type;
    string _name;
    float _vie, _vieMax,
        _mana, _manaMax;
    string sceneToLoad;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void OnLevelWasLoaded()
    {
        int i;
        switch (_type)
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

    #region GETTERS/SETTERS
    public TYPES Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public float Vie
    {
        get { return _vie; }
        set { _vie = value; }
    }

    public float VieMax
    {
        get { return _vieMax; }
        set { _vieMax = value; }
    }

    public float Mana
    {
        get { return _mana; }
        set { _mana = value; }
    }

    public float ManaMax
    {
        get { return _manaMax; }
        set { _manaMax = value; }
    }

    public string SceneToLoad
    {
        get { return sceneToLoad; }
        set { sceneToLoad = value; }
    }
    #endregion
}
