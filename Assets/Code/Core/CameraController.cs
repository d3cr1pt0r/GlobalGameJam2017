using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private CharacterController CharacterController1;
    [SerializeField] private CharacterController CharacterController2;
    [SerializeField] private float AttractSpeed = 10;

    void Update() {
        
        Vector3 charactersCenter = (CharacterController1.transform.position + CharacterController2.transform.position) * 0.5f;
        Vector3 dif = charactersCenter - transform.position;
        float dis = Vector3.Magnitude(dif);
        Vector3 moveVector = dif.normalized * dis * AttractSpeed;
        moveVector.y = 0;

        transform.Translate(moveVector);
    }
}
