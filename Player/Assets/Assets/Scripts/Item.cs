using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Test")]
public class Item : ScriptableObject
{
    public new string name;
    public string type;
    public Color color;
    public GameObject gameItem;

    //Put the rest of item variables here

    //If you wish to add different types of items that need seperate variables you need to create new scripts for every type of item in the game
    //For example apples = food so make a new script called food fileName = "New Item", menuName = "Items/Food" this can apply to any type of item
}
