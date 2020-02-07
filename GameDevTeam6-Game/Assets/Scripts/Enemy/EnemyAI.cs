using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/*
 * This class handles the logical layer of an enemy in order to separate
 * the graphical layer from the logical layer.
 * An enemy can have different behaviours - each behaviour is defined as a state.
 * Each state is responsible for changing to another state
 * 
 * - Clarence
 */

public class EnemyAI: MonoBehaviour
{
    private IState currentState;

    private GameObject target;
    public GameObject Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
            GetComponent<AIDestinationSetter>().target = this.target.transform;
        }
    }

    public AIPath aiPath;

    private void Awake()
    {
        ChangeState(new SearchState());
    }

    private void Start()
    {
    }

    private void Update()
    {
        currentState.Update();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void SetDirection(Vector2 direction)
    {
        GetComponentInChildren<EnemyGFX>().Direction = direction;
    }
}