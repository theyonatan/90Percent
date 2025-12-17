using System;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private ShopItemDisplay itemDisplay;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI price;

    public void ChangeItem(ShopItem item)
    {
        ChangeTitle(item.ItemName);
        ChangeDescription(item.Description);
        ChangePrice(item.Price);
        itemDisplay.ChangeDisplayedItem(item.ItemPrefab);
    }
    
    private void ChangeTitle(string newTitle)
    {
        title.text = newTitle;
    }

    private void ChangeDescription(string newDescription)
    {
        description.text = newDescription;
    }

    private void ChangePrice(int newPrice)
    {
        price.text = newPrice.ToString();
    }
}
