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
		Vector3 textPos = Camera.main.transform.up * 1.0f - Camera.main.transform.forward * 3.0f;
		// ダメージ表記を出すオブジェクトの高さに合わせて、高さを決める
		if (col == GameObject.Find("TreasureBox"))// クリスタル
		{
			textPos = Camera.main.transform.up * 1.5f - Camera.main.transform.forward * 1.0f;
			Debug.Log("クリスタル");
		}
		else if (col == GameObject.Find("enemyShot"))// 射撃の敵
		{
			textPos = Camera.main.transform.up * 1.0f - Camera.main.transform.forward * 1.0f;
			Debug.Log("射撃");
		}
		else if (col == GameObject.Find("enemyFry"))// 飛翔の敵
		{
			textPos = Camera.main.transform.up * 0.5f - Camera.main.transform.forward * 1.0f;
			Debug.Log("飛翔");
		}
		else if (col == GameObject.Find("enemyBomb"))// 爆発の敵
		{
			textPos = Camera.main.transform.up * 4.0f - Camera.main.transform.forward * 3.0f;
			Debug.Log("爆発");
		}
		//　DamageUIをインスタンス化。登場位置は接触したコライダの中心からカメラの方向に少し寄せた位置
		var obj = Instantiate(damageUI, col.transform.position + textPos, Quaternion.identity);
		obj.transform.SetParent(col.transform);// 親（ダメージを食らったもの）の子オブジェクトにすることで、ダメージ表記をついてこさせるようにする
		Debug.Log("今当たったものの名前 : " + col + " textPos : " + textPos);
	}
}
