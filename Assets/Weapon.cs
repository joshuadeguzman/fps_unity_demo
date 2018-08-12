using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float damageAmount = 100f;
	public float range = 100f;
	public Camera playerCamera;

	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		if(Input.GetButton("Fire1")){
			Shoot();
		}
	}

	private void Shoot(){
		RaycastHit hit;
		if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range)){
			Debug.Log(hit.transform.name);
			Target target = hit.transform.GetComponent<Target>();
			if(target != null){
				target.TakeDamage(damageAmount);
			}
		}
	}
}
