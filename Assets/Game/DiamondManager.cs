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
            Debug.LogError("a story executer already exists!");
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion
    
    private Diamond[] _diamonds;
    private int _collectedDiamondsCount = 0;
    private EditTmproText _uiText;
    
    private void Start()
    {
        _uiText = FindFirstObjectByType<EditTmproText>();
        _uiText.UpdateText("0");
        
        DetectAllDiamonds();
    }

    private void DetectAllDiamonds()
    {
        _diamonds = FindObjectsByType<Diamond>(FindObjectsSortMode.None);

        _collectedDiamondsCount = 0;
    }

    public void CollectDiamond()
    {
        _collectedDiamondsCount++;
        
        _uiText.UpdateText(_collectedDiamondsCount.ToString());
    }
}
