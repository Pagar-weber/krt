using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject musuhPrefab;
    public Transform kepalaKereta;

    public int jumlahMusuhAwal = 3;
    public int tambahPerWave = 3;

    [Header("Jarak Samping Dari Rel")]
    public float minJarakSamping = 20f;
    public float maxJarakSamping = 35f;

    [Header("Lokasi Spawn Belakang")]
    public float minJarakBelakang = -80f;
    public float maxJarakBelakang = -50f;

    private int wave = 1;
    private int musuhAktif = 0;

    private bool menungguStasiun = false;

    void Start()
    {
        MulaiWave();
    }

    void Update()
    {
        // Jangan bikin wave baru otomatis.
        // Tunggu sampai stasiun selesai.
    }

    void MulaiWave()
    {
        menungguStasiun = false;

        int jumlahMusuh =
            jumlahMusuhAwal + ((wave - 1) * tambahPerWave);

        musuhAktif = jumlahMusuh;

        for (int i = 0; i < jumlahMusuh; i++)
        {
            SpawnMusuh();
        }

        Debug.Log("WAVE " + wave + " DIMULAI!");

        wave++;
    }

    void SpawnMusuh()
    {
        float sisi = Random.value < 0.5f ? -1f : 1f;

        float randomSamping =
            Random.Range(minJarakSamping, maxJarakSamping);

        float randomBelakang =
            Random.Range(minJarakBelakang, maxJarakBelakang);

        Vector3 posisiSpawn =
            kepalaKereta.position
            + (kepalaKereta.right * sisi * randomSamping)
            + (kepalaKereta.forward * randomBelakang);

        Instantiate(
            musuhPrefab,
            posisiSpawn,
            kepalaKereta.rotation
        );
    }

    public void MusuhMati()
    {
        musuhAktif--;

        Debug.Log("Musuh tersisa: " + musuhAktif);

        if (musuhAktif <= 0)
        {
            menungguStasiun = true;

            Debug.Log("SEMUA MUSUH MATI!");
        }
    }

    public void MulaiWaveBerikutnya()
    {
        if (menungguStasiun)
        {
            MulaiWave();
        }
    }

    public bool SemuaMusuhMati()
    {
        return musuhAktif <= 0;
    }
}