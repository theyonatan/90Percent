using UnityEngine;

[CreateAssetMenu(
    fileName = "ShopItem",
    menuName = "Game/Shop Item"
)]
public class ShopItem : ScriptableObject
{
    public string ItemName;
    public string Description;
    public ShopItemType ItemType;
    public Pickaxe PickaxeValue;
    public string OtherItemValue;
    public GameObject ItemPrefab;
    public int Price;
}

public enum ShopItemType
{
    Pickaxe,
    Strengthner
}
