﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmanSearchState : StateMachineBehaviour
{
    AStarNavigation astar;
    HitmanFSM hitFSM;
    GameObject visibleTarget;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        astar = animator.GetComponent<AStarNavigation>();
        hitFSM = animator.GetComponent<HitmanFSM>();
        visibleTarget = hitFSM.GetVisibleTargetObject();
        if (visibleTarget != null)
        {
            //transition to assassinate
            animator.SetBool("FoundTarget", true);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        visibleTarget = hitFSM.GetVisibleTargetObject();
        if (visibleTarget != null)
        {
            //transition to assassinate
            animator.SetBool("FoundTarget", true);
        }
        else if (astar.reachedGoal)
        {
            //transition to Set New Location
            animator.SetBool("ReachedWayPoint",true);
        }
        //Check if chasing target & being chased & have smoke grenades available
        if (hitFSM.isBeingChased)
        {
            hitFSM.CreateSmokeBubble();
        }
        if (!hitFSM.GetContractObject().activeSelf)
        {
            //transition to new contract
            animator.SetBool("IsTargetDead", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}