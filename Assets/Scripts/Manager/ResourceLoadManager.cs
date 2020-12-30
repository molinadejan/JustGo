﻿using UnityEngine;

public partial class ResourceLoadManager : MonoBehaviour
{
    #region SingleTon
    private static ResourceLoadManager instance = null;
    public static ResourceLoadManager Instance => instance;
    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

            LoadSprites();
            LoadPrefabs();
            Input.multiTouchEnabled = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CheckDataOnStart();
    }
}
