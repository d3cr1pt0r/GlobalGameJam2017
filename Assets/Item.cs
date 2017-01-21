using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField] private Rigidbody2D Rigidbody2D;

    void Update() {
        if (transform.position.y < 0.1f) {
            transform.position = Vector3.up * 5 + Vector3.right * Random.Range(-3, 3);
            Rigidbody2D.velocity = Vector2.zero;
        }
    }
}
