using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public TileData tileData {
        get;
        private set;
    }

    public Vector2 position
    {
        get;
        private set;
    }
    public void InitTile(TileData _tileData, Vector2 _position) {

        tileData = _tileData;

        position = _position;
    }
}
