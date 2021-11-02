using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TileGenerator/SubsetData")]
public class SubsetData : ScriptableObject {
    [Header("Subset Data")]

    public TileData[] tileLayouts;

    [Multiline]
    public string tileGrid = "EEE\nEEE\nEEE";

    public GameObject prefab;

    public bool isSpawnLocal;

    public SubsetType subsetType;
}

public enum SubsetType {
    Lot,
    Target,
    Obs,
    Road
}

