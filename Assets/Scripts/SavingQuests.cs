using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class SavingQuests : MonoBehaviour {

    private List<string[]> m_rowData = new List<string[]>();
    private Dictionary<string, int> m_ladderPieceDictionary = new Dictionary<string, int>();
    public GameObject[] ladderPieces;
    private string[] rowDataTemp = new string[5];
    public int m_idNo;

    //when reading data, this checks which specific data is being read
    private int m_currentAttNo;
    private string m_qName;
    private string m_qDesc;
    private int m_qExp;
    private int m_qType;
    private int m_maxAmount;
    private int m_currentAmount;
    private int m_qID;


    void Start()
    {
        //m_ladderPieceDictionary.Add("Triangle", 0);
        //m_ladderPieceDictionary.Add("Square", 1);
        //m_ladderPieceDictionary.Add("Circle", 2);
        SaveHeader();
    }

    void SaveHeader()
    {
        rowDataTemp[0] = "ID";
        rowDataTemp[1] = "Type";
        rowDataTemp[2] = "Name";
        rowDataTemp[3] = "Description";
        rowDataTemp[4] = "EXP";
        m_rowData.Add(rowDataTemp);
    }

    public void SavePath()
    {

        string[][] output = new string[m_rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = m_rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }

        string filePath = GetPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public void SaveLadderInfo(GameObject currentQuest)
    {
        QuestDetails currentDetails = currentQuest.GetComponent<QuestDetails>();
        rowDataTemp = new string[5];
        rowDataTemp[0] = "" + m_idNo; // ID
        rowDataTemp[1] = ""; //type
        rowDataTemp[2] = "" + currentDetails.questName; // name
        rowDataTemp[3] = "" + currentDetails.questDescription; // description
        rowDataTemp[4] = "" + currentDetails.expValue; // exp value

        m_rowData.Add(rowDataTemp);
        print("added data");

        m_idNo += 1;
    }
    private string GetPath()
    {
#if UNITY_EDITOR
        return Application.persistentDataPath + /*"/CSV/" + */"/Saved_quest_data.csv";
#elif UNITY_ANDROID
                return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
                return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
                return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }

    void LoadThisData()
    {
        //string dataPath = Application.persistentDataPath + "/ladderPath.txt";
        string dataPath = Application.persistentDataPath + /*"/CSV/" + */"/Saved_quest_data.csv";
        StreamReader streamReader = new StreamReader(dataPath);
        string headerLine = streamReader.ReadLine();
        while (!streamReader.EndOfStream)
        {
            string currentLine = streamReader.ReadLine();
            char[] chars = currentLine.ToCharArray();
            m_currentAttNo = 0;
            for (int i = 0; i < currentLine.Length; i++)
            {
                if(m_currentAttNo == 0)
                {
                    char[] idChars = new char[3]; 
                    idChars[i] = chars[i];
                }
                //End of line
                if (i == currentLine.Length - 1)
                {
                    MakeThing();
                    //m_currentAttNo = 0;
                    //m_negativePositive = 1;
                    //m_decimalPoint = false;
                    //m_powNo = 1;
                    for (int j = 0; j < 6; j++)
                    {
                        //m_attNo[j] = 0;
                    }
                }
            }
        }
    }

    void MakeThing()
    {
        //GameObject currentOneOffQuest = Instantiate(quest[m_questType], transform.position, Quaternion.identity) as GameObject;
        //currentOneOffQuest.transform.parent = questPanel.GetComponent<Transform>();
        //QuestDetails currentScript = currentOneOffQuest.GetComponent<QuestDetails>();
        //currentScript.questName = questName.text;
        //currentScript.questDescription = questDesc.text;
        //currentScript.expValue = int.Parse(questEXP.text);
        //currentScript.maxAmount = int.Parse(questAmount.text);
        //currentScript.idNo = m_idNo;
    }
}
