//using System;
//using System.Collections;
//using UnityEngine;

//public class AttackState : IState
//{
//    private Enemy parent;

//    public void Enter(Enemy parent)
//    {
//        this.parent = parent;
//        Debug.Log("enemy in attack state");

//        parent.MyRigidBody.velocity = Vector2.zero;
//        parent.Direction = Vector2.zero;
//    }

//    public void Exit()
//    {

//    }

//    public void Update()
//    {
//        if (parent.Target != null)
//        {
//            //check range and attack
//            float distance = Vector2.Distance(parent.Target.transform.position, parent.transform.position);

//            if (distance >= parent.AttackRange)
//            {
//                parent.ChangeState(new FollowState());
//            }
//        }
//        else
//        {
//            parent.ChangeState(new IdleState());
//        }
//    }

//    public IEnumerator Attack()
//    {
//        parent.IsAttacking = true;
//        parent.MyAnimator.SetTrigger("attack");

//        yield return new WaitForSeconds(parent.MyAnimator.GetCurrentAnimatorStateInfo(2).length);
//    }
//}

//using System;
//using UnityEngine;

//public class FollowState : IState
//{
//    private Enemy parent;

//    public void Enter(Enemy parent)
//    {
//        this.parent = parent;
//        Debug.Log("enemy in follow state");
//    }

//    public void Exit()
//    {
//        parent.Direction = Vector2.zero;
//    }

//    public void Update()
//    {
//        if (parent.Target != null)
//        {
//            //Find the target's direction
//            parent.Direction = (parent.Target.transform.position - parent.transform.position).normalized;

//            //calculate distance between target and itself
//            float distance = Vector2.Distance(parent.Target.transform.position, parent.transform.position);

//            if (distance <= parent.AttackRange)
//            {
//                parent.ChangeState(new AttackState());
//            }

//        }
//        else
//        {
//            parent.ChangeState(new IdleState());
//        }

//    }

//}

//using UnityEngine;
//using System.Collections;

//public class IdleState: IState
//{
//    private Enemy parent;

//    public void Enter(Enemy parent)
//    {
//        this.parent = parent;
//        Debug.Log("enemy in idle state");
//    }

//    public void Exit()
//    {
//    }

//    public void Update()
//    {
//        if (parent.Target != null)
//        {
//            Debug.Log("target found in Idle");
//            parent.ChangeState(new PathState());
//        }
//    }
//}

//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public class PathState : IState
//{
//    private Enemy parent;
//    private Vector3 destination;
//    private Vector3 current;
//    private Vector3 goal;
//    private Transform transform;

//    public void Enter(Enemy parent)
//    {
//        Debug.Log("enemy in path state");
//        this.parent = parent;
//        this.transform = parent.transform;
//        this.goal = parent.Target.transform.position;

//        parent.MyPath = parent.MyAstar.FindPath(parent.transform.position, goal);
//        this.current = transform.position;
//        this.destination = parent.MyPath.Pop();
//    }


//    public void Exit()
//    {
//        parent.MyPath = null;
//    }

//    public void Update()
//    {
//        if (parent.MyPath != null)
//        {
//            transform.position = Vector2.MoveTowards(transform.position, destination, parent.Speed * Time.deltaTime);

//            parent.Direction = (destination - transform.position).normalized;

//            float distance = Vector2.Distance(destination, transform.position);
//            float totalDistance = Vector2.Distance(parent.Target.transform.position, transform.position);

//            if (totalDistance <= parent.AttackRange)
//            {
//                parent.ChangeState(new AttackState());
//            }

//            if (distance <= 0)
//            {
//                if (parent.MyPath.Count > 0)
//                {
//                    destination = parent.MyPath.Pop();
//                }
//                else
//                {
//                    parent.MyPath = null;
//                    parent.ChangeState(new IdleState());
//                }
//            }
//        }



//    }
//}


