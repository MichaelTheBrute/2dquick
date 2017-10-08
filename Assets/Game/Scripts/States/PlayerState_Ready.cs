using UnityEngine;
using System.Collections;

public class PlayerState_Ready : StateMachineBehaviour {

	Transform transform = null;
	PlayerController pc = null;

	#region StateMachineBehaviour
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		pc = animator.gameObject.GetComponentInParent<PlayerController> ();
		transform = pc.transform;
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
		TryMove ();
	}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	#endregion

	float moveDelay { get { return 0.1f; } }
	System.DateTime lastMove = System.DateTime.Now;

	bool Fatigued() {
		return (System.DateTime.Now - lastMove).TotalSeconds < moveDelay;
	}

	bool ApplyMove() {
		if (transform == null) {
			return false;
		}
		Vector3 cp = transform.position;
		cp.x += Input.GetAxisRaw ("Horizontal");
		cp.z += Input.GetAxisRaw ("Vertical");
		if (transform.position != cp) {
			transform.position = cp;
			return true;
		}
		return false;
	}

	void RecordLastMoveTime() {
		lastMove = System.DateTime.Now;
	}

	void TryMove() {
		if (Fatigued ()) {
			return;
		}

		if (ApplyMove ()) {
			RecordLastMoveTime ();
		}
	}
}
