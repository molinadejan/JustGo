using UnityEngine;

public partial class GoogleManager : MonoBehaviour
{
    private static GoogleManager instance = null;
    public static GoogleManager Instance => instance;

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
        GoogleLoadSaveInit();
        GoogleLoginInit();

        Login();
    }

    public bool CheckInternetConnected()
    {
        return !(Application.internetReachability == NetworkReachability.NotReachable);
    }
}
