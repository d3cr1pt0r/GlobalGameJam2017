using UnityEngine;

public class CharacterController : MonoBehaviour {

    [SerializeField] private float WalkSpeed = 1;
    [SerializeField] private float JumpForce = 100;
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
    private Vector3 Acceleration;

    void Update() {

        // Get input
        float xAxis1 = Input.GetAxis("Horizontal1");
        float xAxis2 = Input.GetAxis("Horizontal2");
        float xAxis = PlayerNumber == 1 ? xAxis1 : xAxis2;
        bool jump1 = Input.GetButtonDown("JumpP1");
        bool jump2 = Input.GetButtonDown("JumpP2");
        bool jump = PlayerNumber == 1 ? jump1 : jump2;

        // Rotate toward walk direction
        if (!Mathf.Approximately(xAxis, 0)) {
            SpriteRenderer.flipX = xAxis < 0;
        }

        MoveUpdate(xAxis, jump);
        AttractUpdate();
        RenderRope();
        RopeCollisionUpdate();

        // Reset if fallen down
        if (transform.position.y < -10) {
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

        // Update position
//        transform.position += Vector3.right * xStep;
        Rigidbody2D.AddForce(Vector3.right * xAxis * 10);
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
            if (hit.collider.gameObject.layer == GroundLayerMask) {
                LineRenderer.material.color = Color.red;
            } else {
                Destroy(hit.collider.gameObject);
            }
        } else {
            LineRenderer.material.color = Color.white;
        }
    }
}
