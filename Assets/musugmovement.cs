using UnityEngine;

public class MusuhMovement : MonoBehaviour
{
    public Transform kereta;
    public float speed = 5f;

    void Update()
    {
        if (kereta == null) return;

        Vector3 arah = (kereta.position - transform.position).normalized;

        transform.position += arah * speed * Time.deltaTime;
    }
}