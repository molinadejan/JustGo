using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;

public class GoogleManager : MonoBehaviour
{
    private static GoogleManager instance = null;

    public static GoogleManager Instance => instance;

    private bool isLogin;
    public bool IsLogin => isLogin;

    private const string SAVED_DATA = "saveddata";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        var config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Login();
    }

    public void Login()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                isLogin = true;
                MainMenuManager.Instance.LoginSuccess();
            }
            else
            {
                isLogin = false;
                MainMenuManager.Instance.LoginFail();
            }
        });
    }

    public bool CheckInternetConnected()
    {
        return !(Application.internetReachability == NetworkReachability.NotReachable);
    }

    #region Save And Load

    private ISavedGameClient SavedGame()
    {
        return PlayGamesPlatform.Instance.SavedGame;
    }

    public void SaveCloud()
    {
        SavedGame().OpenWithAutomaticConflictResolution(SAVED_DATA,
            DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, SaveGame);

        Debug.Log("Save Cloud");
    }

    private void SaveGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        Debug.Log("Save Game");

        if (status == SavedGameRequestStatus.Success)
        {
            var update = new SavedGameMetadataUpdate.Builder().Build();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(ResourceLoadManager.Instance.JsonString);
            SavedGame().CommitUpdate(game, update, bytes, SaveData);
        }
    }

    
    private void SaveData(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        Debug.Log("Save Data");

        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save Data Success");
        }
        else
        {
            Debug.Log("Save Data Failed");
        }
    }
    

    public void LoadCloud()
    {
        SavedGame().OpenWithAutomaticConflictResolution(SAVED_DATA,
            DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, LoadGame);

        Debug.Log("Load Cloud");
    }

    private void LoadGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
            SavedGame().ReadBinaryData(game, LoadData);

        Debug.Log("Load Game");
    }

    private void LoadData(SavedGameRequestStatus status, byte[] LoadedData)
    {
        Debug.Log("Load Data");

        if (status == SavedGameRequestStatus.Success)
        {
            MainMenuManager.Instance.LoadSuccess();
            ResourceLoadManager.Instance.JsonString = System.Text.Encoding.UTF8.GetString(LoadedData);
        }
        else
        {
            MainMenuManager.Instance.LoadFail();
            ResourceLoadManager.Instance.JsonString = "LoadError";
        }
    }

    #endregion
}
