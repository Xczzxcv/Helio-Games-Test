using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
	[SerializeField] protected GameObject _model;
	[SerializeField] protected SphereCollider _rollDiceCollider;
	[SerializeField] protected Rigidbody _rigidbody;

	protected int _luck;
	protected bool initialized;
	
	public bool Alive { get; private set; }  = true;
	public int Luck => _luck;

	public abstract void Init(Config config);

	public void Kill()
	{
		Destroy(gameObject);
	}

	protected void OnTriggerEnter(Collider other)
	{
		// if 'other' is NPC. InstanceId check is to prevent double trigger
		if (other.gameObject.layer == gameObject.layer 
			&& other.gameObject.GetInstanceID() > gameObject.GetInstanceID())
		{
			RollDice(other.GetComponent<NPC>());
		}
	}

	protected void RollDice(NPC other)
	{
		int luckSum = Luck + other.Luck;
		int luckRandom = Random.Range(0, luckSum + 1);

		NPC toKill = (luckRandom >= Luck) ? other : this;
		toKill.Kill();
	}
}