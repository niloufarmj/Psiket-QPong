using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject selected;
    public void OnPointerEnter(PointerEventData eventData)
    {
        selected.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selected.SetActive(false);
    }
}
    
