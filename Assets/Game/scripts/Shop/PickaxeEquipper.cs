using UnityEngine;

public class PickaxeEquipper : MonoBehaviour
{
    public void EquipItem(ShopItem pickaxe)
    {
        // kill all children if any
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        
        // spawn new pickaxe
        Instantiate(pickaxe.ItemPrefab, transform);
        
        // equip it in code
        PlayerPickaxe.Instance.EquipPickaxe(pickaxe.PickaxeValue);
    }
}
