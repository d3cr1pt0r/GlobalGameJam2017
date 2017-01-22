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
    [SerializeField] private Rigidbody2D Rigidbody2D;

    [Header("Graphics")]
    [SerializeField] private Animator Animator;
    [SerializeField] private Transform BackWheel;
    [SerializeField] private Transform FrontWheel;
    [SerializeField] private float BackWheelRotation = 200;
    [SerializeField] private float ForwardWheelRotation = 400;

    [Header("Rope")]
    [SerializeField] private Transform RopeSource;
    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField] private float RopeForce = 100;
    [SerializeField] private float RopeLength = 2;
    [SerializeField] private Texture2D ValidTexture;
    [SerializeField] private Texture2D InvalidTexture;

    private bool IsOnGround;
    private Vector3 Speed;
    private Vector3 Velocity;
    private Vector3 Position;
    private float XAxis;
    private bool Jump;
    private RaycastHit2D Hit;
    private float TurnHeadDelay;
    private Vector3 PrevPos;
    private bool IsControlsActive;

    public void Reset(){
        Position = transform.position;
        PrevPos = transform.position;
        Rigidbody2D.velocity = Vector3.zero;
    }

    void Start() {
        Reset();
    }

    void Update() {

        // Get input
        if (IsControlsActive) {
            float xAxis1 = Input.GetAxis("Horizontal_p1");
            float xAxis2 = Input.GetAxis("Horizontal_p2");
            XAxis = PlayerNumber == 1 ? xAxis1 : xAxis2;
            bool jump1 = Input.GetButtonDown("Jump_p1");
            bool jump2 = Input.GetButtonDown("Jump_p2");
            Jump = PlayerNumber == 1 ? jump1 : jump2;
        } else {
            XAxis = 0;
            Jump = false;
        }

        // Rotate toward walk direction
        Vector3 dif = OtherCharacter.transform.position - transform.position;
        float dir = Mathf.Sign(Vector3.Dot(Vector3.right, dif));
        Graphics.transform.localScale = new Vector3(dir, 1, 1);

        RenderRope();
        RopeCollisionUpdate();

        // Trigger head animation
        TurnHeadDelay -= Time.deltaTime;
        if (TurnHeadDelay <= 0) {
            Animator.SetTrigger("DoTurnHead");
            TurnHeadDelay = Random.Range(5, 10);
        }
    }

    void FixedUpdate() {
        MoveUpdate();
        // Reset if fallen down
        if (transform.position.y < -10) {
            transform.position = Vector3.zero;
            Position = Vector3.zero;
            Velocity = Vector3.zero;
        }

        // Turn wheels
        if (IsOnGround) {
            float xStep = transform.position.x - PrevPos.x;
            BackWheel.Rotate(Vector3.forward, -xStep * BackWheelRotation);
            FrontWheel.Rotate(Vector3.forward, -xStep * ForwardWheelRotation);
            //            Debug.Log(string.Format("Velocity.x={0}", Velocity.x));
        }
        PrevPos = transform.position;
    }

    public void SetControlsActive(bool active) {
        IsControlsActive = active;
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
//            Debug.Log(string.Format("hit={0}", hit.collider.gameObject));
            if (hit.collider.gameObject.layer == Layers.GROUND) {
                LineRenderer.material.SetTexture("_MainTexture", InvalidTexture);
            } else if (hit.collider.gameObject.layer == Layers.ITEM) {
//              Destroy(hit.collider.gameObject);
//				hit.collider.transform.position = Vector3.up * 5 + Vector3.right * Random.Range (-3, 3);
//				hit.collider.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
                hit.collider.GetComponent<ItemController>().OnCollisionEnterRope();
            }
        } else {
            LineRenderer.material.SetTexture("_MainTexture", ValidTexture);
        }
    }
}
