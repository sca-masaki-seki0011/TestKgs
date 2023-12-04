using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerScore
{
    public string PlayerName;
    public int score;
}

[System.Serializable]
public class PlayerScoresData
{
    public PlayerScore[] playerScores;
}

public class JsonReader : MonoBehaviour
{

    public PlayerScoresData ReadJsonFile()
    {
        string filePath = "Assets/Resources/Ranking.json"; // �p�X���w��

        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            // �f�o�b�O���O�ɓǂݍ��񂾃f�[�^��\��
            Debug.Log("Loaded JSON data: " + jsonContent);

            PlayerScoresData playerScoresData = JsonUtility.FromJson<PlayerScoresData>(jsonContent);

            if (playerScoresData != null)
            {
                // �f�o�b�O���O�ɃX�R�A��\��
                foreach (var playerScore in playerScoresData.playerScores)
                {
                    
                    Debug.Log($"Player: {playerScore.PlayerName}, Score: {playerScore.score}");
                }

                return playerScoresData;
            }
            else
            {
                Debug.LogWarning("Failed to parse JSON data!");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("Json file does not exist at path: " + filePath);
            return null;
        }
    }
}
