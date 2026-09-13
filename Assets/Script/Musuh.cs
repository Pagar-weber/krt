using UnityEngine;

public class Musuh : MonoBehaviour
{
    public int maxHP = 5;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        Debug.Log("Musuh kena! HP: " + currentHP);

        if (currentHP <= 0)
        {
            Mati();
        }
    }

    void Mati()
    {
        Debug.Log("Musuh mati!");

        // Beri tahu WaveManager
        WaveManager waveManager =
            FindFirstObjectByType<WaveManager>();

        if (waveManager != null)
        {
            waveManager.MusuhMati();
        }

        Destroy(gameObject);
    }
}