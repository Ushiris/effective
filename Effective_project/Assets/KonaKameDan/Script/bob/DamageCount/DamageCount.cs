using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCount : MonoBehaviour
{
	//　DamageUIプレハブ
	[SerializeField]
	private GameObject damageUI;

	public void Damage(GameObject col)
	{
		//　DamageUIをインスタンス化。登場位置は接触したコライダの中心からカメラの方向に少し寄せた位置
		var obj = Instantiate<GameObject>(damageUI, col.transform.position - Camera.main.transform.forward * 1.0f, Quaternion.identity);
	}
}
