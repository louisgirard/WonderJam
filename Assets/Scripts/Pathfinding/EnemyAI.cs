using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	Transform target;
	public float speed = 20;

	Vector2[] path;
	int targetIndex;

	Animator animator;

	void Start() {
		target = FindObjectOfType<PlayerHealth>().transform;
		animator = GetComponent<Animator>();
		StartCoroutine (RefreshPath ());
	}

	IEnumerator RefreshPath() {
		Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially
			
		while (true) {
			if (targetPositionOld != (Vector2)target.position) {
				targetPositionOld = (Vector2)target.position;

				path = Pathfinding.RequestPath (transform.position, target.position);
				StopCoroutine (FollowPath());
				StartCoroutine (FollowPath());
			}

			yield return new WaitForSeconds (.25f);
		}
	}
		
	IEnumerator FollowPath() {
		if (path.Length > 0) {
			targetIndex = 0;
			Vector2 currentWaypoint = path [0];

			while (true) {
				if ((Vector2)transform.position == currentWaypoint) {
					targetIndex++;
					if (targetIndex >= path.Length) {
						yield break;
					}
					currentWaypoint = path [targetIndex];
				}

				animator.SetTrigger("walk");
				transform.position = Vector2.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
				FaceTarget();

				yield return null;
			}
		}
	}

	private void FaceTarget()
	{
		Vector2 direction = (target.position - transform.position).normalized;
		bool facingRight = transform.localScale.x >= 0;
		if (facingRight && direction.x < 0 || !facingRight && direction.x >= 0)
		{
			Vector2 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				//Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
