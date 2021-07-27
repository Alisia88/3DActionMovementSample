using UnityEngine;

public partial class ActionStateMachine : MonoBehaviour
{
    public class MoveState : ActionStateBase
    {
        public override void OnEnter(ActionStateMachine owner)
        {
            owner.PlayAnimation("Sprint");
        }

        public override void OnExit(ActionStateMachine owner)
        {
        }

        public override void OnUpdate(ActionStateMachine owner)
        {
            if (owner.inputAxis.sqrMagnitude > 0.1f)
            {
                var dir = owner.moveForward;
                dir.y = 0f;
                owner.targetRot = Quaternion.LookRotation(dir);
                owner.currentVelocity = new Vector3(owner.selfTrans.forward.x, owner.currentVelocity.y, owner.selfTrans.forward.z);
            }
            else
            {
                owner.ChangeState(owner.idleState);
            }
        }
    }
}
