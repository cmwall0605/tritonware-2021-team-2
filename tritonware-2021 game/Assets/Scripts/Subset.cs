using System;
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

    public int rotation
    {
        get;
        private set;
    }

    public void initSubset(SubsetData _subsetData, Vector2 _position, System.Random random, int _rotation) {

        subsetData = _subsetData;

        position = _position;

        rotation = _rotation;

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

    private TileType[][] rotateGridOnce(TileType[][] tileGrid) {

        TileType[][] rotateGrid = new TileType[SIZE][];

        for (int i = 0; i < SIZE; i++) {
            rotateGrid[i] = new TileType[SIZE];
        }
        rotateGrid[0][0] = tileGrid[0][2];
        rotateGrid[0][1] = tileGrid[1][2];
        rotateGrid[0][2] = tileGrid[2][2];
        rotateGrid[1][0] = tileGrid[0][1];
        rotateGrid[1][1] = tileGrid[1][1];
        rotateGrid[1][2] = tileGrid[2][1];
        rotateGrid[2][0] = tileGrid[0][0];
        rotateGrid[2][1] = tileGrid[1][0];
        rotateGrid[2][2] = tileGrid[2][0];

        return rotateGrid;
    }

    private TileType[][] processTileGrid(string stringTile) {
        string[] lines = stringTile.Split('\n');
        Array.Reverse(lines);
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
        for (int i = 0; i < (rotation / 90) % 4; i++) {
            res = rotateGridOnce(res);
        }

        return res;
    }

    private Tile CreateTile(TileData tile, Vector2 rPos)
    {
        Vector3 objectPosition = new Vector3(rPos.x, 0, rPos.y);
        GameObject tileObejct = Instantiate(tile.prefab, objectPosition,
            Quaternion.Euler(0, rotation, 0));
        tileObejct.transform.SetParent(transform, false);
        tileObejct.AddComponent<Tile>().InitTile(tile, position);
        return tileObejct.GetComponent<Tile>();
    }

}
