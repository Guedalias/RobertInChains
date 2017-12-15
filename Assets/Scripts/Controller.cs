using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public float Speed = 5f;
	public float JumpHeight = 2f;
	public float Gravity = -20f;
	public float GroundDistance = 0.2f;

	public float JumpFallMultiplier = 1.5f;
	
	public Vector3 Drag;

	private CharacterController _controller;
	private Vector3 _velocity;

	// Use this for initialization
	void Start () {
		_controller = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		bool isGrounded = _controller.isGrounded;
		if (isGrounded && _velocity.y < 0)
			_velocity.y = 0f;

		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
		_controller.Move(move * Time.deltaTime * Speed);

		if (move != Vector3.zero)
			transform.forward = move;

		if (isGrounded == true)
		{
			if (Input.GetButtonDown("Jump"))
			{
				_velocity.y += Mathf.Sqrt(JumpHeight * -2f * Gravity);
			}
		}
		if (_velocity.y < 0)
			_velocity.y += JumpFallMultiplier * Gravity * Time.deltaTime;
		else
			_velocity.y += Gravity * Time.deltaTime;
		
		_velocity.x /= 1 + Drag.x * Time.deltaTime;
		//_velocity.y /= 1 + Drag.y * Time.deltaTime;
		

		_controller.Move(_velocity * Time.deltaTime);
	}
}
