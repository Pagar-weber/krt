using UnityEngine;

public class RandomSpawnPosition : MonoBehaviour
{
    [Header("Referensi Kereta")]
    public Transform keretaTransform; // Posisi kereta sebagai pusat acuan

    [Header("Rentang Jarak Samping (X) Dari Kereta")]
    [Tooltip("Jarak paling dekat dari rel kereta")]
    public float minX = 15f; 
    [Tooltip("Jarak paling jauh dari rel kereta")]
    public float maxX = 30f;

    [Header("Rentang Depan / Belakang (Z)")]
    public float minZ = 20f;
    public float maxZ = 60f;

    [Header("Tinggi Posisi Tanah (Y)")]
    public float spawnY = 0f;

    // Fungsi untuk mendapatkan koordinat acak
    public Vector3 GetRandomPosition()
    {
        // 1. Tentukan acak: Kanan (true) atau Kiri (false)
        bool isRightSide = Random.value > 0.5f;

        // 2. Acak nilai X (jarak dari kereta)
        float randomX = Random.Range(minX, maxX);

        // Jika terpilih Kiri, ubah koordinat X menjadi negatif
        if (!isRightSide)
        {
            randomX = -randomX;
        }

        // 3. Acak nilai Z (posisi sepanjang rel)
        float randomZ = Random.Range(minZ, maxZ);

        // 4. Hitung posisi relatif terhadap posisi kereta saat ini
        Vector3 acuanPosisi = (keretaTransform != null) ? keretaTransform.position : Vector3.zero;

        Vector3 finalSpawnPos = new Vector3(
            acuanPosisi.x + randomX,
            spawnY,
            acuanPosisi.z + randomZ
        );

        return finalSpawnPos;
    }

    // Menampilkan kotak hijau (Gizmos) di Scene View untuk memudahkan setting area spawn
    private void OnDrawGizmosSelected()
    {
        Vector3 posPusat = (keretaTransform != null) ? keretaTransform.position : transform.position;
        Gizmos.color = Color.green;

        // Area Kanan
        Vector3 centerRight = posPusat + new Vector3((minX + maxX) / 2f, spawnY, (minZ + maxZ) / 2f);
        Vector3 sizeRight = new Vector3(maxX - minX, 1f, maxZ - minZ);
        Gizmos.DrawWireCube(centerRight, sizeRight);

        // Area Kiri
        Vector3 centerLeft = posPusat + new Vector3(-(minX + maxX) / 2f, spawnY, (minZ + maxZ) / 2f);
        Vector3 sizeLeft = new Vector3(maxX - minX, 1f, maxZ - minZ);
        Gizmos.DrawWireCube(centerLeft, sizeLeft);
    }
}