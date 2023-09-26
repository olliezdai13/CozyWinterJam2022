public class PlayerStateFactory
{
    PlayerStateMachine _context;
    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Grounded()
    {
        return new PlayerStateGrounded(_context, this);
    }
    public PlayerBaseState Idle()
    {
        return new PlayerStateIdle(_context, this);
    }
    public PlayerBaseState Move()
    {
        return new PlayerStateMove(_context, this);
    }
    public PlayerBaseState Attack()
    {
        return new PlayerStateAttack(_context, this);
    }
    public PlayerBaseState Dead()
    {
        return new PlayerStateDead(_context, this);
    }
}
