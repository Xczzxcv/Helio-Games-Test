using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Text;

[CreateAssetMenu]
public class Config : ScriptableObject
{
	public float PatrolSpeed => RandGet(_patrolSpeed);
	[SerializeField] private float[] _patrolSpeed;
	public float PatrolRadius => RandGet(_patrolRadius);
	[SerializeField] private float[] _patrolRadius;
	public float PatrolWaitTime => RandGet(_patrolWaitTime);
	[SerializeField] private float[] _patrolWaitTime;
	public int PatrolLuck => RandGet(_patrolLuck);
	[SerializeField] private int[] _patrolLuck;
	public float RollDiceRange => _rollDiceRange;
	[SerializeField] private float _rollDiceRange;
	public Vector2 GroundSize => _groundSize;
	[SerializeField] private Vector2 _groundSize;

	public static string Path = "Assets/Configs/";
	

	private float RandGet(float[] minMaxArr)
	{
		return UnityEngine.Random.Range(minMaxArr[0], minMaxArr[1]);
	}

	private int RandGet(int[] minMaxArr)
	{
		return UnityEngine.Random.Range(minMaxArr[0], minMaxArr[1]);
	}

	public void SaveToFile(string filename)
	{
		FileInfo fi = new FileInfo(Path + filename);

		using (FileStream fs = fi.Create())
		{
			var jsonString = JsonUtility.ToJson(this, true);

			Debug.Log(jsonString);

			var dataBytesArr = Encoding.ASCII.GetBytes(jsonString);
			fs.Write(dataBytesArr, 0, dataBytesArr.Length);
		}
	}

	public void LoadFromFile(string filename)
	{
		FileInfo fi = new FileInfo(Path + filename);

		using (FileStream fs = fi.OpenRead())
		{
			var dataBytesArr = new byte[fi.Length];

			fs.Read(dataBytesArr, 0, dataBytesArr.Length);
			try
			{
				var jsonString = Encoding.ASCII.GetString(dataBytesArr);
				JsonUtility.FromJsonOverwrite(jsonString, this);
			}
			catch (ArgumentException exc)
			{
				Debug.LogError("Can't deserialize config file: " + exc.Message);
			}
		}
	}

	public void LoadFromFileThreading(object filename)
	{
		LoadFromFile((string)filename);
	}
}

