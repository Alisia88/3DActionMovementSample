using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public partial class ActionStateMachine : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 10f;
    [SerializeField]
    float rotateSpeed = 10f;
    [SerializeField]
    float gravityScale = 1f;

    Transform selfTrans;
    CharacterController characterController;
    Animator animator;

    ActionStateBase currentState;
    IdleState idleState = new IdleState();
    MoveState moveState = new MoveState();

    Vector2 inputAxis = Vector2.zero;
    Vector3 moveForward = Vector3.zero;
    Vector3 currentVelocity = Vector3.zero;
    Quaternion targetRot = Quaternion.identity;

    void Start()
    {
        selfTrans = transform;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        ChangeState(idleState);
    }

    void Update()
    {
        var dir = new Vector3(inputAxis.x, 0f, inputAxis.y);
        moveForward = Camera.main.transform.TransformDirection(dir);

        ApplyInputAxis();
        ApplyGravity();
        ApplyMovement();
        ApplyRotation();

        currentState.OnUpdate(this);
    }

    void ApplyInputAxis()
    {
        var h = Input.GetAxisRaw("MoveHorizontal");
        var v = Input.GetAxisRaw("MoveVertical");
        inputAxis = new Vector2(h, v);
    }

    void ApplyMovement()
    {
        var velocity = Vector3.Scale(currentVelocity, new Vector3(moveSpeed, 1f, moveSpeed));
        characterController.Move(Time.deltaTime * velocity);
    }

    void ApplyRotation()
    {
        var rot = selfTrans.rotation;
        rot = Quaternion.Slerp(rot, targetRot, rotateSpeed * Time.deltaTime);
        selfTrans.rotation = rot;
    }

    void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            currentVelocity.y += gravityScale * Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            currentVelocity.y = 0f;
        }
    }

    void PlayAnimation(string stateName, float transitionDuration = 0.1f)
    {
        animator.CrossFadeInFixedTime(stateName, transitionDuration);
    }

    void ChangeState(ActionStateBase nextState)
    {
        currentState?.OnExit(this);
        nextState.OnEnter(this);
        currentState = nextState;
    }
}
