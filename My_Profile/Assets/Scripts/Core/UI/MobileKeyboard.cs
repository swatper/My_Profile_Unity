using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileKeyboard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image image;
    [SerializeField] Sprite[] OnOffsprite;
    [SerializeField] bool isPressed;
    public Define.KeyEvent keyType;

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
