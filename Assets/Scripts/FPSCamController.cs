using UnityEngine;

public class FPSCamController : MonoBehaviour
{
    public GameObject player; // Player referans�
    private Vector3 offset; // Oyuncu ile kamera aras�ndaki mesafe

    void Start()
    {
        // Oyuncu ile kamera aras�ndaki ba�lang�� mesafesini hesapla
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // Kamera, oyuncunun pozisyonuna ve rotasyonuna g�re ayarlan�r
        transform.position = player.transform.position + offset;

        // Kameran�n y�n�, oyuncunun rotasyonuna e�itlenir
        transform.rotation = player.transform.rotation;
    }
}
