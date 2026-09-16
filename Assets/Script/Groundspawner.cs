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

    [Header("Jeda Wave Setelah Kaktus")]
    public float jedaWave = 3f;

    [Header("Panel Stasiun")]
    public StasiunPanel stationPanel;

    private int groundLewat = 0;

    private bool stasiunBerjalan = false;
    private bool diStasiun = false;
    private bool keluarStasiun = false;

    private bool menungguWave = false;
    private float timerWave = 0f;

    private WaveManager waveManager;


    // ==================================
    // START
    // ==================================

    void Start()
    {
        Time.timeScale = 1f;

        groundBiasa.SetActive(true);
        groundStasiun.SetActive(false);

        waveManager =
            FindFirstObjectByType<WaveManager>();

        if (cactusSpawner == null)
        {
            cactusSpawner =
                FindFirstObjectByType<CactusSpawner>();
        }

        if (stationPanel == null)
        {
            stationPanel =
                FindFirstObjectByType<StasiunPanel>();
        }
    }


    // ==================================
    // UPDATE
    // ==================================

    void Update()
    {
        // ==================================
        // WAVE MENUNGGU SETELAH KAKTUS
        // ==================================

        if (menungguWave)
        {
            timerWave += Time.deltaTime;

            if (timerWave >= jedaWave)
            {
                menungguWave = false;
                timerWave = 0f;

                if (waveManager != null)
                {
                    Debug.Log(
                        "🔥 KAKTUS SUDAH JALAN → WAVE BARU!"
                    );

                    waveManager.MulaiWaveBerikutnya();
                }
            }
        }


        // ==================================
        // DI STASIUN
        // ==================================

        if (diStasiun)
        {
            // Tidak pakai Space lagi.
            // Keluar lewat tombol panel.

            return;
        }


        // ==================================
        // GROUND BIASA
        // ==================================

        groundBiasa.transform.position +=
            Vector3.back *
            moveSpeed *
            Time.deltaTime;


        // ==================================
        // STASIUN BERGERAK
        // ==================================

        if (stasiunBerjalan || keluarStasiun)
        {
            groundStasiun.transform.position +=
                Vector3.back *
                moveSpeed *
                Time.deltaTime;


            // ==================================
            // STASIUN DATANG
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
            // CEK MUSUH
            // ==================================

            if (waveManager != null &&
                waveManager.SemuaMusuhMati())
            {
                // ==================================
                // CEK KAKTUS
                // ==================================

                if (cactusSpawner != null &&
                    !cactusSpawner.SemuaKaktusHabis())
                {
                    Debug.Log(
                        "⏳ Musuh habis, kaktus masih jalan..."
                    );

                    return;
                }


                // ==================================
                // SEMUA HABIS
                // ==================================

                groundLewat++;

                Debug.Log(
                    "👾 MUSUH HABIS + 🌵 KAKTUS HABIS | Ground: "
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
    // MUNCULKAN STASIUN
    // ==================================

    void MunculkanStasiun()
    {
        Debug.Log(
            "🚉 SEMUA SYARAT TERPENUHI → STASIUN MUNCUL!"
        );

        stasiunBerjalan = true;

        Vector3 posisi =
            groundStasiun.transform.position;

        posisi.z =
            player.position.z +
            jarakStasiun;

        groundStasiun.transform.position =
            posisi;

        groundStasiun.SetActive(true);
    }


    // ==================================
    // SAMPAI STASIUN
    // ==================================

    void SampaiStasiun()
    {
        Debug.Log(
            "🚉 SAMPAI STASIUN!"
        );

        stasiunBerjalan = false;
        diStasiun = true;


        // ==================================
        // BUKA PANEL
        // ==================================

        if (stationPanel != null)
        {
            Debug.Log(
                "📋 MEMBUKA PANEL STASIUN!"
            );

            stationPanel.BukaPanel();
        }
        else
        {
            Debug.LogError(
                "❌ STATION PANEL BELUM TERHUBUNG!"
            );
        }
    }


    // ==================================
    // KELUAR DARI STASIUN
    // DIPANGGIL TOMBOL
    // ==================================

    public void LanjutDariStasiun()
    {
        if (!diStasiun)
            return;


        Debug.Log(
            "➡️ TOMBOL DITEKAN → KERETA JALAN!"
        );


        // Pastikan game berjalan lagi
        Time.timeScale = 1f;

        // Tutup status stasiun
        diStasiun = false;

        // Stasiun mulai bergerak keluar
        keluarStasiun = true;

        groundBiasa.SetActive(true);
    }


    // ==================================
    // CEK STASIUN KELUAR
    // ==================================

    void CekStasiunKeluar()
    {
        if (!keluarStasiun)
            return;


        if (groundStasiun.transform.position.z <
            player.position.z - jarakKeluarStasiun)
        {
            Debug.Log(
                "🚉 STASIUN KELUAR LAYAR → OFF"
            );

            groundStasiun.SetActive(false);

            keluarStasiun = false;
            groundLewat = 0;


            // ==================================
            // KAKTUS MUNCUL DULU
            // ==================================

            if (cactusSpawner != null)
            {
                Debug.Log(
                    "🌵 STASIUN HILANG → KAKTUS MUNCUL DULU!"
                );

                cactusSpawner.MulaiKaktus();
            }


            // ==================================
            // WAVE DITUNDA
            // ==================================

            menungguWave = true;
            timerWave = 0f;

            Debug.Log(
                "⏳ WAVE DITUNDA " +
                jedaWave +
                " DETIK"
            );
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