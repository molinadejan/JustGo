using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    #region SingleTon
    private static SpriteManager instance = null;
    public static SpriteManager Instance => instance;
    #endregion

    private Dictionary<string, Sprite> spriteDic;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            spriteDic = new Dictionary<string, Sprite>();

            Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites");

            for (int i = 0; i < sprites.Length; i++)
                spriteDic.Add(sprites[i].name, sprites[i]);
        }
    }

    public Sprite GetSprite(string name)
    {
        if (spriteDic.ContainsKey(name)) return spriteDic[name];
        return null;
    }
}
