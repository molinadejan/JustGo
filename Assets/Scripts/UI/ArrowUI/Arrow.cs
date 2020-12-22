using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text countText;

    public void SetSprite(Sprite sprite) => image.sprite = sprite;
    public void SetSprite(string name) => image.sprite = ResourceLoadManager.Instance.GetSprite(name);

    public void SetSprite(Vector3 vector)
    {
        if      (vector == Vector3.up)    image.sprite = ResourceLoadManager.Instance.GetSprite("up"   );
        else if (vector == Vector3.down)  image.sprite = ResourceLoadManager.Instance.GetSprite("down" );
        else if (vector == Vector3.left)  image.sprite = ResourceLoadManager.Instance.GetSprite("left" );
        else if (vector == Vector3.right) image.sprite = ResourceLoadManager.Instance.GetSprite("right");
        else                              image.sprite = null;
    }

    public void SetText(string text) => countText.text = text;
}
