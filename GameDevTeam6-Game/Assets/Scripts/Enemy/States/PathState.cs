using Pathfinding;

/*
 * Path state responsible for turning on AIPath class to start searching for paths
 * to the target.
 * From path state, the agent can go into attack state or search state
 * - Clarence 
 */
internal class PathState : IState
{
    private EnemyAI parent;


    public void Enter(EnemyAI parent)
    {
        this.parent = parent;
        parent.aiPath.canMove = true;
        parent.aiPath.canSearch = true;
    }

    public void Exit()
    {
        parent.aiPath.canMove = false;
        parent.aiPath.canSearch = false;
        //throw new System.NotImplementedException();
    }

    public void Update()
    {
        //from path state you can go into attack state, search state
    }
}