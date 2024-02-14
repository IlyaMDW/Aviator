using System.Threading.Tasks;
using UnityEngine;

public abstract class Storage
{
    //PLAYER DATA
    public abstract void SaveData<T>(T savingData);
    public abstract Task<PlayerData> LoadPlayerData();


    //HELP
    protected string BuildPath(string type)
    {
        return Application.persistentDataPath + $"{type}.json";
    }
}