using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // Singleton pattern

    public AudioSource musicSource; // Arka plan m�zikleri veya ba�lang�� m�zi�i i�in
    public AudioSource sfxSource; // Ses efektleri i�in

    public AudioClip startMusic; // Ba�lang�� m�zi�i
    public AudioClip ghostKillSound; // Hayalet �ld�rme sesi
    public AudioClip playerDeathSound; // Oyuncu �lme sesi
    public AudioClip dotSound; // Dot sesi

    private void Awake()
    {
        // Singleton instance olu�tur
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahne ge�i�lerinde kaybolmas�n
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
