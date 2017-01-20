using UnityEngine;

public class CharacterController : MonoBehaviour {

    [SerializeField] private float WalkSpeed = 1;
    [SerializeField] private Rigidbody2D Rigidbody2D;

    void Update() {

        // Get input
        float xAxis = Input.GetAxis("Horizontal");

        if (Mathf.Abs(xAxis) > 0) {
            Debug.Log(string.Format("xAxis={0}", xAxis));
        }

        float xStep = xAxis * WalkSpeed;
        float yStep = 0;

        // Update position
        transform.position += new Vector3(xStep, yStep);

        // Reset if fallen down
        if (transform.position.y < -3) {
            transform.position = Vector3.zero;
            Rigidbody2D.velocity = Vector2.zero;
        }
    }
}
