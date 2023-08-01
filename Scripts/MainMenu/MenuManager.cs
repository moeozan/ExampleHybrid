using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button startGame;
    [SerializeField] private GameObject gpsManager;
    [SerializeField] private GameObject fbLoginManager;
    private void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Login"))
        {
            PlatformController();
        }
        else 
        {
            startGame.onClick.AddListener(StartGame);
        }
    }

    private void OnDestroy()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Menu"))
            startGame.onClick.RemoveAllListeners();
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    private void PlatformController()
    {
#if UNITY_EDITOR
        gpsManager.gameObject.SetActive(false);
#elif PLATFORM_ANDROID
        fbLoginManager.gameObject.SetActive(false);
#endif
    }
}
