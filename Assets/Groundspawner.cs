using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public Transform ground;
    public Transform player;

    public float groundLength = 100f;
    public float moveSpeed = 10f;
    public float repeatDistance = 100f;

    void Update()
    {
        // Ground bergerak mundur
        ground.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        // Kalau ground sudah terlalu jauh di belakang player
        if (ground.position.z <= player.position.z - repeatDistance)
        {
            // Pindahkan ground ke depan
            ground.position += Vector3.forward * groundLength;
        }
    }
}