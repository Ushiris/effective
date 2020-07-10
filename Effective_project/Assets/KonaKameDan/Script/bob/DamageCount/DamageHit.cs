using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHit : MonoBehaviour
{	void OnParticleCollision(GameObject col)
	{
		if (col.tag == "Enemy")
		{
			col.transform.root.GetComponent<DamageCount>().Damage(col);
		}
	}
}