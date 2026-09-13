using UnityEngine;
using System.Collections.Generic;

public class CactusSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject cactusPrefab;

    [Header("Ground")]
    public Transform groundBiasa;

    [Header("Jumlah Kaktus")]
    public int jumlahKaktus = 8;

    [Header("Area Spawn X")]
    public float minX = -15f;
    public float maxX = 15f;

    [Header("Area Spawn Z")]
    public float minZ = -45f;
    public float maxZ = 45f;

    [Header("Posisi Y")]
    public float posisiY = 0f;

    private List<GameObject> cactusList =
        new List<GameObject>();


    void Start()
    {
        SpawnKaktus();
    }


    public void SpawnKaktus()
    {
        HapusKaktus();

        for (int i = 0; i < jumlahKaktus; i++)
        {
            float randomX =
                Random.Range(minX, maxX);

            float randomZ =
                Random.Range(minZ, maxZ);

            Vector3 posisi =
                groundBiasa.position;

            posisi.x += randomX;
            posisi.y += posisiY;
            posisi.z += randomZ;

            GameObject cactus =
                Instantiate(
                    cactusPrefab,
                    posisi,
                    Quaternion.identity
                );

            // Kaktus mengikuti ground
            cactus.transform.SetParent(
                groundBiasa
            );

            cactusList.Add(cactus);
        }
    }


    void HapusKaktus()
    {
        foreach (GameObject cactus in cactusList)
        {
            if (cactus != null)
            {
                Destroy(cactus);
            }
        }

        cactusList.Clear();
    }
}