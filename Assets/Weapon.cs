using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate = 10f;
	public float nextFire = 0f;
	public float damageAmount = 100f;
	public float range = 100f;
	public Camera playerCamera;
	public ParticleSystem muzzleFlash;
	public ParticleSystem hitEffect;
	public float hitForce = 60f;
	// Use this for initialization
	private void Start () {
		hitEffect.Stop();
	}
	
	// Update is called once per frame
	private void Update () {
		if(Input.GetButton("Fire1") && Time.time >= nextFire){
			nextFire = Time.time + 1f / fireRate;
			Shoot();
		}
	}

	private void Shoot(){
		muzzleFlash.Play();

		RaycastHit hit;
		if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range)){
			Debug.Log(hit.transform.name);
			Target target = hit.transform.GetComponent<Target>();
			if(target != null){
				target.TakeDamage(damageAmount);
			}

			if(hit.rigidbody != null){
				hit.rigidbody.AddForce(-hit.normal * hitForce);
			}

			GameObject hitObject = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal)).gameObject;
			Destroy(hitObject, 0.5f);
		}
	}
}
