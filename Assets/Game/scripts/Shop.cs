using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopItem[] shopItems;
    private int _selectedItem;

    private void Start()
    {
        _selectedItem = 0;
    }

    public void PurchaseItem()
    {
        int diamonds = (int)StatsSingleton.Instance.GetStat(StatType.Diamonds).Value;

        if (shopItems[_selectedItem].Price >= diamonds) return;
        
        Debug.Log("Purchasing item " + shopItems[_selectedItem].ItemName);
        PlayerPickaxe.Instance.UnlockItem(shopItems[_selectedItem]);
        EquipSelectedItem();
    }

    public void EquipSelectedItem()
    {
        PlayerPickaxe player = PlayerPickaxe.Instance;
        ShopItem item = shopItems[_selectedItem];
        
        switch (shopItems[_selectedItem].ItemType)
        {
            case ShopItemType.Pickaxe:
                Debug.Log($"Equipped pickaxe {item.ItemName}");
                player.EquipPickaxe(item.PickaxeValue);
                break;
            case ShopItemType.Strengthner:
                Debug.Log("equipped strengthner");
                break;
            default:
                Debug.LogError("tried to equip Unknown item type");
                break;
        }
    }
    
    // select next items, wrap around if went over
    public void SelectNextItem() => _selectedItem = (_selectedItem + 1) % shopItems.Length;
    public void SelectPreviousItem() => _selectedItem = (_selectedItem - 1) % shopItems.Length;
}
