using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Control : MonoBehaviour
{
	// deg / s
	public float LookSpeed = 10;

	private float _upDown;
	private Vector2 _moveValue;
	private Vector2 _lockValue;

	public GameObject Pausemenu;
	private bool _isPaused = false;

	private DroneControl _lookedAt;
	private bool isSelecting;

	private float _pitch;
	private float Pitch
	{
		get => _pitch;
		set => _pitch = value > 85f ? 85f : value < -85f ? -85f :value ; 
	}
	private float Yaw;

	public float MoveSpeeed = 1;

	void Update()
	{
		if (_isPaused) return;
		if (!isSelecting)
		{
			Pitch -= _lockValue.y * LookSpeed * Time.deltaTime;
			Yaw += _lockValue.x * LookSpeed * Time.deltaTime;
			transform.rotation = Quaternion.Euler(Pitch, Yaw, 0);

			Vector3 v = (transform.forward * _moveValue.y + transform.up * _upDown + transform.right * _moveValue.x) * (MoveSpeeed * Time.deltaTime);
			//var v = new Vector3(_moveValue.x, _upDown, _moveValue.y) * Time.deltaTime;
			transform.position += v;
		}
		else if(_lookedAt != null)
		{
			_lookedAt.Pitch += _lockValue.y * LookSpeed * Time.deltaTime;
			_lookedAt.Yaw += _lockValue.x * LookSpeed * Time.deltaTime;

			_lookedAt.transform.position += (_lookedAt.transform.forward * _moveValue.y + _lookedAt.transform.up * _upDown + _lookedAt.transform.right * _moveValue.x) * (MoveSpeeed * Time.deltaTime);

			transform.LookAt(_lookedAt.transform);
		}
	}

	public void MoveHorizontaly(InputAction.CallbackContext context)
	{
		_moveValue = context.ReadValue<Vector2>();
	}
	public void MoveVerticaly(InputAction.CallbackContext context)
	{
		_upDown = context.ReadValue<float>();
	}
	public void Look(InputAction.CallbackContext context)
	{
		_lockValue = context.ReadValue<Vector2>();
	}

	public void Fire( InputAction.CallbackContext context)
	{
		//Debug.Log("" + isSelecting + " " + context.performed + " " + (isSelecting ^ context.performed));
		if(isSelecting ^= context.performed)
		{
			Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);
			_lookedAt = hit.collider == null ? null : hit.collider.GetComponentInParent<DroneControl>();
			if (_lookedAt == null)
			{
				isSelecting = false;
				return;
			}
			_lookedAt.SetSelected(true);
		}
		else if(_lookedAt != null)
		{
			_lookedAt.SetSelected(false);
		}


	}

	public void Pause(InputAction.CallbackContext context)
	{
		_isPaused ^= context.performed;
		Pausemenu.SetActive(_isPaused);
		Time.timeScale = _isPaused?1:0;
	}

}
