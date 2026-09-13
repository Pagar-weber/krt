using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Ground")]
    public GameObject groundBiasa;
    public GameObject groundStasiun;

    [Header("Player")]
    public Transform player;

    [Header("Setting")]
    public float moveSpeed = 10f;
    public float groundLength = 100f;

    [Header("Jarak Stasiun")]
    public float jarakStasiun = 200f;

    [Header("Posisi Berhenti Stasiun")]
    public float offsetBerhenti = -5f;

    [Header("Jarak Stasiun Keluar")]
    public float jarakKeluarStasiun = 250f;

    [Header("Cactus")]
    public CactusSpawner cactusSpawner;

    private int groundLewat = 0;

    private bool stasiunBerjalan = false;
    private bool diStasiun = false;
    private bool keluarStasiun = false;

    private WaveManager waveManager;

    void Start()
    {
        groundBiasa.SetActive(true);
        groundStasiun.SetActive(false);

        waveManager = FindFirstObjectByType<WaveManager>();

        // Cari CactusSpawner otomatis kalau belum diisi
        if (cactusSpawner == null)
        {
            cactusSpawner =
                FindFirstObjectByType<CactusSpawner>();
        }
    }

    void Update()
    {
        // ==================================
        // SUDAH DI STASIUN
        // ==================================
        if (diStasiun)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                KeluarStasiun();
            }

            return;
        }

        // ==================================
        // GROUND BIASA BERGERAK
        // ==================================
        groundBiasa.transform.position +=
            Vector3.back * moveSpeed * Time.deltaTime;


        // ==================================
        // STASIUN BERGERAK
        // ==================================
        if (stasiunBerjalan || keluarStasiun)
        {
            groundStasiun.transform.position +=
                Vector3.back * moveSpeed * Time.deltaTime;

            // ==================================
            // STASIUN SEDANG DATANG
            // ==================================
            if (stasiunBerjalan &&
                groundStasiun.transform.position.z <=
                player.position.z + offsetBerhenti)
            {
                SampaiStasiun();
            }

            return;
        }


        // ==================================
        // GROUND BIASA BERGANTI
        // ==================================
        if (groundBiasa.transform.position.z <=
            player.position.z - groundLength)
        {
            Vector3 posisi =
                groundBiasa.transform.position;

            posisi.z += groundLength;

            groundBiasa.transform.position =
                posisi;

            Debug.Log("GROUND BERGANTI");

            // ==================================
            // CEK SEMUA MUSUH MATI
            // ==================================
            if (waveManager != null &&
                waveManager.SemuaMusuhMati())
            {
                // ==============================
                // KAKTUS STOP
                // ==============================
                if (cactusSpawner != null)
                {
                    cactusSpawner.StopKaktus();
                }

                groundLewat++;

                Debug.Log(
                    "Ground setelah semua musuh mati: "
                    + groundLewat + "/2"
                );

                if (groundLewat >= 2)
                {
                    MunculkanStasiun();
                }
            }
        }
    }


    // ==================================
    // STASIUN MUNCUL DI DEPAN
    // ==================================
    void MunculkanStasiun()
    {
        Debug.Log("🚉 STASIUN MUNCUL DI DEPAN!");

        stasiunBerjalan = true;

        Vector3 posisi =
            groundStasiun.transform.position;

        posisi.z =
            player.position.z + jarakStasiun;

        groundStasiun.transform.position =
            posisi;

        groundStasiun.SetActive(true);
    }


    // ==================================
    // SAMPAI STASIUN
    // ==================================
    void SampaiStasiun()
    {
        Debug.Log("🚉 SAMPAI STASIUN!");

        stasiunBerjalan = false;
        diStasiun = true;

        Debug.Log(
            "🚉 BERHENTI! Offset: "
            + offsetBerhenti
            + " | Z Stasiun: "
            + groundStasiun.transform.position.z
            + " | Z Kereta: "
            + player.position.z
        );
    }


    // ==================================
    // SPACE
    // ==================================
    void KeluarStasiun()
    {
        Debug.Log("SPACE → KELUAR STASIUN");

        diStasiun = false;

        // Stasiun mulai bergerak keluar
        keluarStasiun = true;

        groundBiasa.SetActive(true);

        // WAVE BELUM DIMULAI DI SINI!
        // Kaktus juga BELUM dimulai di sini!
    }


    // ==================================
    // CEK STASIUN SUDAH KELUAR
    // ==================================
    void CekStasiunKeluar()
    {
        if (!keluarStasiun)
            return;

        if (groundStasiun.transform.position.z <
            player.position.z - jarakKeluarStasiun)
        {
            Debug.Log("🚉 STASIUN KELUAR LAYAR → OFF");

            groundStasiun.SetActive(false);

            keluarStasiun = false;
            groundLewat = 0;

            // ==================================
            // WAVE BARU
            // ==================================
            if (waveManager != null)
            {
                Debug.Log(
                    "🔥 STASIUN HILANG → WAVE BARU DIMULAI!"
                );

                waveManager.MulaiWaveBerikutnya();
            }

            // ==================================
            // KAKTUS MULAI LAGI
            // ==================================
            if (cactusSpawner != null)
            {
                Debug.Log(
                    "🌵 STASIUN HILANG → KAKTUS MULAI LAGI!"
                );

                cactusSpawner.MulaiKaktus();
            }
        }
    }


    // ==================================
    // LATE UPDATE
    // ==================================
    void LateUpdate()
    {
        CekStasiunKeluar();
    }
}