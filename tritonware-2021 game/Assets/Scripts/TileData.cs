using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TileGenerator/TileData")]
public class TileData : ScriptableObject {
    [Header("Tile Data")]

    // Prefab of the tile
    public GameObject prefab;

    // Determines if the room can be spawned into
    public bool isSpawnLocal;

    public TileType tileType;
}

public enum TileType {
    Empty,
    Obstacle,
    Target
}
