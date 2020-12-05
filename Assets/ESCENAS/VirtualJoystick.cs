//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;

//public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
//{
//    [SerializeField] RectTransform stick = null;
//    [SerializeField] Image background = null;
//    public string player = "";
//    public float limit = 250;
//    public void OnPointerDown(PointerEventData eventData)
//    {
//        background.color = Color.black;
//        stick.anchoredPosition = ConvertTOLocal(eventData);
//    }
//    public void OnDrag(PointerEventData eventData)
//    {
//        Vector2 pos = ConvertTOLocal(eventData);
//        if (pos.magnitude > limit)
//        {
//            pos = pos.normalized * limit;
//        }
//        stick.anchoredPosition = pos;

//        float x = pos.x / limit;
//        float y = pos.y / limit;

//        InputManager.Instance.SetAxis("Horizontal" + player, x);
//        InputManager.Instance.SetAxis("Vertical" + player, y);
//    }

//    public void OnPointerUp(PointerEventData eventData)
//    {
//        InputManager.Instance.SetAxis("Horizontal" + player, 0);
//        InputManager.Instance.SetAxis("Vertical" + player, 0);
//        background.color = Color.white;
//        stick.anchoredPosition = Vector2.zero;
//    }
//    private void OnDisable()
//    {
//        InputManager.Instance.SetAxis("Horizontal" + player, 0);
//        InputManager.Instance.SetAxis("Vertical" + player, 0);
//    }
//    Vector2 ConvertTOLocal(PointerEventData eventData)
//    {
//        Vector2 newPos;
//        RectTransformUtility.ScreenPointToLocalPointInRectangle(
//            transform as RectTransform,
//                eventData.position,
//                eventData.enterEventCamera,
//                out newPos);
//        return newPos;


//    }

//}
