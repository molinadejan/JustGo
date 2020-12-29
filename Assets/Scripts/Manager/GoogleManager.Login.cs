using GooglePlayGames;
using UnityEngine;

public partial class GoogleManager
{
    private bool isLogin;
    public bool IsLogin => isLogin;

    private void GoogleLoginInit()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
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
}
