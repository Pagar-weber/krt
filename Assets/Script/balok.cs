 using UnityEngine;

public class LookAtMusuh : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 0.5f;
    public float range = 30f;

    [Header("Pengaturan Kecepatan Putar Turret")]
    [Tooltip("Kecepatan putar turret (derajat/detik). Makin besar nilainya, makin cepat nengok ke musuh baru.")]
    public float rotationSpeed = 500f; 

    [Header("Pengaturan Peluru")]
    [Tooltip("Kecepatan meluncur peluru saat ditembakkan.")]
    public float bulletSpeed = 30f;

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

                // Memutar turret secara bertahap/halus sesuai rotationSpeed
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, 
                    targetRotation, 
                    rotationSpeed * Time.deltaTime
                );
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
        if (bulletPrefab == null || firePoint == null) return;

        // Spawn peluru
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        // Memberikan dorongan kecepatan ke peluru (jika prefab peluru pakai Rigidbody)
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }
    }
}