using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private StoryExecuter _storyExecuter;
    [SerializeField] private GameObject[] hideOnShopMenuObjects;
    [SerializeField] private GameObject shopUICanvas;

    private ShopItemDisplay _itemDisplay;
    public bool IsShopUIOpen;
    [SerializeField] private ShopItem[] shopItems;
    private int _selectedItem;

    private void Start()
    {
        _selectedItem = 0;
        _itemDisplay = GetComponentInChildren<ShopItemDisplay>();
        
        // disable canvas on load
        shopUICanvas.SetActive(false);
        
        _storyExecuter = StoryExecuter.Instance;
    }

    public void PurchaseSelectedItem()
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
    public void SelectNextItem()
    {
        if (shopItems.Length == 0)
        {
            Debug.LogError("Add items to shop!");
            return;
        }
        
        // wrap around shopItems length
        _selectedItem = (_selectedItem + 1) % shopItems.Length;
        if (shopItems.Length > 0)
            _itemDisplay.ChangeDisplayedItem(shopItems[_selectedItem].ItemPrefab);
    }

    public void SelectPreviousItem()
    {
        if (shopItems.Length == 0)
        {
            Debug.LogError("Add items to shop!");
            return;
        }
        
        // wrap around shopItems length
        _selectedItem = (_selectedItem - 1) % shopItems.Length;
        Debug.LogError($"selected {_selectedItem}");
        if (shopItems.Length > 0)
            _itemDisplay.ChangeDisplayedItem(shopItems[_selectedItem].ItemPrefab);
    }
    
    public void QuitShopMenu()
    {
        // setup leave shop chapter
        _storyExecuter = StoryExecuter.Instance;
        var system = _storyExecuter.GetSystem();
        
        // chapter
        _storyExecuter.SetChapter("LeaveShop");
        
        // show intro cutscene
        system.EnableInput();
        system.SwapPlayerState<cc_fpState, FP_CameraState>();
        system.HideMovieBars();
        system.HideCursor();
        
        // end
        _storyExecuter.startChapter();
        
        IsShopUIOpen = false;
        EnableShopMenuHidingObjects();
    }

    public void DisableShopMenuHidingObjects()
    {
        foreach (var distraction in hideOnShopMenuObjects)
        {
            distraction.SetActive(false);
        }
        
        shopUICanvas.SetActive(true);
    }

    private void EnableShopMenuHidingObjects()
    {
        foreach (var distraction in hideOnShopMenuObjects)
        {
            distraction.SetActive(true);
        }
        
        shopUICanvas.SetActive(false);
    }
}
