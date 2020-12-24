using GooglePlayGames;
using UnityEngine;

public class GoogleLoginManager : MonoBehaviour
{
    private static GoogleLoginManager instance = null;

    public static GoogleLoginManager Instance => instance;

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
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();

        Login();
    }

    public void Login()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success) MainMenuManager.Instance.LoginSuccess();
            else MainMenuManager.Instance.LoginFail();
        });
    }
}
