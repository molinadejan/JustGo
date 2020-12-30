using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;

public partial class GoogleManager
{
    private const string SAVED_DATA = "saveddata";

    private void GoogleLoadSaveInit()
    {
        var config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
    }

    private ISavedGameClient SavedGame()
    {
        return PlayGamesPlatform.Instance.SavedGame;
    }

    private bool isSaveProcess;
    public bool IsSaveProcess => isSaveProcess;

    private bool isSaveSuccess;
    public bool IsSaveSuccess => isSaveSuccess;

    public void SaveCloud()
    {
        isSaveProcess = true;

        SavedGame().OpenWithAutomaticConflictResolution(SAVED_DATA,
            DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, SaveGameToServer);

        Debug.Log("Save Cloud");
    }

    private void SaveGameToServer(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        Debug.Log("Save Game");

        if (status == SavedGameRequestStatus.Success)
        {
            var update = new SavedGameMetadataUpdate.Builder().Build();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(ResourceLoadManager.Instance.JsonString);
            SavedGame().CommitUpdate(game, update, bytes, SaveDataToServer);
        }
        else
        {
            isSaveSuccess = false;
            isSaveProcess = false;
        }
    }

    private void SaveDataToServer(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        isSaveProcess = false;

        Debug.Log("Save Data");

        if (status == SavedGameRequestStatus.Success)
        {
            isSaveSuccess = true;
            Debug.Log("Save Data Success");
        }
        else
        {
            isSaveSuccess = false;
            Debug.Log("Save Data Failed");
        }
    }


    public void LoadCloud()
    {
        SavedGame().OpenWithAutomaticConflictResolution(SAVED_DATA,
            DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, LoadGameFromServer);

        Debug.Log("Load Cloud");
    }

    private void LoadGameFromServer(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
            SavedGame().ReadBinaryData(game, LoadDataFromServer);

        Debug.Log("Load Game");
    }

    private void LoadDataFromServer(SavedGameRequestStatus status, byte[] LoadedData)
    {
        Debug.Log("Load Data");

        if (status == SavedGameRequestStatus.Success)
        {
            ResourceLoadManager.Instance.JsonString = System.Text.Encoding.UTF8.GetString(LoadedData);
        }
        else
        {
            ResourceLoadManager.Instance.JsonString = "LoadError";
        }
    }
}
