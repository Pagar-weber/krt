using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Musuh musuh = other.GetComponent<Musuh>();

        if (musuh != null)
        {
            musuh.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}