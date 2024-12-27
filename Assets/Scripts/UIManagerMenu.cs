using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
public class UIManagerMenu : MonoBehaviour
{
    // Start butonuna basıldığında sonraki sahneyi yükler
    public void StartGame()
    {
        // Bir sonraki sahneyi yükle
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogError("No next scene found! Check your Build Settings.");
        }
    }

    // Settings butonuna basıldığında "Settings" sahnesini yükler
    public void OpenSettings()
    {
        // Settings sahnesini yükle
        if (Application.CanStreamedLevelBeLoaded("Settings"))
        {
            SceneManager.LoadScene("Settings");
        }
        else
        {
            Debug.LogError("Settings scene not found in Build Settings!");
        }
    }

    // Exit butonuna basıldığında oyunu kapatır
    public void QuitGame()
    {
        //EditorApplication.isPlaying = false; eğerki unity editöründen çıkmak istersen...
        Application.Quit();
    }

    public void BackToMainMenu()
{
    SceneManager.LoadScene("Menu"); // Ana menü sahnesini yükle
}
    public void RestartGame()
    {
        SceneManager.LoadScene("Level-1"); // Aktif sahneyi yeniden yükle
    }   

}

