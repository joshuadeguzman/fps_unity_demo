using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 10f;
    public float nextFire = 0f;
    public float damageAmount = 100f;
    public float range = 100f;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public ParticleSystem impactDefault;
    public ParticleSystem impactSand;
    public ParticleSystem impactConcrete;
    public ParticleSystem impactWood;
    public float hitForce = 60f;
    public AudioClip soundFireShot;
    private AudioSource weaponAudioSource;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;

    // Use this for initialization
    private void Start()
    {
        muzzleFlash.Stop();
        weaponAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            PlayFireSound();
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {

            // Reduce health amount of the specified target
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damageAmount);
            }

            // Slightly move the object when being hit by the weapon
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }

            // Determine what type of object is being hit based on tag
            ParticleSystem impactEffect = impactDefault;
            if (hit.transform.gameObject.tag != "" || hit.transform.gameObject.tag != "Untagged")
            {
                switch (hit.transform.gameObject.tag)
                {
                    case "Wood":
                        impactEffect = impactWood;
                        break;
                    case "Sand":
                        impactEffect = impactSand;
                        break;
                    case "Concrete":
                        impactEffect = impactConcrete;
                        break;
                }
            }

            // Render impact effect
            GameObject hitObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)).gameObject;
            Destroy(hitObject, 2f);
        }
    }

    private void PlayFireSound()
    {
        weaponAudioSource.pitch = Random.Range(lowPitchRange, highPitchRange);
        weaponAudioSource.PlayOneShot(soundFireShot);
    }
}
