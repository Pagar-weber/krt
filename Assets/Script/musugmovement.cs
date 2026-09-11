using UnityEngine;

public class MusuhMovement : MonoBehaviour
{
    public float speed = 5f;
    private Transform target;

    void Start()
    {
        GameObject kereta = GameObject.Find("KepalaKereta");

        if (kereta != null)
        {
            target = kereta.transform;
        }
        else
        {
            Debug.LogError("KepalaKereta tidak ditemukan!");
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector3 arah = target.position - transform.position;

        // Biar musuh nggak naik/turun
        arah.y = 0;

        transform.position += arah.normalized * speed * Time.deltaTime;
    }
}