using UnityEngine;
using System.Collections;

public class EnemyState_Hunting : StateMachineBehaviour {

	EnemyController ec = null;
	PlayerController pc = null;

	#region StateMachineBehaviour

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		ec = animator.gameObject.GetComponentInParent<EnemyController> ();
		var players = GameObject.FindObjectsOfType<PlayerController> ();
		if (players.Length > 0) {
			pc = players [0];
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (EnemyKnowsAboutPlayer ()) {
			HuntThePlayer ();
		}
	}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	#endregion

	bool EnemyKnowsAboutPlayer() {
		return pc != null;
	}

	void HuntThePlayer() {
		if (PlayerIsOutOfAttackRange ()) {
			TryToMoveTowardsThePlayer ();
		} else {
			AttackThePlayer ();
		}
	}

	bool PlayerIsOutOfAttackRange() {
		Debug.Log ("Dt:" + DistanceToPlayer () + " AR:" + AttackRange ());
		return DistanceToPlayer () > AttackRange ();
	}

	void TryToMoveTowardsThePlayer() {
		if (Fatigued ()) {
			return;
		}

		if (ActuallyMoveTowardsPlayer ()) {
			RecordLastMoveTime ();
		}
	}

	bool Fatigued() {
		return SecondsSinceLastMove () < SecondsBetweenMoves ();
	}

	System.DateTime lastMoveTime = System.DateTime.Now;

	float SecondsSinceLastMove() {
		return (float)(System.DateTime.Now - lastMoveTime).TotalSeconds;
	}

	void RecordLastMoveTime() {
		lastMoveTime = System.DateTime.Now;
	}

	bool ActuallyMoveTowardsPlayer() {
		if (pc == null) {
			return false;
		}

		var newPosition = ec.transform.position;
		var playerPosition = pc.transform.position;
		var targetVector = playerPosition - newPosition;
		if (System.Math.Abs (targetVector.x) > System.Math.Abs (targetVector.z)) {
			newPosition.x += Mathf.Sign (targetVector.x);
		} else {
			newPosition.z += Mathf.Sign(targetVector.z);
		}
		if (newPosition != ec.transform.position) {
			ec.transform.position = newPosition;
			return true;
		}
		return false;
	}

	float SecondsBetweenMoves() {
		return 0.3f;
	}

	void AttackThePlayer() {
		Debug.Log ("Attack!!!");
	}

	float DistanceToPlayer() {
		if(pc == null) {
			return float.MaxValue;
		}

		return (pc.transform.position - ec.transform.position).magnitude;
	}

	float AttackRange() {
		return 5;
	}
}
