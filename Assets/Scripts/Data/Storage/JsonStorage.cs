using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class JsonStorage : Storage
{
    //PLAYER DATA
    public override void  SaveData<T>(T savingData)
    {
        string type = typeof(T).Name;
        string path = BuildPath(type);

        string jsonData = JsonUtility.ToJson(savingData);
        File.WriteAllText(path, jsonData);
    }
    public override async Task<PlayerData> LoadPlayerData()
    {
        string type = typeof(PlayerData).Name;

        string filePath = string.Format(BuildPath(type));

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            var loadingData = JsonUtility.FromJson<PlayerData>(jsonData);

            return loadingData;
        }

        return null;
    }
}