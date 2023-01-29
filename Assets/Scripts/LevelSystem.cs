using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelSystem : MonoBehaviour
{
    //my current level (levels work like: 
    // level = n; ( ( (n^2 + n) / 2 )* 100) + ((n + 1) * 100)) so level 0 needs 100 exp, level 11 needs 
    private int m_currentLevel = 0;
    //my current experience
    private int m_currentExp;
    //amount of exp for next level
    private float m_nextLevelExp;
    //The text that shows the information
    public Text levelText;

    //saves level and experience
    private List<int> levelExp = new List<int>();

    void Start()
    {
        //load list
        LoadLevelDetails();
        //m_nextLevelExp = /*Mathf.FloorToInt*/(((Mathf.Pow(m_currentLevel, 2) + m_currentLevel) * 0.5f * 100) + (m_currentLevel + 1) * 100);
        int n = m_currentLevel;
        m_nextLevelExp = (((Mathf.Pow(n, 2) + n) * 0.5f) * 100) + ((n + 1) * 100);
        print(m_nextLevelExp);


        levelText.text = string.Format("Level {0} Exp ({1}/{2})", m_currentLevel, m_currentExp, m_nextLevelExp);

    }

    //saving data
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveLevelDetails();
        }
    }

    //will add the quests specific exp to current exp
    public void AddExperience(int expAdded)
    {
        m_currentExp += expAdded;
        if (m_currentExp >= m_nextLevelExp)
        {
            LevelUp();
        }
        levelText.text = string.Format("Level {0} Exp ({1}/{2})", m_currentLevel, m_currentExp, m_nextLevelExp);
    }

    void LevelUp()
    {
 
            m_currentLevel += 1;
            int n = m_currentLevel;
            m_nextLevelExp = (((Mathf.Pow(n, 2) + n) * 0.5f) * 100) + ((n + 1) * 100);
            levelText.text = string.Format("Level {0} Exp ({1}/{2})", m_currentLevel, m_currentExp, m_nextLevelExp);

        if (m_currentExp >= m_nextLevelExp)
        {
            LevelUp();
        }
        //do cool stuff now
    }
    
    void LoadLevelDetails()
    {
        if (File.Exists(Application.persistentDataPath + "/LevelData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/LevelData.dat", FileMode.Open);
            List<int> levelData = (List<int>)bf.Deserialize(file);
            file.Close();

            levelExp = levelData;
            m_currentLevel = levelExp[0];
            m_currentExp = levelExp[1];
            int n = m_currentLevel;
            m_nextLevelExp = (((Mathf.Pow(n, 2) + n) * 0.5f) * 100) + ((n + 1) * 100);
            levelText.text = string.Format("Level {0} Exp ({1}/{2})", m_currentLevel, m_currentExp, m_nextLevelExp);
        }
    }

    public void SaveLevelDetails()
    {
        levelExp.Clear();
        levelExp.Add(m_currentLevel);
        levelExp.Add(m_currentExp);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/LevelData.dat");
        List<int> levelData = levelExp;
        //data.userScores = userScores;
        bf.Serialize(file, levelData);
        file.Close();
    }

    public void ResetLevel()
    {
        m_currentExp = 0;
        m_currentLevel = 0;
        int n = m_currentLevel;
        m_nextLevelExp = (((Mathf.Pow(n, 2) + n) * 0.5f) * 100) + ((n + 1) * 100);
        levelText.text = string.Format("Level {0} Exp ({1}/{2})", m_currentLevel, m_currentExp, m_nextLevelExp);
    }
}
