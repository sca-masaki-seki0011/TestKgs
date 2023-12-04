using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public JsonReader jsonReader;
    public NumberImageChange numberImageChange; // NumberImageChangeの参照を追加

    private int[] scoreToShow;
    private string[] nameToShow;

    private void Start()
    {
        scoreToShow = new int[5];//初期化
        nameToShow = new string[5];//初期化

        // JsonReaderを使ってランキングデータを読み込む
        PlayerScoresData playerScoresData = ReadJsonFile();
        if (playerScoresData != null)
        {
            // ランキングの更新や必要な処理を行う
            UpdateRanking(playerScoresData);
            DisplayScoreImages(playerScoresData); // スコアデータを表示する
        }
        else
        {
            Debug.LogWarning("Failed to load ranking data!");
        }
    }

    private PlayerScoresData ReadJsonFile()
    {
        string filePath = "Assets/Resources/Ranking.json"; // パスを指定

        if (System.IO.File.Exists(filePath))
        {
            string jsonContent = System.IO.File.ReadAllText(filePath);
            // デバッグログに読み込んだデータを表示
            //
            //Debug.Log("Loaded JSON data: " + jsonContent);

            PlayerScoresData playerScoresData = JsonUtility.FromJson<PlayerScoresData>(jsonContent);

            if (playerScoresData != null)
            {
                // デバッグログにplayerScoresDataの内容を表示
                Debug.Log("Loaded player score data: "+jsonContent);
                for (int i = 0; i < playerScoresData.playerScores.Length; i++)
                {
                    Debug.Log("Player Name: " + playerScoresData.playerScores[i].PlayerName + ", Score: " + playerScoresData.playerScores[i].score);

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

    private void UpdateRanking(PlayerScoresData playerScoresData)
    {
        // ここでランキングの更新処理を実装する
        // playerScoresData.playerScores を適切に処理してランキングを更新する
    }

    private void DisplayScoreImages(PlayerScoresData playerScoresData)
    {
        if (numberImageChange != null)
        {
            if (playerScoresData != null && playerScoresData.playerScores.Length > 0)
            {
                
                // 上位５人のスコアを取得し、NumberImageChangeに渡す
                for (int i = 0; i < 5; i++)
                {
                    scoreToShow[i] = playerScoresData.playerScores[i].score;
                    nameToShow[i] = playerScoresData.playerScores[i].PlayerName;
                    // ここでnameToShow[i]を使ってプレイヤー名を表示したり利用したりすることができます
                    //Debug.Log($"Rank {i + 1}: Player Name: {nameToShow[i]}, Score: {scoreToShow[i]}");
                    Debug.Log(scoreToShow[i]);
                    numberImageChange.ShowScoreImages(scoreToShow[i],i);
                    numberImageChange.ShowName(nameToShow[i]);
                }

                
            }
            else
            {
                Debug.LogWarning("PlayerScoresData is null or empty!");
            }
        }
        else
        {
            Debug.LogWarning("NumberImageChange reference is missing!");
        }
    }
}
