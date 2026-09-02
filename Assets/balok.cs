using UnityEngine;

public class LookAtMusuh : MonoBehaviour
{
    public Transform musuh;

    void Update()
    {
        if (musuh != null)
        {
            Vector3 arah = musuh.position - transform.position;

            // Hilangkan perbedaan ketinggian
            arah.y = 0;

            if (arah != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(arah);
            }
        }
    }
}