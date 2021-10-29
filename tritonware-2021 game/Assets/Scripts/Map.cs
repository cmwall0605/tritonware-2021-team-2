using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public const int MAP_WIDTH = 10;
    public const int MAP_HEIGHT = 10;

    public TileData[] tileLayouts;
    public GameObject tileSetObject;

    public List<List<Tile>> grid {
        get;
        private set;
    }

    public GameObject player {
        get;
        private set;
    }
    
    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player");
        grid = new List<List<Tile>>();
        CreateGrid(Random.Range(0, int.MaxValue));
    }

    private System.Random random;

    void CreateGrid(int givenSeed) {

        random = new System.Random(givenSeed);

        List<TileData> spawnLayouts = new List<TileData>();

        List<TileData> emptyLayouts = new List<TileData>();

        List<TileData> obstacleLayouts = new List<TileData>();

        List<TileData> targetLayouts = new List<TileData>();

        foreach (TileData tileData in tileLayouts) {
            if (tileData.isSpawnLocal) spawnLayouts.Add(tileData);

            switch (tileData.tileType) {
                case TileType.Empty:
                    emptyLayouts.Add(tileData);
                    break;
                case TileType.Obstacle:
                    obstacleLayouts.Add(tileData);
                    break;
                case TileType.Target:
                    targetLayouts.Add(tileData);
                    break;
            }
        }

        for (int widthIndex = 0; widthIndex < MAP_HEIGHT; widthIndex++) {
            List<Tile> currentColumnList = new List<Tile>();
            grid.Add(currentColumnList);
            currentColumnList.Add(CreateTile(
                spawnLayouts[random.Next(0, spawnLayouts.Count)],
                new Vector2(0, 0)));
            for (int rowIndex = 0; rowIndex < MAP_HEIGHT; rowIndex++) {
                if (widthIndex + rowIndex == 0) continue;
                Vector2 newPosition = new Vector2(widthIndex, rowIndex);
                currentColumnList.Add(CreateTile(
                    emptyLayouts[random.Next(0, emptyLayouts.Count)], 
                    newPosition));
            }
        }
    }

    public Tile CreateTile(TileData tile, Vector2 position) {
        Vector3 objectPosition = new Vector3(position.x, 0, position.y);
        GameObject tileObejct = Instantiate(tile.prefab, objectPosition, 
            Quaternion.identity, tileSetObject.transform);
        tileObejct.AddComponent<Tile>().InitTile(tile, position);
        return tileObejct.GetComponent<Tile>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Run on end on coroutine
    void MovePlayerToStart(GameObject player) {
    
    }
}
