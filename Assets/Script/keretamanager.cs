using UnityEngine;
using System.Collections.Generic;

public class KeretaManager : MonoBehaviour
{
    [Header("Semua Jenis Gerbong")]
    public GameObject[] gerbongPrefabs;

    [Header("Kereta")]
    public Transform kepalaKereta;

    public float jarakGerbong = 5f;

    private List<Transform> gerbongList =
        new List<Transform>();

    // Dipanggil saat player dapat kesempatan upgrade
    public void RandomGerbong()
    {
        if (gerbongPrefabs == null ||
            gerbongPrefabs.Length == 0)
        {
            Debug.LogError("❌ Gerbong prefab belum diisi!");
            return;
        }

        GameObject prefab =
            gerbongPrefabs[
                Random.Range(0, gerbongPrefabs.Length)
            ];

        Vector3 spawnPosition;

        if (gerbongList.Count == 0)
        {
            spawnPosition =
                kepalaKereta.position -
                kepalaKereta.forward * jarakGerbong;
        }
        else
        {
            Transform gerbongTerakhir =
                gerbongList[gerbongList.Count - 1];

            spawnPosition =
                gerbongTerakhir.position -
                kepalaKereta.forward * jarakGerbong;
        }

        GameObject gerbongBaru = Instantiate(
            prefab,
            spawnPosition,
            kepalaKereta.rotation
        );

        gerbongList.Add(gerbongBaru.transform);

        Debug.Log(
            "🚃 GERBONG RANDOM TERPILIH: " +
            prefab.name
        );
    }
}