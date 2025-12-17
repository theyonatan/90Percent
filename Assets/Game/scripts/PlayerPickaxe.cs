using System.Collections.Generic;
using UnityEngine;

public class PlayerPickaxe : MonoBehaviour
{
    #region Singleton

    private static PlayerPickaxe _instance;

    public static PlayerPickaxe Instance
    {
        get
        {
            if (!_instance)
            {
                // find an existing one in the scene
                _instance = FindFirstObjectByType<PlayerPickaxe>();

                if (!_instance)
                {
                    // doesn't exist. let's create a new one.
                    GameObject go = new GameObject("PlayerPickaxe");
                    _instance = go.AddComponent<PlayerPickaxe>();
                    DontDestroyOnLoad(go);
                    Debug.Log("New PlayerPickaxe created.");
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError("a PlayerPickaxe already exists!");
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        OnSingletonAwake();
    }

    #endregion

    private Dictionary<string, ShopItem> _unlockedItems = new ();
    
    // setting this to default (regular) at awake().
    private Pickaxe _pickaxe = Pickaxe.Regular;
    private PickaxeDatabase _database;

    private void OnSingletonAwake()
    {
        // load game data
        _pickaxe = Pickaxe.Regular;
        _database = Resources.Load<PickaxeDatabase>("PickaxeDatabase");
        
        if (!_database)
            Debug.LogError("PickaxeDatabase not found in resources!");
        
        // reset data in memory
        StatsSingleton.Instance.ResetStats(new Dictionary<StatType, Stat>()
        {
            {StatType.Diamonds, new Stat(0)}
        });
    }
    
    public int GetMiningStrength(DiamondType diamondType)
    {
        if (!_database.Pickaxes.TryGetValue(_pickaxe, out var data))
            return 0;

        if ((data.MineableDiamonds & diamondType) == 0)
            return 0;
        

        return data.Strength;
    }

    public void EquipPickaxe(Pickaxe newPickaxe)
    {
        _pickaxe = newPickaxe;
    }

    public void UnlockItem(ShopItem item)
    {
        if (!_unlockedItems.TryAdd(item.ItemName, item))
            Debug.LogError("Item " + item.ItemName + " is already unlocked!");
    }
    
    public static void CollectDiamond(DiamondType diamondType)
    {
        Debug.Log($"Collecting Diamond {diamondType}");
        
        int amountOfNewDiamonds = diamondType switch
        {
            DiamondType.Blue => 1,
            DiamondType.Red => Random.Range(2, 6),
            DiamondType.Green => Random.Range(10, 21),
            _ => 0
        };
        
        StatsSingleton.Instance.IncreamentStat(StatType.Diamonds, amountOfNewDiamonds);
    }
}
