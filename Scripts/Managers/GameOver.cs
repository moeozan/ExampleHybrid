using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button backButton;
    private void Start()
    {
        backButton.onClick.AddListener(GameEnd);
    }

    private void GameEnd()
    {
        DatabaseManager.instance.AddGold(PropertyManager.instance.TotalGoldAmount);
        SceneManager.LoadScene("Menu");
    }
}
