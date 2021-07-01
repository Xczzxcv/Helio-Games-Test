using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class NPCFactory : MonoBehaviour
{
	[SerializeField] private List<GameObject> npcPrefabs;
	[SerializeField] private Transform npcParent;
	[SerializeField] private Config currConfig;
	
	// maybe in the future it will be possible to have many NPC to spawn
	private GameObject currNPC;

	private void Awake()
	{
		currNPC = npcPrefabs[0];
	}

	public void Spawn(Vector3 pos) 
	{
		var newNPC = Instantiate(currNPC, pos, Quaternion.identity, npcParent.transform);
		var npcScript = newNPC.GetComponent<NPC>();
		npcScript.Init(currConfig);
	}
}