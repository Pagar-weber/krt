using UnityEngine;

public class LookAtMusuh : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 0.5f;
    public float range = 30f;

    private float nextFireTime;
    private Transform target;

    void Update()
    {
        CariMusuh();

        if (target != null)
        {
            Vector3 arah = target.position - transform.position;

            // Jangan nengok atas/bawah
            arah.y = 0;

            if (arah != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(arah);
                transform.rotation = targetRotation;
            }

            // Nembak
            if (Time.time >= nextFireTime)
            {
                Tembak();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void CariMusuh()
    {
        GameObject[] semuaMusuh = GameObject.FindGameObjectsWithTag("Musuh");

        float jarakTerdekat = Mathf.Infinity;
        Transform musuhTerdekat = null;

        foreach (GameObject musuh in semuaMusuh)
        {
            float jarak = Vector3.Distance(
                transform.position,
                musuh.transform.position
            );

            if (jarak < jarakTerdekat && jarak <= range)
            {
                jarakTerdekat = jarak;
                musuhTerdekat = musuh.transform;
            }
        }

        target = musuhTerdekat;
    }

    void Tembak()
    {
        Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );
    }
}