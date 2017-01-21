using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField] private Rigidbody2D Rigidbody2D;

    void Update() {
        if (transform.position.y < -5) {
            transform.position = Vector3.up;
            Rigidbody2D.velocity = Vector2.zero;
        }
    }
}
