using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    private List<List<GameObject>> grid;

    public GameObject player {
        get;
        private set;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        grid = new List<List<GameObject>>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Run on end on coroutine
    void movePlayerToStart(GameObject player) {
    
    }
}
