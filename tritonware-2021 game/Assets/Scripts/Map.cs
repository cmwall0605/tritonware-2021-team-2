using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public const int MAP_WIDTH = 10;
    public const int MAP_HEIGHT = 10;

    public GameObject tilePrefab;
    public GameObject tileSetObject;

    public List<List<GameObject>> grid {
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
        grid = new List<List<GameObject>>();
        CreateGrid();
    }

    void CreateGrid() {
        for (int widthIndex = 0; widthIndex < MAP_HEIGHT; widthIndex++) {
            List<GameObject> currentColumnList = new List<GameObject>();
            grid.Add(currentColumnList);
            for (int rowIndex = 0; rowIndex < MAP_HEIGHT; rowIndex++) {
                Vector3 spawnPosition = new Vector3(widthIndex, 0, rowIndex);
                GameObject currentTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                currentColumnList.Add(currentTile);
                currentTile.transform.parent = tileSetObject.transform;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Run on end on coroutine
    void MovePlayerToStart(GameObject player) {
    
    }
}
