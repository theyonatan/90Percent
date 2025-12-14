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

    // setting this to default (regular) at awake().
    private EquippedPickaxe _equippedPickaxe = EquippedPickaxe.Regular;
    private PickaxeDatabase _database;

    private void OnSingletonAwake()
    {
        // load game data
        _equippedPickaxe = EquippedPickaxe.Regular;
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
        if (!_database.Pickaxes.TryGetValue(_equippedPickaxe, out var data))
            return 0;

        if ((data.MineableDiamonds & diamondType) == 0)
            return 0;
        

        return data.Strength;
    }

    public void EquipPickaxe(EquippedPickaxe newPickaxe)
    {
        _equippedPickaxe = newPickaxe;
    }
    
    public static void CollectDiamond(DiamondType diamondType)
    {
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
