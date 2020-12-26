
#if UNITY_ANDROID
using GooglePlayGames;
#endif

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
#if UNITY_EDITOR
        MainMenuManager.Instance.LoginSuccess();
#elif UNITY_ANDROID
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();

        Login();
#endif
    }

    public void Login()
    {
        MainMenuManager.Instance.LoginSuccess();

        Social.localUser.Authenticate((bool success) =>
        {
            if (success) MainMenuManager.Instance.LoginSuccess();
            else MainMenuManager.Instance.LoginFail();
        });
    }
}
