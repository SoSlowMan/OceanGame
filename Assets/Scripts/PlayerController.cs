using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Movement")]
    //[SerializeField] float rotationSpeed;
    [SerializeField] float rotationSpeedDefault = .15f;
    //[SerializeField] float rotationSpeedDrift = 120;
    [SerializeField] float speed;
    //[SerializeField] float maxSpeed;
    //[SerializeField] float brakeSpeed;
    [SerializeField] MovementState state;

    [Header("Controls")]
    [SerializeField] bool isPC;
    private Vector2 move, mouseLook, gamepadLook;
    private Vector3 rotationTarget;
    
    enum MovementState
    {
        idle,
        sailing,
        turningRight,
        turningLeft,
        driftingRight,
        driftingLeft
    }

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isPC)
        {
            Raycasting();
        }
        else
        {
            if(gamepadLook.x == 0 && gamepadLook.y == 0)
            {
                MovePlayer();
            }
            else
            {
                MovePlayerWithAim();
            }
        }
    }

    public void KeyboardMovement(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void MouseLookMovement(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }
    public void GamepadMovement(InputAction.CallbackContext context)
    {
        gamepadLook = context.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeedDefault);
        }
        transform.Translate(-movement * speed * Time.deltaTime, Space.World);
    }       
    
    private void MovePlayerWithAim()
    {
        MovePlayerChecks();

        Vector3 movement = new Vector3(move.x, 0f, move.y);
        transform.Translate(-movement * speed * Time.deltaTime, Space.Self);
    }
    
    void MovePlayerChecks()
    {
        if (isPC)
        {
            var lookPos = rotationTarget - transform.position;
            lookPos.y = 0f;
            var rotation = Quaternion.LookRotation(-lookPos);

            Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeedDefault);
            }
        }
        else
        {
            Vector3 aimDirection = new Vector3(gamepadLook.x, 0f, gamepadLook.y);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), rotationSpeedDefault);
            }
        }
    }

    void Raycasting()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mouseLook);

        if(Physics.Raycast(ray, out hit))
        {
            rotationTarget = hit.point;
        }

        MovePlayerWithAim();
    }

    //private void Drift()
    //{
    //    if (Input.GetKey(driftKey))
    //    {
    //        rotationSpeed = rotationSpeedDrift;
    //    }
    //    else rotationSpeed = rotationSpeedDefault;
    //}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "IslandTrigger")
        {
            transform.Translate(new Vector3(0f,0f,0f));
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Wall")
        {

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {

        }

        if (collision.transform.tag == "IslandTrigger")
        {

        }
    }

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
