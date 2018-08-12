using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public float health = 100f;
	public GameObject gameObjectDestroy;

	public void TakeDamage(float damageAmount){
		health -= damageAmount;
		Debug.Log("Target Health: " + health);
		if(health <= 0f){
			Die();
		}
	}

	public void Die(){
		gameObjectDestroy.SetActive(true);
		Destroy(gameObject);
	}
}
