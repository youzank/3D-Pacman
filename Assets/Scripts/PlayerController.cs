using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Movement speed
    public int score = 0; // Toplam puan
    public int lives = 3; // Can sayýsý
    private Vector3 moveDirection = Vector3.zero; // Current direction of movement
    private Vector3 startPosition; // Baþlangýç pozisyonu
    public bool isPowerActive = false; // Power Pellet etkisi
    private float powerDuration = 6f; // Etkinin süresi
    private float powerTimer = 0f;
    public GameObject TeleportPoint1;
    public GameObject TeleportPoint2;
    public GameObject LoseScreen;
    public GameObject Enemy;
    public UIManagerMenu uiManagerMenu;
    public AudioClip scoreSound;
    private AudioSource audioSource;
    private bool isTeleporting = false;
    public int count;

    void Start()
    {
        // Set an initial movement direction
        moveDirection = Vector3.forward; // Start moving forward
        startPosition = transform.position; // Save the starting position
        SoundManager.instance.PlayMusicOnce(SoundManager.instance.startMusic);
        count = 0;
    }

    void Update()
    {
        // Check for input to change direction
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirection = Vector3.forward; // Change direction to up
            transform.rotation = Quaternion.Euler(0, 0, 0); // Face forward
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector3.back; // Change direction to down
            transform.rotation = Quaternion.Euler(0, 180, 0); // Face backward
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector3.left; // Change direction to left
            transform.rotation = Quaternion.Euler(0, 270, 0); // Face left
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector3.right; // Change direction to right
            transform.rotation = Quaternion.Euler(0, 90, 0); // Face right
        }
        // Power Pellet etkisini kontrol et
        if (isPowerActive)
        {
            powerTimer += Time.deltaTime;
            if (powerTimer >= powerDuration)
            {
                isPowerActive = false;
                Debug.Log("Power Pellet etkisi sona erdi.");
            }
        }
    }

    void FixedUpdate()
    {
        // Move Pac-Man at constant speed in the current direction
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with a coin
        if (other.gameObject.CompareTag("Dots"))
        {
            // Increase the score by 1
            score+=100;
            count = count + 1;
            if (count >= 156)
            {
                Destroy(Enemy);
                uiManagerMenu.StartGame();
            }
            // Play the score sound
            SoundManager.instance.PlaySFX(SoundManager.instance.dotSound);
            Debug.Log("Score: " + score);
            // Destroy the dot
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Pellets"))
        {
            score += 200;
            isPowerActive = true;
            powerTimer = 0f;
            Debug.Log("Power Pellet etkisi baþladý!");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            if (isPowerActive)
            {
                GhostBehavior ghost = other.GetComponent<GhostBehavior>();
                if (ghost != null)
                {
                    ghost.KillGhost(); // Hayaleti öldür
                    score += 500; // Hayalet öldürme puaný
                    Debug.Log("Ghost killed! Score: " + score);
                }
            }
            else
            {
                LoseLife(); // Can kaybet
            }
        }
        if (isTeleporting) return;
        Vector3 offset = new Vector3(0.5f, 0, 0);
        if (other.gameObject.CompareTag("Teleport"))
        {
            if (other.gameObject == TeleportPoint1)
                transform.position = TeleportPoint2.transform.position + offset;
            else if (other.gameObject == TeleportPoint2)
                transform.position = TeleportPoint1.transform.position + offset;
            Invoke(nameof(ResetTeleporting), 0.1f);
        }
    }
    public bool IsPowerActive()
    {
        return isPowerActive;
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip); // Ses efektini çal
        }
    }
    private void ResetTeleporting()
    {
        isTeleporting = false;
    }
    private void LoseLife()
    {
        lives--; // Bir can eksilt
        SoundManager.instance.PlaySFX(SoundManager.instance.playerDeathSound); // Can kaybetme sesi
        Debug.Log("Lives remaining: " + lives);

        if (lives <= 0)
        {
            Destroy(gameObject);
            Destroy(Enemy);
            LoseScreen.SetActive(true);
            
        }
        else
        {
            // Pac-Man ve hayaletleri baþlangýç pozisyonuna döndür
            ResetPositions();
        }
    }

    private void ResetPositions()
    {
        // Pac-Man baþlangýç pozisyonuna dön
        transform.position = startPosition;

        // Hayaletleri resetle
        GhostBehavior[] ghosts = FindObjectsOfType<GhostBehavior>();
        foreach (GhostBehavior ghost in ghosts)
        {
            ghost.ResetPosition();
        }
    }

}
