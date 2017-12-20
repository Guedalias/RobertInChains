using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject	Character;
	public float		StartMovementDist;
	public float		DistToChar;
	public float		MaxSpeed;

	private Transform			_transform;
	private CharacterController _cc;

	private Vector3				_goal;
	private bool				_moving;

	private Vector3 _DebugPos;

	// Use this for initialization
	void Start () {
		_cc = Character.GetComponent<CharacterController>();
		_transform = transform;
		_moving = false;
		_goal = _transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Transform	charTrans = Character.transform;
		//p' = p - (n ⋅ (p - o)) * n
		Vector3 projectedPos = charTrans.position - Vector3.Dot(charTrans.position - _transform.position, _transform.forward) * _transform.forward;

		_DebugPos = projectedPos;
		float distance = Vector3.Distance(projectedPos, _transform.position);
		if (distance > StartMovementDist)
		{
			Debug.Log(_goal);
			_moving = true;
		}
		else if (distance < 0.01f)
		{
			_moving = false;
		}

		if (_moving)
		{
			Vector3 vel = Vector3.zero;

			_goal = projectedPos;
			transform.position = Vector3.SmoothDamp(transform.position, _goal, ref vel, .01f, MaxSpeed);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(_DebugPos, 0.1f);
	}
}
