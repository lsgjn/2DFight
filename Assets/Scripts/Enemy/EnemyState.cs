public abstract class EnemyState
{
    protected EnemyController controller;

    public EnemyState(EnemyController controller)
    {
        this.controller = controller;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update(); // HandleInput 대신 AI 판단
}
