using UnityEngine;
using System.Collections.Generic;

public class CactusSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject cactusPrefab;

    [Header("Player / Kereta")]
    public Transform player;

    [Header("Gerakan")]
    public float moveSpeed = 10f;

    [Header("Jumlah Kaktus")]
    public int jumlahKaktus = 8;

    [Header("Jarak Spawn Depan")]
    public float minJarakDepan = 150f;
    public float maxJarakDepan = 250f;

    [Header("Jarak Samping")]
    public float minJarakSamping = 12f;
    public float maxJarakSamping = 25f;

    [Header("Posisi Y")]
    public float posisiY = 0f;

    [Header("Batas Belakang")]
    public float jarakBelakang = 50f;

    private List<GameObject> cactusList =
        new List<GameObject>();

    private bool aktif = true;

    void Start()
    {
        SpawnAwal();
    }

    void Update()
    {
        if (!aktif)
            return;

        for (int i = cactusList.Count - 1; i >= 0; i--)
        {
            GameObject cactus = cactusList[i];

            if (cactus == null)
            {
                cactusList.RemoveAt(i);
                continue;
            }

            // ==================================
            // KAKTUS JALAN KE BELAKANG
            // ==================================

            cactus.transform.position +=
                Vector3.back * moveSpeed * Time.deltaTime;

            // ==================================
            // KAKTUS KELUAR LAYAR
            // ==================================

            if (cactus.transform.position.z <
                player.position.z - jarakBelakang)
            {
                Destroy(cactus);
                cactusList.RemoveAt(i);

                Debug.Log(
                    "🌵 Kaktus keluar. Sisa: "
                    + cactusList.Count
                );

                if (cactusList.Count == 0)
                {
                    Debug.Log(
                        "🌵 SEMUA KAKTUS SUDAH KELUAR!"
                    );
                }
            }
        }
    }

    // ==================================
    // SPAWN AWAL
    // ==================================

    void SpawnAwal()
    {
        for (int i = 0; i < jumlahKaktus; i++)
        {
            GameObject cactus =
                Instantiate(
                    cactusPrefab,
                    PosisiRandom(),
                    cactusPrefab.transform.rotation
                );

            cactusList.Add(cactus);
        }

        Debug.Log(
            "🌵 " + jumlahKaktus +
            " KAKTUS MUNCUL DARI DEPAN!"
        );
    }

    // ==================================
    // POSISI RANDOM
    // ==================================

    Vector3 PosisiRandom()
    {
        float sisi =
            Random.value < 0.5f ? -1f : 1f;

        float jarakSamping =
            Random.Range(
                minJarakSamping,
                maxJarakSamping
            );

        float jarakDepan =
            Random.Range(
                minJarakDepan,
                maxJarakDepan
            );

        Vector3 posisi =
            player.position;

        posisi.x += sisi * jarakSamping;
        posisi.y = posisiY;
        posisi.z += jarakDepan;

        return posisi;
    }

    // ==================================
    // CEK SEMUA KAKTUS HABIS
    // ==================================

    public bool SemuaKaktusHabis()
    {
        return cactusList.Count == 0;
    }

    // ==================================
    // MULAI BATCH BARU
    // ==================================

    public void MulaiKaktus()
    {
        aktif = true;

        // Bersihkan object yang sudah null
        for (int i = cactusList.Count - 1; i >= 0; i--)
        {
            if (cactusList[i] == null)
            {
                cactusList.RemoveAt(i);
            }
        }

        // Kalau sudah habis, spawn batch baru
        if (cactusList.Count == 0)
        {
            SpawnAwal();

            Debug.Log(
                "🌵 BATCH KAKTUS BARU → DARI DEPAN!"
            );
        }
    }
}