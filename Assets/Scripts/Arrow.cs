using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Arrow : MonoBehaviour, IPointerUpHandler
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UIManager.Instance.MinusArrow(image);
            gameObject.SetActive(false);
        }
    }
}
