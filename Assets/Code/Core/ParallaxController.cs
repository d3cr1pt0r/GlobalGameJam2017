using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour {

    private Vector3 OriginalPosition;

    // Use this for initialization
    void Start() {
        OriginalPosition = transform.position;
    }
	
    // Update is called once per frame
    void Update() {
        float offset = Game.Instance.CameraHolder.transform.position.x;
        float parallax = Game.Instance.Parallax;
        transform.position = OriginalPosition + Vector3.right * offset * parallax;
    }
}
