using System.IO;
using UnityEngine;
using SimpleJSON;

public class FishParser : MonoBehaviour
{
    private JSONNode _jsonObj;
    private string _path;
    [SerializeField] Material material;

    void Start()
    {
        _path = Application.streamingAssetsPath + "/" + "Fish.json";
        _jsonObj = JSON.Parse(File.ReadAllText(_path));
        //Debug.Log(_jsonObj[0][0][0]);
        material.color = new Color32(_jsonObj[0][1][1], _jsonObj[0][1][2], _jsonObj[0][1][3], 255);
        // TODO: change the list of colors in JSON to a list of materials from assets
        //Coloring();
    }

    //    private void ReadJSON()
    //    {
    //        string jsonString = File.ReadAllText(fileName);
    //        fish = CreateFromJSON(jsonString);
    //    }

    //    public static Fish CreateFromJSON(string jsonString)
    //    {
    //        return JsonUtility.FromJson<Fish>(jsonString);
    //    }

    private void Coloring()
    {
        //switch (_jsonObj.AsArray)
        //{
        //    case "test1":
        //        break;
        //}
    }
}
