using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PointCloud : MonoBehaviour
{
	public string fileName;
	private MeshFilter meshFilter;

	private readonly List<Vector3> _points = new();
	private readonly List<int> _indexes = new();

	void Start()
	{
		meshFilter = gameObject.GetComponent<MeshFilter>();
		meshFilter.mesh.SetIndices(new int[]{}, MeshTopology.Points, 0);
		FromFile(fileName);
		//Random();
	}

	void RemoveAllPoints()
	{
		_points.Clear();
		_indexes.Clear();
	}

	void AddPoint(Vector3 p)
	{
		_points.Add(p);
		_indexes.Add(_indexes.Count);
	}

	private void UpdateMesh()
	{
		meshFilter.mesh.SetVertices(_points);
		meshFilter.mesh.SetIndices(_indexes, MeshTopology.Points, 0);
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
	void FromFile(string fileName)
	{

		RemoveAllPoints();

		var lines = File.ReadAllLines(fileName);
		for (int i = 11; i < lines.Length; i++)
		{
			var vals = lines[i].Split(' ');
			AddPoint(new Vector3(float.Parse(vals[0]), float.Parse(vals[1]), float.Parse(vals[2])));
		}

		UpdateMesh();
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
	void Random()
	{
		var mf = gameObject.GetComponent<MeshFilter>();
		Mesh m = mf.mesh;

		const int amt = 1000;
		var points = new Vector3[amt];
		var indexes = new int[amt];

		for (int i = 0; i < points.Length; i++)
		{
			points[i] = new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10));
			indexes[i] = i;
		}

		m.vertices = points;

		m.SetIndices(indexes, MeshTopology.Points, 0);

		mf.sharedMesh = m;
	}

}
