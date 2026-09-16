using UnityEngine;

public class StasiunPanel : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panel;

    private GroundSpawner groundSpawner;

    void Awake()
    {
        groundSpawner = FindFirstObjectByType<GroundSpawner>();

        // Kalau belum diisi, anggap GameObject ini adalah panelnya
        if (panel == null)
        {
            panel = gameObject;
        }

        panel.SetActive(false);
    }

    // ==================================
    // BUKA PANEL
    // ==================================

    public void BukaPanel()
    {
        Debug.Log("📋 PANEL STASIUN DIBUKA!");

        panel.SetActive(true);

        Time.timeScale = 0f;
    }

    // ==================================
    // UPGRADE
    // ==================================

    public void UpgradeTurret()
    {
        Debug.Log("🔫 PILIH UPGRADE TURRET");

        TutupDanLanjut();
    }

    // ==================================
    // GERBONG
    // ==================================

    public void TambahGerbong()
    {
        Debug.Log("🚃 PILIH TAMBAH GERBONG");

        TutupDanLanjut();
    }

    // ==================================
    // LANJUT
    // ==================================

    public void Lanjut()
    {
        Debug.Log("➡️ PILIH LANJUT");

        TutupDanLanjut();
    }

    // ==================================
    // TUTUP + KELUAR STASIUN
    // ==================================

    void TutupDanLanjut()
    {
        Time.timeScale = 1f;

        panel.SetActive(false);

        if (groundSpawner != null)
        {
            groundSpawner.LanjutDariStasiun();
        }
    }
}