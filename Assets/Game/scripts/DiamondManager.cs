using System.Collections.Generic;
using UnityEngine;

public class DiamondManager : MonoBehaviour
{
    #region Singleton

    public static DiamondManager _instance;
    
    public static DiamondManager Instance
    {
        get
        {
            if (!_instance)
            {
                // find an existing one in the scene
                _instance = FindFirstObjectByType<DiamondManager>();

                if (!_instance)
                {
                    // doesn't exist. let's create a new one.
                    GameObject go = new GameObject("DiamondManager");
                    _instance = go.AddComponent<DiamondManager>();
                    DontDestroyOnLoad(go);
                    Debug.Log("New DiamondManager created.");
                }
            }
            
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError("a DiamondManager already exists!");
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion
    
    private EditTmproText _uiText;
    
    private void Start()
    {
        _uiText = FindFirstObjectByType<EditTmproText>();
        
        StatsSingleton.Instance.ResetStats(new Dictionary<StatType, Stat>()
        {
            {StatType.Diamonds, new Stat(0)}
        });
    }

    public void CollectDiamond(DiamondType diamondType)
    {
        StatsSingleton.Instance.IncreamentStat(StatType.Diamonds, 1f);
    }
}
