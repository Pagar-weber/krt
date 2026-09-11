using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject musuhPrefab;
    public Transform kepalaKereta;

    public int jumlahMusuhAwal = 3;
    public int tambahPerWave = 3;

    [Header("1. Jarak Samping Dari Rel (Kiri/Kanan)")]
    public float minJarakSamping = 20f; // Jarak aman agar tidak terlalu dekat rel
    public float maxJarakSamping = 35f;

    [Header("2. Lokasi Spawn Belakang (Lingkaran Merah)")]
    public float minJarakBelakang = -80f; // Jauh di belakang kamera
    public float maxJarakBelakang = -50f;

    private int wave = 1;
    private int musuhAktif = 0;

    void Start() { MulaiWave(); }

    void Update()
    {
        if (musuhAktif <= 0) MulaiWave();
    }

    void MulaiWave()
    {
        int jumlahMusuh = jumlahMusuhAwal + ((wave - 1) * tambahPerWave);
        musuhAktif = jumlahMusuh;

        for (int i = 0; i < jumlahMusuh; i++)
        {
            SpawnMusuh();
        }
        wave++;
    }

    void SpawnMusuh()
    {
        float sisi = Random.value < 0.5f ? -1f : 1f;
        float randomSamping = Random.Range(minJarakSamping, maxJarakSamping);
        float randomBelakang = Random.Range(minJarakBelakang, maxJarakBelakang);

        // Menghitung titik spawn relatif terhadap rel miring
        Vector3 posisiSpawn = kepalaKereta.position 
            + (kepalaKereta.right * sisi * randomSamping) 
            + (kepalaKereta.forward * randomBelakang);

        Instantiate(musuhPrefab, posisiSpawn, kepalaKereta.rotation);
    }

    public void MusuhMati() { musuhAktif--; }
}