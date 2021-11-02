using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public const int MAP_WIDTH = 10;
    public const int MAP_HEIGHT = 10;

    public SubsetData[] subsetLayouts;
    public GameObject subsetSetObject;
    public GameObject player;

    public Tile[][] grid {
        get;
        private set;
    }

   /*public GameObject player {
        get;
        private set;
    }
    */
    
    // Start is called before the first frame update
    void Start() {
        grid = new Tile[MAP_WIDTH * Subset.SIZE][];
        for (int i = 0; i < MAP_WIDTH * Subset.SIZE; i++) {
            grid[i] = new Tile[MAP_HEIGHT * Subset.SIZE];
        } 
        CreateSubsetGrid(Random.Range(0, int.MaxValue));
        //Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private System.Random random;

    void CreateSubsetGrid(int givenSeed) {

        random = new System.Random(givenSeed);

        List<SubsetData> spawnLayouts = new List<SubsetData>();

        List<SubsetData> roadLayouts = new List<SubsetData>();

        List<SubsetData> lotLayouts = new List<SubsetData>();

        List<SubsetData> obstacleLayouts = new List<SubsetData>();

        List<SubsetData> targetLayouts = new List<SubsetData>();

        foreach (SubsetData subsetData in subsetLayouts)
        {
            if (subsetData.isSpawnLocal) spawnLayouts.Add(subsetData);

            switch (subsetData.subsetType)
            {
                case SubsetType.Road:
                    roadLayouts.Add(subsetData);
                    break;
                case SubsetType.Lot:
                    lotLayouts.Add(subsetData);
                    break;
                case SubsetType.Obs:
                    obstacleLayouts.Add(subsetData);
                    break;
                case SubsetType.Target:
                    targetLayouts.Add(subsetData);
                    break;
            }
        }

        for (int colI = 0; colI < MAP_WIDTH; colI++) {
            for (int rowI = 0; rowI < MAP_HEIGHT; rowI++) {
                Vector2 newPosition = new Vector2(colI * Subset.SIZE, rowI * Subset.SIZE);
                Subset currSubset = null;
                switch (random.Next(0, 4)) {
                    case 0:
                        currSubset = CreateSubset(roadLayouts[random.Next(0, roadLayouts.Count)],
                            newPosition);
                        break;
                    case 1:
                        currSubset = CreateSubset(lotLayouts[random.Next(0, lotLayouts.Count)],
                            newPosition);
                        break;
                    case 2:
                        currSubset = CreateSubset(obstacleLayouts[random.Next(0, obstacleLayouts.Count)],
                            newPosition);
                        break;
                    case 3:
                        currSubset = CreateSubset(targetLayouts[random.Next(0, targetLayouts.Count)],
                            newPosition);
                        break;
                }
                for (int innerColI = 0; innerColI < Subset.SIZE; innerColI++) {
                    for (int innerRowI = 0; innerRowI < Subset.SIZE; innerRowI++) {
                        grid[colI * Subset.SIZE + innerColI][rowI * Subset.SIZE + innerRowI] = currSubset.grid[innerColI][innerRowI];
                    }
                }
            }
        }
    }

    private Subset CreateSubset(SubsetData subset, Vector2 rPos) {
        Vector3 objectPosition = new Vector3(rPos.x, 0, rPos.y);
        GameObject subsetObject = Instantiate(subset.prefab, objectPosition,
            Quaternion.identity);
        subsetObject.transform.SetParent(subsetSetObject.transform, false);
        subsetObject.AddComponent<Subset>().initSubset(subset, rPos, random);
        return subsetObject.GetComponent<Subset>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Run on end on coroutine
    void MovePlayerToStart(GameObject player) {
    }
}
