using UnityEngine;

public partial class ActionStateMachine : MonoBehaviour
{
    public class IdleState : ActionStateBase
    {
        public override void OnEnter(ActionStateMachine owner)
        {
            owner.currentVelocity.x = 0f;
            owner.currentVelocity.z = 0f;
            owner.PlayAnimation("Idle", 0.2f);
        }

        public override void OnExit(ActionStateMachine owner)
        {
        }

        public override void OnUpdate(ActionStateMachine owner)
        {
            if (owner.inputAxis.sqrMagnitude > 0.1f)
            {
                owner.ChangeState(owner.moveState);
            }
        }
    }
}
