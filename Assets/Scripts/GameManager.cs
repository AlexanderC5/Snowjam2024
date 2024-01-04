using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public string zone;
    //[SerializeField]
    //public Unity[] Units;

    //private GameObject[] availableObstacles;
    //[MenuItem("AssetDatabase/Get Folder List")]
    // Start is called before the first frame update
    void Start()
    {
        string[] guids1 = AssetDatabase.FindAssets("Zone1Obs", null);
        foreach (string guid1 in guids1)
        {
            Debug.Log(AssetDatabase.GUIDToAssetPath(guid1));
        }
        //Object[] prefabs = Resources.LoadAll<Object>("Assets/Prefabs/Obstacles/Zone1");
        //Debug.Log(prefabs.Length);
        //zone = "Zone1";
        //Object[] skinObjects = Resources.LoadAll("Assets/Prefabs/Obstacles");
        // Get length of list
        //int amountOfSkins = skinObjects.Length;
        //Debug.Log(amountOfSkins);
        //var folders = AssetDatabase.GetSubFolders("Assets\\Prefabs\\Obstacles\\" + zone);
        /*availableObstacles = Resources.LoadAll("Assets\\Prefabs\\Obstacles\\" + zone);
        foreach (var t in availableObstacles)
        {
            Debug.Log(availableObstacles);
        }
        int amount = availableObstacles.Length;
        Debug.Log(amount);*/
        /*foreach (var folder in folders)
        {
            Debug.Log(folder);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
