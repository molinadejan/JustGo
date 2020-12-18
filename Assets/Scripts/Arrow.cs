using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text countText;

    public void SetSprite(Sprite sprite) => image.sprite = sprite;
    public void SetText(string text) => countText.text = text;

    public void ArrowButton()
    {
        UIManager.Instance.MinusArrow(this);
    }
}
