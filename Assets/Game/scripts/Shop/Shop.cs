using UnityEngine;

public class Shop : MonoBehaviour
{
    private StoryExecuter _storyExecuter;
    private PickaxeEquipper _pickaxeEquipper;
    [SerializeField] private GameObject[] hideOnShopMenuObjects;
    [SerializeField] private ShopUI shopUI;

    public bool IsShopUIOpen;
    [SerializeField] private ShopItem[] shopItems;
    private int _selectedItem;

    private void Start()
    {
        _selectedItem = 0;
        
        _storyExecuter = StoryExecuter.Instance;
        _pickaxeEquipper = FindFirstObjectByType<Player>().GetCamera()
            .GetComponentInChildren<PickaxeEquipper>();
        
        // disable canvas on load
        shopUI.ChangeItem(shopItems[_selectedItem]);
        shopUI.gameObject.SetActive(false);
    }

    public void PurchaseSelectedItem()
    {
        int diamonds = (int)StatsSingleton.Instance.GetStat(StatType.Diamonds).Value;

        if (shopItems[_selectedItem].Price > diamonds) return;
        
        Debug.Log("Purchasing item " + shopItems[_selectedItem].ItemName);
        PlayerPickaxe.Instance.UnlockItem(shopItems[_selectedItem]);
        shopUI.ChangeItem(shopItems[_selectedItem]); // show unlocked
        
        // like a good merchant, we take the diamonds at the end.
        StatsSingleton.Instance.DecreamentStat(StatType.Diamonds, diamonds);
    }

    public void EquipSelectedItem()
    {
        ShopItem item = shopItems[_selectedItem];
        
        switch (shopItems[_selectedItem].ItemType)
        {
            case ShopItemType.Pickaxe:
                Debug.Log($"Equipped pickaxe {item.ItemName}");
                _pickaxeEquipper.EquipItem(item);
                break;
            case ShopItemType.Strengthner:
                Debug.Log("equipped strengthner");
                break;
            default:
                Debug.LogError("tried to equip Unknown item type");
                break;
        }
        
        shopUI.ChangeItem(item);
    }
    
    
    // select next items, wrap around if went over
    public void SelectNextItem() => ChangeSelectedItem(1);

    public void SelectPreviousItem() => ChangeSelectedItem(-1);
    
    private void ChangeSelectedItem(int direction)
    {
        if (shopItems.Length == 0)
        {
            Debug.LogError("Add items to shop!");
            return;
        }
    
        // wrap around shopItems length
        _selectedItem = (_selectedItem + direction + shopItems.Length) % shopItems.Length;
        shopUI.ChangeItem(shopItems[_selectedItem]);
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
        
        shopUI.gameObject.SetActive(true);
    }

    private void EnableShopMenuHidingObjects()
    {
        foreach (var distraction in hideOnShopMenuObjects)
        {
            distraction.SetActive(true);
        }
        
        shopUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            PlayerPickaxe.CollectDiamond(DiamondType.Blue);
    }
}
