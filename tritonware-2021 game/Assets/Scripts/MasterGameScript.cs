using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterGameScript : MonoBehaviour 
{

    //TODO: Add State Machine for Game 

    public static MasterGameScript instance
    {
        private set;
        get;
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
            return;
        }
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        
    }
}
