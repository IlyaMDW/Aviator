using System.Threading.Tasks;
using UnityEngine;

public class DataProvider
{
    private PlayerData _playerData;
    private Storage storageService;

    public PlayerData PlayerData => _playerData;

    public DataProvider()
    {
        storageService = new JsonStorage();
    }
    //PLAYER DATA
    public void SaveData<T>(T savingData)
    {
        storageService.SaveData<T>(savingData);
    }
    public async Task LoadDataAsync()
    {
        await LoadDataPlayerFrofileAsync();
    }

    private async Task LoadDataPlayerFrofileAsync()
    {
        Task<PlayerData> task = storageService.LoadPlayerData();
        _playerData = await task;
        if (_playerData == null)
            _playerData = new PlayerData();
    }
}
