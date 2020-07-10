using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHit : MonoBehaviour
{
	[SerializeField] private DamageCount damageCount;
	private void Start()
	{
		GameObject damageObject = GameObject.FindWithTag("Enemy");
		damageCount = damageObject.GetComponent<DamageCount>();
	}
	void OnParticleCollision(GameObject col)
	{
		if (col.tag == "Enemy")
		{
			damageCount.Damage(col);
		}
	}
	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.tag == "Enemy")
	//	{
	//		damageCount.test(other);
	//	}
	//}
}