using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // Singleton pattern

    public AudioSource musicSource; // Arka plan müzikleri veya baþlangýç müziði için
    public AudioSource sfxSource; // Ses efektleri için

    public AudioClip startMusic; // Baþlangýç müziði
    public AudioClip ghostKillSound; // Hayalet öldürme sesi
    public AudioClip playerDeathSound; // Oyuncu ölme sesi
    public AudioClip dotSound; // Dot sesi

    private void Awake()
    {
        // Singleton instance oluþtur
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahne geçiþlerinde kaybolmasýn
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusicOnce(AudioClip clip)
    {
        if (musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = false; // Loop'u kapat
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
