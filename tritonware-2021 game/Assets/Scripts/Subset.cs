using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subset : MonoBehaviour {

    public const int SIZE = 3;

    public SubsetData subsetData {
        get;
        private set;
    }

    public Tile[][] grid
    {
        get;
        private set;
    }

    public Vector2 position {
        get;
        private set;
    }

    public void initSubset(SubsetData _subsetData, Vector2 _position, System.Random random) {

        subsetData = _subsetData;

        position = _position;

        grid = new Tile[SIZE][];

        for (int i = 0; i < SIZE; i++) {
            grid[i] = new Tile[SIZE];
        }

        CreateGrid(random);
    }

    void CreateGrid(System.Random random) {

        List<TileData> spawnLayouts = new List<TileData>();

        List<TileData> emptyLayouts = new List<TileData>();

        List<TileData> obstacleLayouts = new List<TileData>();

        List<TileData> targetLayouts = new List<TileData>();

        foreach (TileData tileData in subsetData.tileLayouts)
        {
            if (tileData.isSpawnLocal) spawnLayouts.Add(tileData);

            switch (tileData.tileType)
            {
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

        for (int widthIndex = 0; widthIndex < SIZE; widthIndex++) {
            for (int rowIndex = 0; rowIndex < SIZE; rowIndex++) {
                Vector2 newPosition = new Vector2(widthIndex, rowIndex);
                TileType[][] tileGrid = processTileGrid(subsetData.tileGrid);
                switch (tileGrid[widthIndex][rowIndex]) {
                    case TileType.Empty:
                        grid[widthIndex][rowIndex] = CreateTile(
                            emptyLayouts[random.Next(0, emptyLayouts.Count)],
                            newPosition);
                        break;
                    case TileType.Obstacle:
                        grid[widthIndex][rowIndex] = CreateTile(
                            obstacleLayouts[random.Next(0, obstacleLayouts.Count)],
                            newPosition);
                        break;
                    case TileType.Target:
                        grid[widthIndex][rowIndex] = CreateTile(
                            targetLayouts[random.Next(0, targetLayouts.Count)],
                            newPosition);
                        break;
                }
            }
        }
    }

    private TileType[][] processTileGrid(string stringTile) {
        string[] lines = stringTile.Split('\n');
        TileType[][] res = new TileType[SIZE][];
        for (int i = 0; i < SIZE; i++) {
            res[i] = new TileType[SIZE];
        }
        for (int colI = 0; colI < SIZE; colI++) {
            for (int rowI = 0; rowI < SIZE; rowI++) {
                switch (lines[rowI][colI]) {
                    case 'E':
                        res[colI][rowI] = TileType.Empty;
                        break;
                    case 'O':
                        res[colI][rowI] = TileType.Obstacle;
                        break;
                    case 'T':
                        res[colI][rowI] = TileType.Target;
                        break;
                }
            }
        }
        return res;
    }

    private Tile CreateTile(TileData tile, Vector2 rPos)
    {
        Vector3 objectPosition = new Vector3(rPos.x, 0, rPos.y);
        GameObject tileObejct = Instantiate(tile.prefab, objectPosition,
            Quaternion.identity);
        tileObejct.transform.SetParent(transform, false);
        tileObejct.AddComponent<Tile>().InitTile(tile, position);
        return tileObejct.GetComponent<Tile>();
    }

}
