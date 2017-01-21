using UnityEngine;

public class CharacterController : MonoBehaviour {

    [SerializeField] private float WalkSpeed = 1;
    [SerializeField] private float Friction = 0.96f;
    [SerializeField] private float JumpForce = 1;
    [SerializeField] private float MaxVelocity = 1;
    [SerializeField] private float Gravity = -1;
    [SerializeField] private Transform TopLeft;
    [SerializeField] private Transform BottomRight;
    [SerializeField] private GameObject Graphics;
    [SerializeField] private LayerMask GroundLayerMask;
    [SerializeField] private LayerMask RopeLayerMask;
    [SerializeField] private int PlayerNumber;
    [SerializeField] private CharacterController OtherCharacter;

    [Header("Rope")]
    [SerializeField] private Transform RopeSource;
    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField] private float RopeForce = 100;
    [SerializeField] private float RopeLength = 2;

    private bool IsOnGround;
    private Vector3 Speed;
    private Vector3 Velocity;
    private Vector3 Position;
    private float XAxis;
    private bool Jump;
    private RaycastHit2D Hit;

    void Start() {
        Position = transform.position;
    }

    void Update() {

        // Get input
        float xAxis1 = Input.GetAxis("Horizontal1");
        float xAxis2 = Input.GetAxis("Horizontal2");
        XAxis = PlayerNumber == 1 ? xAxis1 : xAxis2;
        bool jump1 = Input.GetButtonDown("JumpP1");
        bool jump2 = Input.GetButtonDown("JumpP2");
        Jump = PlayerNumber == 1 ? jump1 : jump2;

        // Rotate toward walk direction
        Vector3 dif = OtherCharacter.transform.position - transform.position;
        float dir = Mathf.Sign(Vector3.Dot(Vector3.right, dif));
        Graphics.transform.localScale = new Vector3(dir, 1, 1);

        RenderRope();
        RopeCollisionUpdate();
    }

    void FixedUpdate() {
        MoveUpdate();
        // Reset if fallen down
        if (transform.position.y < -10) {
            transform.position = Vector3.zero;
            Position = Vector3.zero;
            Velocity = Vector3.zero;
        }
    }

    private void MoveUpdate() {

        // Vertical collision
        Hit = Physics2D.Raycast(BottomRight.transform.position, Vector3.down, 0.1f, GroundLayerMask);
        if (Hit.collider == null)
            Hit = Physics2D.Raycast(TopLeft.transform.position, Vector3.down, 0.1f, GroundLayerMask);
        
//        Debug.DrawRay(BottomRight.transform.position, Vector3.down, Color.red);
//        Debug.DrawRay(TopLeft.transform.position, Vector3.down, Color.red);
        IsOnGround = Hit.collider != null;
        //        Debug.Log(string.Format("IsOnGround={0}", IsOnGround));

        if (IsOnGround) {
            Vector3 pos = transform.position;
            transform.position = pos;
            Position = transform.position;
        }

        bool noInput = Mathf.Approximately(XAxis, 0);
        if (!noInput && Mathf.Sign(Velocity.x) != Mathf.Sign(XAxis)) {
            Velocity.x = 0;
        }

        // Moving
        Velocity.x += XAxis * WalkSpeed;
        Velocity.x = Mathf.Clamp(Velocity.x, -MaxVelocity, MaxVelocity);

        // Spring
        Vector3 dif = OtherCharacter.transform.position - transform.position;
        float dis = Vector3.Magnitude(dif);
        float x = Mathf.Clamp(dis - RopeLength, 0, Mathf.Infinity);
        float k = RopeForce;
        float ropeForce = x * k;
        Velocity += dif.normalized * ropeForce;

        // Jumping
        if (IsOnGround) {
            Velocity.y = 0;
            if (Jump) {
                Velocity.y += JumpForce;
            }
        }

        // Gravity
        if (!IsOnGround) {
            Velocity.y += Gravity;
        }

        // Friction
        if (noInput) {
            Velocity.x *= Friction;
        }

        // Apply movement
        Position += Velocity;
        transform.position = Position;
    }

    private void RenderRope() {
        int ropeIndex = PlayerNumber - 1;
        LineRenderer.SetPosition(ropeIndex, RopeSource.position);
    }

    private void RopeCollisionUpdate() {
        RaycastHit2D hit = Physics2D.Linecast(RopeSource.position, OtherCharacter.RopeSource.position, RopeLayerMask);
        if (hit.collider != null) {
            Debug.Log(string.Format("hit={0}", hit.collider.gameObject));
            if (hit.collider.gameObject.layer == Layers.GROUND) {
                LineRenderer.material.color = Color.red;
            } else if (hit.collider.gameObject.layer == Layers.OBJECTS) {
//                Destroy(hit.collider.gameObject);
                hit.collider.transform.position = Vector3.up * 5 + Vector3.right * Random.Range(-3, 3);
                hit.collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        } else {
            LineRenderer.material.color = Color.white;
        }
    }
}
