using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;

    public new string name;
    public TextMeshProUGUI[] infoText;
    public GameObject infoTextHolder;
    public Equip equip;

    [HideInInspector]
    private bool canPlace = true;
    private bool canDrop;
    private GameObject player;
    public bool selected;

    private void OnValidate()
    {
        name = item.name; //Sets the name of the item in the editor without running the game
    }

    private void Start()
    {
        infoTextHolder.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Image>().color = item.color; //Sets the color of the item to the scriptable objects color (you can replace this with whatever variables you want color is just a placeholder)
    }

    private void Update()
    {
        if (selected == true)
        {
            MoveWithMouse(); //Runs the function if selected

            if (Input.GetKeyDown(KeyCode.E)) //Runs Equip script Equip item function changing the color to the coresponding part
            {
                equip.EquipItem(gameObject);
                infoTextHolder.SetActive(false);
                selected = false;
                return;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) //Selects this item when clicked on and deselects it once clicked another time
    {
        if (selected == true && canDrop == true)
        {
            Drop();
            return;
        }

        if (selected == true && canPlace == true)
        {
            selected = false;
            Debug.Log("Item: " + name + " deselected");
            return;
        }
        else if (selected == false && canPlace == true)
        {
            selected = true;
            Debug.Log("Item: " + name + " selected");
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        infoText[0].text = "Name: " + item.name;
        infoText[1].text = "Type: " + item.type;
        infoTextHolder.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoTextHolder.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("You collided with: " + collision.name);

        if (collision.gameObject == transform.parent.gameObject) //Check if the collision is with parent borders
        {
            canPlace = false;
            canDrop = true;
            Debug.Log("You can drop this item");
        }
        
        if (collision.tag == "Item") //Check if the collision is with another inventroy item
        {
            canPlace = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("You stopped colliding with: " + collision.name);

        if (collision.gameObject == transform.parent.gameObject)
        {
            canDrop = false;
            canPlace = true;
            Debug.Log("You can place this item");
        }

        if (collision.tag == "Item")
        {
            canPlace = true;
        }
    }

    private void Drop()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 playerDir = player.transform.forward;
        Vector3 spawnPos = playerPos + playerDir * 2.0f;

        var itemClone = Instantiate(item.gameItem,spawnPos, Quaternion.identity);
        itemClone.GetComponent<Renderer>().material.color = item.color;
        infoTextHolder.SetActive(false);
        Debug.Log("You dropped: " + name);
        Destroy(gameObject);       
        return;
    }

    private void MoveWithMouse()
    {
        transform.position = Input.mousePosition; //Moves UI image according to the mouse position
        transform.SetAsLastSibling(); //Sets it to the top most layer to stack on all other ui elements
    }


}
