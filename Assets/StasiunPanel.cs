using UnityEngine;

public class StasiunPanel : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panel;

    private GroundSpawner groundSpawner;
    private KeretaManager keretaManager;

    private bool sudahPilihGerbong = false;

    void Awake()
    {
        groundSpawner =
            FindFirstObjectByType<GroundSpawner>();

        keretaManager =
            FindFirstObjectByType<KeretaManager>();

        if (panel == null)
        {
            panel = gameObject;
        }

        panel.SetActive(false);
    }

    void Update()
    {
        // Hanya bisa R ketika panel sedang terbuka
        if (!panel.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            PilihGerbongRandom();
        }
    }

    public void BukaPanel()
    {
        Debug.Log("📋 PANEL STASIUN DIBUKA!");

        sudahPilihGerbong = false;

        panel.SetActive(true);

        Time.timeScale = 0f;
    }

    void PilihGerbongRandom()
    {
        if (sudahPilihGerbong)
        {
            Debug.Log("❌ R SUDAH DIGUNAKAN DI STASIUN INI!");
            return;
        }

        if (keretaManager == null)
        {
            Debug.LogError("❌ KeretaManager tidak ditemukan!");
            return;
        }

        sudahPilihGerbong = true;

        keretaManager.RandomGerbong();

        Debug.Log("🎲 RANDOM GERBONG BERHASIL!");
    }

    public void Lanjut()
    {
        Debug.Log("➡️ TOMBOL LANJUT DITEKAN!");

        Time.timeScale = 1f;

        panel.SetActive(false);

        if (groundSpawner != null)
        {
            groundSpawner.LanjutDariStasiun();
        }
    }

    public void UpgradeTurret()
    {
        Debug.Log("🔫 UPGRADE TURRET");

        Lanjut();
    }

    public void TambahGerbong()
    {
        // Kalau tombol ini masih mau dipakai,
        // langsung random gerbong juga.
        PilihGerbongRandom();
    }
}