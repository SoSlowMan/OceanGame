using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Movement")]
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationSpeedDefault = 60;
    [SerializeField] float rotationSpeedDrift = 120;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float brakeSpeed;
    [SerializeField] MovementState state;
    [SerializeField] Vector2 move;
    [SerializeField] enum MovementState
    {
        idle,
        sailing,
        turningRight,
        turningLeft,
        driftingRight,
        driftingLeft,
        air //?
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    public void Movement(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.05f);
        }
        float aceleration = Mathf.Lerp(0, speed, 0.3f); // TODO: fix
        maxSpeed = aceleration;
        transform.Translate(-movement * aceleration * Time.deltaTime, Space.World);
    }

    //private void Drift()
    //{
    //    if (Input.GetKey(driftKey))
    //    {
    //        rotationSpeed = rotationSpeedDrift;
    //    }
    //    else rotationSpeed = rotationSpeedDefault;
    //}

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag == "IslandTrigger")
    //    {

    //    }
    //}

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.transform.tag == "Wall")
    //    {
            
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.transform.tag == "Wall")
    //    {
            
    //    }

    //    if (collision.transform.tag == "IslandTrigger")
    //    {

    //    }
    //}

    //private void StateHandler()
    //{
    //    // Idle
    //    if (rb.velocity == Vector3.zero)
    //    {
    //        state = MovementState.idle;
    //    }
    //    // Sailing
    //    if (rb.velocity != Vector3.zero)
    //    {
    //        state = MovementState.sailing;
    //    }
    //    // TurningRight 
    //    if (rb.angularVelocity != Vector3.zero && (Input.GetKey(KeyCode.D))) // TODO: better key selector
    //    {
    //        state = MovementState.turningRight;
    //    }
    //    // turningLeft
    //    if (rb.angularVelocity != Vector3.zero && (Input.GetKey(KeyCode.A)))
    //    {
    //        state = MovementState.turningLeft;
    //    }
    //    // driftingRight
    //    if (rb.angularVelocity != Vector3.zero && (Input.GetKey(KeyCode.D)) && Input.GetKey(driftKey))
    //    {
    //        state = MovementState.driftingRight;
    //    }
    //    // driftingLeft
    //    if (rb.angularVelocity != Vector3.zero && (Input.GetKey(KeyCode.A)) && Input.GetKey(driftKey))
    //    {
    //        state = MovementState.driftingLeft;
    //    }
    //}
}
