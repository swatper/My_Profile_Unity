using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Core.Define;

public class MobileKeyboard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image image;
    [SerializeField] Sprite[] OnOffsprite;
    [SerializeField] bool isPressed;
    public KeyEvent keyType;

    public void OnPointerUp(PointerEventData eventData){
        isPressed = false;
        image.sprite = OnOffsprite[0];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        image.sprite = OnOffsprite[1];
    }

    void Update()
    {
        if (isPressed){
            GameManager.Input.RequestKeyEvent(keyType);
        }
    }
}
