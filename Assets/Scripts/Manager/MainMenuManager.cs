using UnityEngine;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager instance = null;

    public static MainMenuManager Instance => instance;

    [SerializeField] private UnityEvent loginSuccess;
    public void LoginSuccess() => loginSuccess?.Invoke();

    [SerializeField] private UnityEvent loginFail;
    public void LoginFail() => loginFail?.Invoke();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
