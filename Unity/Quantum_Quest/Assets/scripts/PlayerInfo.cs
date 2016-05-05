using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

    public string[] prefab_name;
    
    public void addToTab(string s)
    {
        string[] result = new string[prefab_name.Length + 1];
        result[0] = s;
        for (int i = 1; i < result.Length; i++)
        {
            result[i] = prefab_name[i - 1];
        }
        prefab_name = result;
    }
}
