using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public TileData tileData {
        get;
        private set;
    }

    public void InitTile(TileData _tileData) {

        tileData = _tileData;
    }
}
