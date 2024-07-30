public class StateMachine<T> where T : class
{
    public T entity;
    public State<T> currentState;

    private bool isInit = false;
    public bool isChange = false;

    public StateMachine(T _entity, State<T> _firstState)
    {
        entity = _entity;
        ChangeState(_firstState);
        isInit = true;
    }

    public void ChangeState(State<T> _changeState)
    {
        isChange = true;
        if (currentState != null)
            currentState.Exit(entity);
        currentState = _changeState;
        currentState.Enter(entity);
        isChange = false;
    }

    public void Update()
    {
        if (!isInit) return;
        if (isChange) return;
            currentState.Update(entity);
    }

    public void FixedUpdate()
    {
        if (!isInit) return;
        if (isChange) return;
        currentState.FixedUpdate(entity);
    }
}
