using UnityEngine;

public class MusuhMovement : MonoBehaviour
{
    [Header("Kecepatan Maju Ke Samping Kereta")]
    public float speed = 15f;

    [Header("Area Berhenti Sejajar Kereta (Sumbu Z)")]
    [Tooltip("Posisi Z paling belakang di samping gerbong")]
    public float minTargetZ = -15f; 
    [Tooltip("Posisi Z paling depan di samping gerbong")]
    public float maxTargetZ = 15f;  

    private float targetZ;
    private bool sudahSampai = false;

    void Start()
    {
        // Tentukan titik berhenti acak di samping kereta
        targetZ = Random.Range(minTargetZ, maxTargetZ);
    }

    void Update()
    {
        if (sudahSampai) return;

        // Gerakkan Z menuju targetZ
        float currentZ = transform.position.z;
        float newZ = Mathf.MoveTowards(currentZ, targetZ, speed * Time.deltaTime);

        // Kunci sumbu X (jarak dari rel) dan Y (tinggi)
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

        // Jika sudah sampai di samping kereta, matikan pergerakan
        if (Mathf.Abs(newZ - targetZ) < 0.1f)
        {
            sudahSampai = true;
        }
    }
}