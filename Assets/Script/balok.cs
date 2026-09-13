using UnityEngine;

public class LookAtMusuh : MonoBehaviour
{
    public GameObject bulletPrefab;

    [Header("Fire Point")]
    public Transform firePoint1;
    public Transform firePoint2;

    public float fireRate = 0.5f;
    public float range = 30f;

    [Header("Pengaturan Kecepatan Putar Turret")]
    public float rotationSpeed = 500f;

    [Header("Pengaturan Peluru")]
    public float bulletSpeed = 30f;

    private float nextFireTime;
    private Transform target;

    // Simpan rotasi awal turret
    private float rotasiX;
    private float rotasiZ;

    void Start()
    {
        rotasiX = transform.eulerAngles.x;
        rotasiZ = transform.eulerAngles.z;
    }

    void Update()
    {
        CariMusuh();

        if (target != null)
        {
            Vector3 arah = target.position - transform.position;

            // Kunci arah vertikal
            arah.y = 0;

            if (arah != Vector3.zero)
            {
                float targetY =
                    Quaternion.LookRotation(arah).eulerAngles.y;

                // Model kebalik 180 derajat
                targetY += 180f;

                Quaternion targetRotation =
                    Quaternion.Euler(
                        rotasiX,
                        targetY,
                        rotasiZ
                    );

                transform.rotation =
                    Quaternion.RotateTowards(
                        transform.rotation,
                        targetRotation,
                        rotationSpeed * Time.deltaTime
                    );
            }

            if (Time.time >= nextFireTime)
            {
                Tembak();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void CariMusuh()
    {
        GameObject[] semuaMusuh =
            GameObject.FindGameObjectsWithTag("Musuh");

        float jarakTerdekat = Mathf.Infinity;
        Transform musuhTerdekat = null;

        foreach (GameObject musuh in semuaMusuh)
        {
            float jarak = Vector3.Distance(
                transform.position,
                musuh.transform.position
            );

            if (jarak < jarakTerdekat &&
                jarak <= range)
            {
                jarakTerdekat = jarak;
                musuhTerdekat = musuh.transform;
            }
        }

        target = musuhTerdekat;
    }

    void Tembak()
    {
        // Lubang 1
        TembakDari(firePoint1);

        // Lubang 2 kalau ada
        if (firePoint2 != null)
        {
            TembakDari(firePoint2);
        }
    }

    void TembakDari(Transform firePoint)
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody rb =
            bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity =
                firePoint.forward * bulletSpeed;
        }
    }
}