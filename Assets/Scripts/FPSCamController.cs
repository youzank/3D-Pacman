using UnityEngine;

public class FPSCamController : MonoBehaviour
{
    public GameObject player; // Player referansý
    private Vector3 offset; // Oyuncu ile kamera arasýndaki mesafe

    void Start()
    {
        // Oyuncu ile kamera arasýndaki baþlangýç mesafesini hesapla
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // Kamera, oyuncunun pozisyonuna ve rotasyonuna göre ayarlanýr
        transform.position = player.transform.position + offset;

        // Kameranýn yönü, oyuncunun rotasyonuna eþitlenir
        transform.rotation = player.transform.rotation;
    }
}
