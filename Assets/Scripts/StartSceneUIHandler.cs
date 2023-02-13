using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
        #if UNITY_EDITOR
using UnityEditor;
  #endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TextMeshProUGUI _highScoreText;

    private void Start()
    {
        SetHighScore();
    }

    public void NewGame()
    {
        GameSession.Instance.SetCurrentPlayer(_nameInput.text);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
  #endif
        Application.Quit();
    }

    private void SetHighScore()
    {
        var highScore = GameSession.Instance.HighScore;
        _highScoreText.text =
            $"Best Score : {highScore.Score} : {highScore.Name}";
    }
    
    
}
