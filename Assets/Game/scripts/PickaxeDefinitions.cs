using System;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(
    fileName = "PickaxeDatabase",
    menuName = "Game/Pickaxe Database"
)]
public class PickaxeDatabase : ScriptableObject
{
    public SerializedDictionary<Pickaxe, PickaxeData> Pickaxes;
}

[Flags]
public enum DiamondType
{
    Blue = 1 << 0,
    Red  = 1 << 1,
    Green = 1 << 2,
}

public enum Pickaxe
{
    Regular,
    Advanced,
    Master
}

[Serializable]
public class PickaxeData
{
    public DiamondType MineableDiamonds;
    public int Strength;
}
