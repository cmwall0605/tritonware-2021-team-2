using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 0f;
    Vector3 pos;
    Transform tr;
    [SerializeField] float acceleration = 5f;
    [SerializeField] float deceleration = 5f;
    [SerializeField] float maxSpeed = 5f; 

    void Start() {
        pos = transform.position;
        tr = transform;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.W) && speed < maxSpeed) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            pos += Vector3.right;
            speed = speed + acceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S) && speed > -maxSpeed) {
            pos -= Vector3.left;
            speed = speed - acceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A)) {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.rotation = Quaternion.Euler(0, -270, 0);
        }
        else {
            if (speed > deceleration * Time.deltaTime) {
                speed = speed - deceleration * Time.deltaTime;
            }
            else if (speed < -deceleration * Time.deltaTime) {
                speed = speed + deceleration * Time.deltaTime;
            }
            else {
                speed = 0;
            }
        }

        if (speed == 0) {
            transform.position = new Vector3(Mathf.Round(transform.position.x),
                             0,
                             Mathf.Round(transform.position.z));
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
        }
    }
}
