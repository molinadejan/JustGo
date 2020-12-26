using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager instance = null;

    public static MainMenuManager Instance => instance;

    [SerializeField] private UnityEvent loginSuccess;
    public void LoginSuccess() => loginSuccess?.Invoke();

    [SerializeField] private UnityEvent loginFail;
    public void LoginFail() => loginFail?.Invoke();

    [SerializeField] private UnityEvent loadSuccess;
    public void LoadSuccess() => loadSuccess?.Invoke();

    [SerializeField] private UnityEvent loadFail;
    public void LoadFail() => loadFail?.Invoke();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
