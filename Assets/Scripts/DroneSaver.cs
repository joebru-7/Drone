using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;
using UnityEngine;
using UnityEngine.UI;

public class DroneSaver : MonoBehaviour
{

	public void Save()
	{

		var ros = FindObjectOfType<ROSConnection>();
		ros.Publish("a", new());

		var drones = FindObjectsOfType<DroneControl>();
		string toWrite = "";

		foreach (var drone in drones)
		{
			toWrite += drone.GetData();
		}

		var tmp = gameObject.GetComponent<TMPro.TMP_InputField>();
		string t = tmp.text;
		try
		{
			File.WriteAllText(t, toWrite);

		}
		catch (System.Exception)
		{
			Logger.Log("Failed to save - could not open file");
			return;
		}
	
		Logger.Log("Saved to " + new FileInfo(t).FullName) ;
	}
}
