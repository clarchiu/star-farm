using Pathfinding;

/*
 * Path state responsible for turning on AIPath class to start searching for paths
 * to the target.
 * From path state, the agent can go into search state if the target ever becomes null
 * or the agent can go into follow state once it is close enough
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
        if (parent.Target == null)
        {
            parent.ChangeState(new SearchState());
        } else if ( parent.aiPath.reachedEndOfPath == true )
        {
            parent.ChangeState(new FollowState());
        }
    }
}