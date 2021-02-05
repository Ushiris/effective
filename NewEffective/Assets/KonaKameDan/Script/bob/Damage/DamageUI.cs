using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
	private Text damageText;
	//　フェードアウトするスピード
	private float fadeOutSpeed = 1f;
	//　移動値
	[SerializeField]
	private float moveSpeed = 0.001f;
	private float viwetime;
	bool viweTime = false;

	void Start()
	{
		damageText = GetComponentInChildren<Text>();
		damageText.text = DamageCount.damageInput.ToString();// ダメージ数入力
		viwetime = Time.deltaTime;
		Invoke("viewTextTime", 0.5f);
	}

	void LateUpdate()
	{
		transform.rotation = Camera.main.transform.rotation;
		if (transform.localScale.x > 0.0f && viweTime)
			transform.localScale -= (Vector3.up * moveSpeed + Vector3.right * moveSpeed) * Time.deltaTime;
		else if(viweTime)
			transform.localScale = new Vector3(0, 0, 0);

		damageText.color = Color.Lerp(damageText.color, new Color(1f, 0f, 0f, 0f), fadeOutSpeed * Time.deltaTime);

		if (damageText.color.a <= 0.1f)
		{
			Destroy(gameObject);
		}
	}
	void viewTextTime()
	{
		viweTime = true;
	}
}
