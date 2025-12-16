using UnityEngine;

public class ShopItemDisplay : MonoBehaviour
{
    public void ChangeDisplayedItem(GameObject item)
    {
        // kill all children if any
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        
        // spawn new item
        Instantiate(item, transform);
    }
}
