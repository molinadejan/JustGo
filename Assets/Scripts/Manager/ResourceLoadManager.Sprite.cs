using System.Collections.Generic;
using UnityEngine;

public partial class ResourceLoadManager
{
    private Dictionary<string, Sprite> spriteDic;

    private void LoadSprites()
    {
        spriteDic = new Dictionary<string, Sprite>();

        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites");

        for (int i = 0; i < sprites.Length; i++)
        {
            spriteDic.Add(sprites[i].name, sprites[i]);
        }
    }

    public Sprite GetSprite(string name)
    {
        if (spriteDic.ContainsKey(name))
        {
            return spriteDic[name];
        }

        return null;
    }
}
