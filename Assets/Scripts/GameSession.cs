using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private ScoreData _highScoreData;
    private readonly ScoreData _currentScoreData = new ScoreData();
    private static GameSession _instance;
    public static GameSession Instance => _instance;
    public ScoreData HighScore => _highScoreData;
    public ScoreData CurrentScoreData => _currentScoreData;

    public void AddScorePoints(int points)
    {
        _currentScoreData.Score += points;
    } 
    public int GetScorePoints()
    {
        return _currentScoreData.Score;
    }

    public void SetCurrentPlayer(string name)
    {
        _currentScoreData.Name = name;
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        LoadSession();
    }

    public void LoadSession()
    {
        _highScoreData = ScoreData.LoadHighScoreData();
        if (_highScoreData == null)
        {
            _highScoreData = new ScoreData();
        }
        
    }

    public void SaveSession()
    {
        if (_currentScoreData.Score > _highScoreData.Score)
        {
            ScoreData.SaveHighScore(_currentScoreData);
        }
    }
    
    
}

[Serializable]
public class ScoreData
{
    [SerializeField] private string _name = "Nobody";
    [SerializeField] private int _score = 0;
    
    public string Name
    {
        get => _name;
        set => _name = value;
    }
    public int Score
    {
        get => _score;
        set => _score = value;
    }

    public static void SaveHighScore(ScoreData dataToSave)
    {
        string json = JsonUtility.ToJson(dataToSave);
        File.WriteAllText(Application.persistentDataPath + "/HighScore.json",json);
    }

    public static ScoreData LoadHighScoreData()
    {
        ScoreData scoreData = null;
        string path = Application.persistentDataPath + "/HighScore.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            scoreData = JsonUtility.FromJson<ScoreData>(json);
        }
        return scoreData;
    }

}
