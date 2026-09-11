using UnityEngine;
using System.Collections.Generic;

public class KeretaManager : MonoBehaviour
{
    public GameObject gerbongPrefab;
    public Transform kepalaKereta;

    public float jarakGerbong = 5f;

    private List<Transform> gerbongList = new List<Transform>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnGerbong();
        }
    }

    void SpawnGerbong()
    {
        Vector3 spawnPosition;

        if (gerbongList.Count == 0)
        {
            // Gerbong pertama → di belakang kepala kereta
            spawnPosition =
                kepalaKereta.position -
                kepalaKereta.forward * jarakGerbong;
        }
        else
        {
            // Gerbong berikutnya → di belakang gerbong terakhir
            Transform gerbongTerakhir =
                gerbongList[gerbongList.Count - 1];

            spawnPosition =
                gerbongTerakhir.position -
                kepalaKereta.forward * jarakGerbong;
        }

        GameObject gerbongBaru = Instantiate(
            gerbongPrefab,
            spawnPosition,
            kepalaKereta.rotation
        );

        gerbongList.Add(gerbongBaru.transform);
    }
}