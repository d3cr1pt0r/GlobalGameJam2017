using UnityEngine;

public class CharacterController : MonoBehaviour {

    [SerializeField] private float WalkSpeed = 1;
    [SerializeField] private float JumpForce = 100;
    [SerializeField] private Transform TopLeft;
    [SerializeField] private Transform BottomRight;
    [SerializeField] private Rigidbody2D Rigidbody2D;
    [SerializeField] private LayerMask GroundLayerMask;
    [SerializeField] private int PlayerNumber;

    private bool IsOnGround;
    private Vector3 Speed;
    private Vector3 Acceleration;

    void Update() {

        // Get input
        float xAxis1 = Input.GetAxis("Horizontal1");
        float xAxis2 = Input.GetAxis("Horizontal2");
        bool jump1 = Input.GetButtonDown("JumpP1");
        bool jump2 = Input.GetButtonDown("JumpP2");

        MoveUpdate(PlayerNumber == 1 ? xAxis1 : xAxis2, PlayerNumber == 1 ? jump1 : jump2);

        // Reset if fallen down
        if (transform.position.y < -3) {
            transform.position = Vector3.zero;
            Rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void MoveUpdate(float xAxis, bool jump) {

        IsOnGround = Physics2D.OverlapArea(TopLeft.position, BottomRight.position, GroundLayerMask);
        //        Debug.Log(string.Format("IsOnGround={0}", IsOnGround));

        if (IsOnGround && jump) {
            Rigidbody2D.AddForce(Vector3.up * JumpForce);
        }

        float xStep = xAxis * WalkSpeed;
        float yStep = 0;

        // Update position
        transform.position += new Vector3(xStep, yStep);

    }
}
