using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateNewQuest : MonoBehaviour
{
    //These are the quest prefabs. 0 = one off, 1 = specific amount
    public GameObject[] quest;

    //these grab data from form
    public InputField questName;
    public InputField questDesc;
    public InputField questEXP;
    public InputField questAmount;

    //this is the panel that they are added to in a grid format
    public GameObject questPanel;

    //if it's one off 0, specific amount 1 or multiple 2
    private int m_questType;

    //I don't know why this is here to be honest
    private string m_questDescription;

    //the number given out to the quests
    private int m_idNo;

    void Start()
    {
        //m_idNo = gameObject.GetComponent<SavingQuests>().idNo;
    }

    //changes the quest type when clicking on the toggle buttons (on form)
	public void QuestType(int typeNo)
    {
        m_questType = typeNo;
    }

    //Instantiates a template quest and fills in the information
    public void CreateQuest()
    {
        GameObject currentOneOffQuest = Instantiate(quest[m_questType], transform.position, Quaternion.identity) as GameObject;
        currentOneOffQuest.transform.parent = questPanel.GetComponent<Transform>();
        QuestDetails currentScript = currentOneOffQuest.GetComponent<QuestDetails>();
        currentScript.questName = questName.text;
        currentScript.questDescription = questDesc.text;
        currentScript.expValue = int.Parse(questEXP.text);
        currentScript.maxAmount = int.Parse(questAmount.text);
        currentScript.idNo = m_idNo;
        m_idNo += 1;
    }
}
