public abstract class PlayerBaseState
{
    private bool _isRootState = false;
    private PlayerStateMachine _ctx;
    private PlayerStateFactory _factory;
    private PlayerBaseState _currentSubState;
    private PlayerBaseState _currentSuperState;
    
    protected bool IsRootState { get { return _isRootState; } set { _isRootState = value; } }
    protected PlayerStateMachine Ctx { get { return _ctx; } }
    protected PlayerStateFactory Factory { get { return _factory; } }

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();
    public void UpdateStates()
    {
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }
    public void ExitStates()
    {
        ExitState();

        if (_currentSubState != null)
        {
            _currentSubState.ExitStates();
        }
    }
    protected void SwitchState(PlayerBaseState newState) {
        ExitState();

        if (newState.IsRootState)
        {
            _currentSubState = null;
        }
        if (IsRootState)
        {
            _ctx.CurrentState = newState;
            newState.EnterState();
        } else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }
    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(PlayerBaseState newSubState) {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
        newSubState.EnterState();
    }

    public string StateString()
    {
        return PrintHelper();
    }

    private string PrintHelper()
    {
        if (_currentSubState == null)
        {
            return this.ToString();
        } else
        {
            return this.ToString() + "<-" + _currentSubState.PrintHelper();
        }
    }
}
