using UnityEngine;
using UnityEngine.UI;

public class Equip : MonoBehaviour
{
    public void EquipItem(GameObject item)
    {
        var newItem = item.GetComponent<ItemDisplay>();
        var equippingTo = transform.Find(newItem.item.type);
        var child = equippingTo.GetComponent<EquippedItem>();
        
        if (child.currentItem == null)
        {
            item.SetActive(false);
            child.currentItem = item;
            child.GetComponent<Image>().color = newItem.item.color;
            Debug.Log("Equipped: " + newItem.item.name + " to slot: " + newItem.item.type);
        }
        else if (child.currentItem != null)
        {
            item.SetActive(false);
            child.GetComponent<Image>().color = newItem.item.color;
            child.currentItem.transform.position = Input.mousePosition;
            child.currentItem.SetActive(true);
            child.currentItem.GetComponent<ItemDisplay>().selected = true;
            Debug.Log("Unequipped: " + child.currentItem.GetComponent<ItemDisplay>().item.name + " from slot: " + child.currentItem.GetComponent<ItemDisplay>().item.type);
            child.currentItem = item;
        }
    }
}
