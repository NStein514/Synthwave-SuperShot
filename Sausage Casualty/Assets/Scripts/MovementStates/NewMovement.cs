using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementAllOneManager : MonoBehaviour
{
    public float currentMoveSpeed = 3;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed =5;

    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float hzInput, vInput;
    CharacterController controller;

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -0.2f;
    Vector3 velocity;

    NewBase currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator anim;

    //variables for Rowan's movement
    [HideInInspector] public bool isOnWall = false;
    [HideInInspector] public float maxSpeed = 10f;
    [HideInInspector] public float acceleration = 0.2f;
    [HideInInspector] public Vector3 playerVelocity = Vector3.zero;
    [HideInInspector] public Vector3 onWallNormal;
    [HideInInspector] public float jumpPower = 100f;
    [HideInInspector] public Vector2 moveInput = Vector2.zero;
    [HideInInspector] float friction_multiplier = 0.9f;
    private bool isRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        //GetDirectionAndMove();
        Move();
        //Gravity();

        //anim.SetFloat("hzInput", Input.GetAxis("Horizontal"));
        //anim.SetFloat("vInput", Input.GetAxis("Vertical"));

        currentState.UpdateState(this);
    }

    public void SwitchState(NewBase state)
    {
        currentState = state;
        currentState.EnterState(this);
    }


    //Rowan adding things, lets hope nothing breaks hahaah...
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("not on wall ");
        if (hit.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("Wall");
            if (!controller.isGrounded)
            {
                isOnWall = true;
                onWallNormal = hit.normal;
            }
        }
    }

    public void GetJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) {
            anim.SetTrigger("Jumping");
            Jump();
        }
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        anim.SetFloat("hzInput", moveInput.x);
        anim.SetFloat("vInput", moveInput.y);

        if (moveInput.magnitude > 0)
        {
            anim.SetBool("Walking", true);
        }
        else anim.SetBool("Walking", false);

        AnimateRun(context.ReadValue<Vector2>());
    }

    private void Jump() {
        if (controller.isGrounded) {Debug.Log("Is Grounded"); playerVelocity.y = jumpPower;}
        if (isOnWall)
        {
            Debug.Log("Wall");
            isOnWall = false;
            //playerVelocity = Vector3.up * 100f;
            playerVelocity = onWallNormal * 50.0f + Vector3.up * jumpPower;
        }
        anim.SetBool("Falling", true);
    }

    void AnimateRun(Vector3 direction){
        isRunning = (direction.x > 0.1f || direction.x < -0.1f) || (direction.z > 0.1f || direction.z < -0.1f) ? true : false;
        anim.SetBool("Running", isRunning);
    }

    private void Move()
    {
        playerVelocity += (transform.right * moveInput.x + transform.forward * moveInput.y) * acceleration;
        Vector2 xz_velocity = new Vector2(playerVelocity.x, playerVelocity.z);

        if (controller.isGrounded)
        {
            //Friction
            if (moveInput == Vector2.zero)
            {
                //Will use air resistance for in_air state.
                xz_velocity *= friction_multiplier;
            }
        }
        //Reconstruct Player Velocity
        playerVelocity = new Vector3(xz_velocity.x, playerVelocity.y, xz_velocity.y);

        //Gravity
        playerVelocity.y += gravity;
        if (controller.isGrounded && playerVelocity.y < -2f)
        {
            playerVelocity.y = -2f;
        }

        //Move player
        if (isOnWall)
        {
            playerVelocity.x = 0;
            playerVelocity.z = 0;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }
}


// add something for turn in the direction of movement, fix animations back to character

public class WalkState : NewBase
{
    public override void EnterState(MovementAllOneManager movement)
    {
        movement.anim.SetBool("Walking", true);
    }

    public override void UpdateState(MovementAllOneManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.Run);
        else if (movement.dir.magnitude < 0.1f) ExitState(movement, movement.Idle);

        if (movement.vInput < 0) movement.currentMoveSpeed = movement.walkBackSpeed;
        else movement.currentMoveSpeed = movement.walkSpeed;
    }

    void ExitState(MovementAllOneManager movement, NewBase state)
    {
        movement.anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}


public class IdleState : NewBase
{
    public override void EnterState(MovementAllOneManager movement)
    {

    }

    public override void UpdateState(MovementAllOneManager movement)
    {
        if (movement.dir.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) movement.SwitchState(movement.Run);
            else movement.SwitchState(movement.Walk);
        }
    }
}

public class RunState : NewBase
{
    public override void EnterState(MovementAllOneManager movement)
    {
        movement.anim.SetBool("Running", true);
    }

    public override void UpdateState(MovementAllOneManager movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.Walk);
        else if (movement.dir.magnitude < 0.1f) ExitState(movement, movement.Idle);

        if (movement.vInput < 0) movement.currentMoveSpeed = movement.runBackSpeed;
        else movement.currentMoveSpeed = movement.runSpeed;
    }

    void ExitState(MovementAllOneManager movement, NewBase state)
    {
        movement.anim.SetBool("Running", false);
        movement.SwitchState(state);
    }
}