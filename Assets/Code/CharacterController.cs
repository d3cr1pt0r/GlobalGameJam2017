using UnityEngine;

public class CharacterController : MonoBehaviour {

    [SerializeField] private float WalkSpeed = 1;
    [SerializeField] private float Friction = 0.96f;
    [SerializeField] private float JumpForce = 1;
    [SerializeField] private float MaxVelocity = 1;
    [SerializeField] private float Gravity = -1;
    [SerializeField] private Transform TopLeft;
    [SerializeField] private Transform BottomRight;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Rigidbody2D Rigidbody2D;
    [SerializeField] private LayerMask GroundLayerMask;
    [SerializeField] private LayerMask RopeLayerMask;
    [SerializeField] private int PlayerNumber;
    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField] private CharacterController OtherCharacter;
    [SerializeField] private AnimationCurve SpringCurve;
    [SerializeField] private float SpringForce = 100;
    [SerializeField] private float MinDis = 2;
    [SerializeField] private float MaxDis = 5;

    private bool IsOnGround;
    private Vector3 Speed;
    private Vector3 Velocity;
    private Vector3 Position;
    private float XAxis;
    private bool Jump;

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
        if (!Mathf.Approximately(XAxis, 0)) {
            SpriteRenderer.flipX = XAxis < 0;
        }

//        MoveUpdate();
//        AttractUpdate();
        RenderRope();
        RopeCollisionUpdate();

    }

    void FixedUpdate() {
        MoveUpdate();
        // Reset if fallen down
        if (transform.position.y < -10) {
            transform.position = Vector3.zero;
            Rigidbody2D.velocity = Vector2.zero;
            Position = Vector3.zero;
            Velocity = Vector3.zero;
        }
    }

    private void MoveUpdate() {

        bool rightRay = Physics2D.Raycast(BottomRight.transform.position, Vector3.down, 0.1f).collider != null;
        bool leftRay = Physics2D.Raycast(TopLeft.transform.position, Vector3.down, 0.1f).collider != null;
//        Debug.DrawRay(BottomRight.transform.position, Vector3.down, Color.red);
//        Debug.DrawRay(TopLeft.transform.position, Vector3.down, Color.red);
//        IsOnGround = Physics2D.OverlapArea(TopLeft.position, BottomRight.position, GroundLayerMask);
        IsOnGround = rightRay || leftRay;
        //        Debug.Log(string.Format("IsOnGround={0}", IsOnGround));

        bool noInput = Mathf.Approximately(XAxis, 0);
        if (!noInput && Mathf.Sign(Velocity.x) != Mathf.Sign(XAxis)) {
            Velocity.x = 0;
        }

        // Moving
        Velocity.x += XAxis * WalkSpeed;
        Velocity.x = Mathf.Clamp(Velocity.x, -MaxVelocity, MaxVelocity);

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

    private void AttractUpdate() {
        Vector3 dif = OtherCharacter.transform.position - transform.position;
        float dis = Vector3.Magnitude(dif);
        float a = Mathf.Clamp01((dis - MinDis) / (MaxDis - MinDis));
        float springForce = SpringCurve.Evaluate(a) * SpringForce;
//        Debug.Log(string.Format("dis={0} a={1}", dis, a));
        Rigidbody2D.AddForce(dif.normalized * springForce);
    }

    private void RenderRope() {
        int ropeIndex = PlayerNumber - 1;
        LineRenderer.SetPosition(ropeIndex, transform.position);
    }

    private void RopeCollisionUpdate() {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, OtherCharacter.transform.position, RopeLayerMask);
        if (hit.collider != null) {
            Debug.Log(string.Format("hit={0}", hit.collider.gameObject));
            if (hit.collider.gameObject.layer == Layers.GROUND) {
                LineRenderer.material.color = Color.red;
            } else {
//                Destroy(hit.collider.gameObject);
                hit.collider.transform.position = Vector3.up * 5 + Vector3.right * Random.Range(-3, 3);
                hit.collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        } else {
            LineRenderer.material.color = Color.white;
        }
    }
}
