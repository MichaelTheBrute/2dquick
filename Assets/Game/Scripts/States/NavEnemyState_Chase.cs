using UnityEngine;
using System.Collections;

public class NavEnemyState_Chase : StateMachineBehaviour {

	EnemyController ec = null;
	PlayerController pc = null;

	#region StateMachineBehaviour

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		ec = animator.gameObject.GetComponentInParent<EnemyController> ();

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (pc == null) {
			var players = GameObject.FindObjectsOfType<PlayerController> ();
			if (players.Length > 0) {
				pc = players [0];
			}
		} else {
			ec.GetComponent<NavMeshAgent> ().destination = pc.transform.position;
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//Vector3 nextPosition = GetNextPathPositionAtSpeed ();
		//nextPosition = RoundPositionToNearestInteger ();
		/*if (ec.GetComponent<NavMeshAgent> ().hasPath) {
			var path = ec.GetComponent<NavMeshAgent> ().path;
			for (int i = 0; i < path.corners.Length - 1; i++)
			{
				//Graphics.
				Debug.DrawLine(path.corners[i] + new Vector3(0,1,0), path.corners[i + 1] + new Vector3(0,1,0), Color.red);
			}
		}*/
	}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
	#endregion
}
