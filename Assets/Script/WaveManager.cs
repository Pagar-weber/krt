using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject musuhPrefab;
    public Transform kepalaKereta;

    public int jumlahMusuhAwal = 3;
    public int tambahPerWave = 3;

    public float jarakSpawn = 15f;
    public float jarakSamping = 8f;

    private int wave = 1;
    private int musuhAktif = 0;

    void Start()
    {
        MulaiWave();
    }

    void Update()
    {
        // Kalau semua musuh sudah mati
        if (musuhAktif <= 0)
        {
            // Nanti di sini kita kasih ground khusus
            MulaiWave();
        }
    }

    void MulaiWave()
    {
        int jumlahMusuh = jumlahMusuhAwal + ((wave - 1) * tambahPerWave);

        musuhAktif = jumlahMusuh;

        for (int i = 0; i < jumlahMusuh; i++)
        {
            SpawnMusuh();
        }

        Debug.Log("Wave " + wave + " dimulai!");

        wave++;
    }

    void SpawnMusuh()
    {
        // Random kiri atau kanan
        float sisi = Random.value < 0.5f ? -1f : 1f;

        Vector3 posisi =
            kepalaKereta.position
            - kepalaKereta.forward * jarakSpawn
            + kepalaKereta.right * sisi * jarakSamping;

        Instantiate(
            musuhPrefab,
            posisi,
            kepalaKereta.rotation
        );
    }

    public void MusuhMati()
    {
        musuhAktif--;
    }
}