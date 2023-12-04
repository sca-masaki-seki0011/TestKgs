using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public JsonReader jsonReader;
    public NumberImageChange numberImageChange; // NumberImageChange�̎Q�Ƃ�ǉ�

    private int[] scoreToShow;
    private string[] nameToShow;

    private void Start()
    {
        scoreToShow = new int[5];//������
        nameToShow = new string[5];//������

        // JsonReader���g���ă����L���O�f�[�^��ǂݍ���
        PlayerScoresData playerScoresData = ReadJsonFile();
        if (playerScoresData != null)
        {
            // �����L���O�̍X�V��K�v�ȏ������s��
            UpdateRanking(playerScoresData);
            DisplayScoreImages(playerScoresData); // �X�R�A�f�[�^��\������
        }
        else
        {
            Debug.LogWarning("Failed to load ranking data!");
        }
    }

    private PlayerScoresData ReadJsonFile()
    {
        string filePath = "Assets/Resources/Ranking.json"; // �p�X���w��

        if (System.IO.File.Exists(filePath))
        {
            string jsonContent = System.IO.File.ReadAllText(filePath);
            // �f�o�b�O���O�ɓǂݍ��񂾃f�[�^��\��
            //
            //Debug.Log("Loaded JSON data: " + jsonContent);

            PlayerScoresData playerScoresData = JsonUtility.FromJson<PlayerScoresData>(jsonContent);

            if (playerScoresData != null)
            {
                // �f�o�b�O���O��playerScoresData�̓��e��\��
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
        // �����Ń����L���O�̍X�V��������������
        // playerScoresData.playerScores ��K�؂ɏ������ă����L���O���X�V����
    }

    private void DisplayScoreImages(PlayerScoresData playerScoresData)
    {
        if (numberImageChange != null)
        {
            if (playerScoresData != null && playerScoresData.playerScores.Length > 0)
            {
                
                // ��ʂT�l�̃X�R�A���擾���ANumberImageChange�ɓn��
                for (int i = 0; i < 5; i++)
                {
                    scoreToShow[i] = playerScoresData.playerScores[i].score;
                    nameToShow[i] = playerScoresData.playerScores[i].PlayerName;
                    // ������nameToShow[i]���g���ăv���C���[����\�������藘�p�����肷�邱�Ƃ��ł��܂�
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
