using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public static PlayerController instance;

    [Header("Movement")]
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationSpeedDefault = 60;
    [SerializeField] float rotationSpeedDrift = 120;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float brakeSpeed;
    [SerializeField] MovementState state;
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

    [Header("Keys")]
    [SerializeField] KeyCode driftKey = KeyCode.LeftShift;
    [SerializeField] KeyCode brakeKey = KeyCode.Space;
    [SerializeField] KeyCode forwardKey = KeyCode.W;
    [SerializeField] KeyCode backKey = KeyCode.S;
    [SerializeField] KeyCode leftKey = KeyCode.A;
    [SerializeField] KeyCode rightKey = KeyCode.D;
    [SerializeField] InputAction.CallbackContext action;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb.maxLinearVelocity = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Controls(action);
        Drift();
        StateHandler();
        //Debug.Log(state);
        //Debug.Log(rb.velocity);
    }

    public void Controls(InputAction.CallbackContext context)
    {

        MovingVert(context);
        //Backward();
        //TurnLeft();
        //TurnRight();
        //float translation = Input.GetAxis("Vertical") * -speed * Time.deltaTime; // TODO: rewrite to AddForce
        //float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime; // TODO: rewrite to AddForce or something

        //transform.Translate(0, 0, translation);

        //transform.Rotate(Mathf.Clamp(transform.rotation.x, -10f, 10f), rotation, Mathf.Clamp(transform.rotation.z, -10f, 10f));
    }

    private void MovingVert(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("Moving performed");
        }
        else if (context.started)
        {
            print("Moving started");
        }
        else if (context.canceled)
        {
            print("Moving canceled");
        }
        //if (Input.GetKey(forwardKey))
        //{
        //    //добавить ускорение
        //    //transform.localPosition += Vector3.forward * -speed * Time.deltaTime;
        //    rb.AddForce(Vector3.back, ForceMode.Acceleration);
        //}
    }
    
    private void Backward()
    {
        if (Input.GetKey(backKey))
        {
            //добавить ускорение
            //transform.position += Vector3.forward * speed * Time.deltaTime;
            rb.AddForce(Vector3.forward, ForceMode.Acceleration);
        }
    }
    private void TurnLeft()
    {
        float rotation = -1;
        if (Input.GetKey(leftKey))
        {
            transform.Rotate(Mathf.Clamp(transform.rotation.x, -10f, 10f), rotation * rotationSpeed * Time.deltaTime, Mathf.Clamp(transform.rotation.z, -10f, 10f));
        }
    }
    
    private void TurnRight()
    {
        float rotation = 1;
        if (Input.GetKey(rightKey))
        {
            transform.Rotate(Mathf.Clamp(transform.rotation.x, -10f, 10f), rotation * rotationSpeed * Time.deltaTime, Mathf.Clamp(transform.rotation.z, -10f, 10f));
        }
    }

    private void Drift()
    {
        if (Input.GetKey(driftKey))
        {
            rotationSpeed = rotationSpeedDrift;
        }
        else rotationSpeed = rotationSpeedDefault;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "IslandTrigger")
        {

        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Wall")
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            rb.gameObject.SetActive(true);
        }

        if (collision.transform.tag == "IslandTrigger")
        {

        }
    }

    private void StateHandler()
    {
        // Idle
        if (rb.velocity == Vector3.zero)
        {
            state = MovementState.idle;
        }
        // Sailing
        if (rb.velocity != Vector3.zero)
        {
            state = MovementState.sailing;
        }
        // TurningRight 
        if (rb.angularVelocity != Vector3.zero && (Input.GetKey(KeyCode.D))) // TODO: better key selector
        {
            state = MovementState.turningRight;
        }
        // turningLeft
        if (rb.angularVelocity != Vector3.zero && (Input.GetKey(KeyCode.A)))
        {
            state = MovementState.turningLeft;
        }
        // driftingRight
        if (rb.angularVelocity != Vector3.zero && (Input.GetKey(KeyCode.D)) && Input.GetKey(driftKey))
        {
            state = MovementState.driftingRight;
        }
        // driftingLeft
        if (rb.angularVelocity != Vector3.zero && (Input.GetKey(KeyCode.A)) && Input.GetKey(driftKey))
        {
            state = MovementState.driftingLeft;
        }
        // Air mode??
        //else
        //{
        //    state = MovementState.air;
        //}
    }
}
