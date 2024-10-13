using System.IO;
using UnityEngine;

public class FishParser : MonoBehaviour
{
    public Fish fish;
    readonly string fileName = "Assets/Scripts/Fish/Fish.json";

    void Start()
    {
        ReadJSON();
        Randomizer();
    }

    private void ReadJSON()
    {
        string jsonString = File.ReadAllText(fileName);
        fish = CreateFromJSON(jsonString);
    }

    public static Fish CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Fish>(jsonString);
    }

    private float[] Randomizer()
    {
        float[] result = new float[4];
        return result;
    }
}
