using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCount : MonoBehaviour
{
	//　DamageUIプレハブ
	[SerializeField]private static GameObject damageUI;
	public Text text;
	public static float damageInput { set; get; }// 値を入れると表示ダメージが変更される

	private void Start()
	{
		damageUI = (GameObject)Resources.Load("DamageUI");// 動的にprefab（DamageUI）をアタッチ
	}

	public static void Damage(GameObject col)
	{
		//　DamageUIをインスタンス化。登場位置は接触したコライダの中心からカメラの方向に少し寄せた位置
		var obj = Instantiate(damageUI, col.transform.position - Camera.main.transform.forward * 1.0f + Camera.main.transform.up * 2.0f, Quaternion.identity);
		obj.transform.SetParent(col.transform);
	}
}
