using UnityEngine;

public class GunFire : Photon.PunBehaviour{

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 8f;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Camera fpsCam;

    [SerializeField]
    private PhotonView m_photonView;
	
    private float nextTimeToFire = 0f;

	void Update () {
        if(m_photonView.isMine)
        {
            if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                m_photonView.RPC("RPC_Shoot", PhotonTargets.All);
            }
        }
	}

    
    [PunRPC]
    void RPC_Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            print(hit.transform.name);
            Target enemy = hit.transform.GetComponent<Target>();
            if(enemy != null)
                enemy.TakeDamage(damage);

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
        }
    }
}
