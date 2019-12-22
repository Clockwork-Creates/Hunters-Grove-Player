using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquippedItem : MonoBehaviour, IPointerClickHandler
{
    public GameObject currentItem;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentItem != null && eventData.button == PointerEventData.InputButton.Right)
        {
            var ct = currentItem.GetComponent<ItemDisplay>();

            currentItem.transform.position = Input.mousePosition;
            transform.GetComponent<Image>().color = Color.white;
            ct.selected = true;
            currentItem.SetActive(true);
            Debug.Log("Unequipped: " + ct.item.name + " from slot: " + ct.item.type);
        }
    }
}
