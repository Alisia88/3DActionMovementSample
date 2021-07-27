using UnityEngine;

public partial class ActionStateMachine : MonoBehaviour
{
    public abstract class ActionStateBase
    {
        public abstract void OnEnter(ActionStateMachine owner);
        public abstract void OnUpdate(ActionStateMachine owner);
        public abstract void OnExit(ActionStateMachine owner);
    }
}
