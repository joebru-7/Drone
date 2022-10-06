using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl : MonoBehaviour
{
	public Material SelMat;
	public Material UnselMat;

	private MeshRenderer MeshRenderer;

	public float Pitch = 0;
	public float Roll = 0;
	public float Yaw = 0;

	public void Start()
	{
		MeshRenderer = GetComponent<MeshRenderer>();
		MeshRenderer.material = UnselMat;
	}

	public void Update()
	{
		transform.rotation = Quaternion.Euler(Pitch, Roll, Yaw);
	}

	public void SetSelected(bool isSelected)
	{
		if (isSelected)
		{
			MeshRenderer.material = SelMat;
		}
		else
		{
			MeshRenderer.material = UnselMat;

		}
	}

	internal string GetData()
	{
		string s = "floa32 {}";
		string retstring;
		//string.Format(s,)
		
		return "" + transform.position + transform.rotation; 
	}
}
