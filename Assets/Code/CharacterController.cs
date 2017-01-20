using UnityEngine;

public class CharacterController : MonoBehaviour {

    [SerializeField] private float WalkSpeed = 1;
    [SerializeField] private float JumpForce = 100;
    [SerializeField] private Transform TopLeft;
    [SerializeField] private Transform BottomRight;
    [SerializeField] private Rigidbody2D Rigidbody2D;
    [SerializeField] private LayerMask GroundLayerMask;

    bool IsOnGround;

    void Update() {

        // Get input
        float xAxis = Input.GetAxis("Horizontal");

        if (Mathf.Abs(xAxis) > 0) {
            Debug.Log(string.Format("xAxis={0}", xAxis));
        }

        IsOnGround = Physics2D.OverlapArea(TopLeft.position, BottomRight.position, GroundLayerMask);
        Debug.Log(string.Format("IsOnGround={0}", IsOnGround));

        if (IsOnGround && Input.GetButtonDown("Fire2")) {
            Rigidbody2D.AddForce(Vector3.up * JumpForce);
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
