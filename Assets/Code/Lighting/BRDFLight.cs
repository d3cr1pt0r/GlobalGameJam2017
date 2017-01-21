using UnityEngine;
using System.Collections;

public class BRDFLight : MonoBehaviour {

    private int LightDirectionID;

    private void Awake() {
        LightDirectionID = Shader.PropertyToID("_LightDirection");
    }

    private void Update() {
        Shader.SetGlobalVector(LightDirectionID, transform.forward);
    }
}
