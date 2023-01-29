using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestDetails : MonoBehaviour
{
    //unique id
    public int idNo;
    //These are the text objects on the gameobject
    public Text questNameText;
    public Text questDescriptionText;
    public Text questValueText;

    //These are gotten from the create new quest form
    public int questType;
    public string questName;
    public string questDescription;
    public int expValue;
    public int maxAmount;

    //If the quest is completed
    public bool completed = false;

    //This is counting how many out of maxAmount has been done, starting at 0
    private int m_currentAmount;

    //The canvas group on this object
    private CanvasGroup m_canvasGroup;

    //this is where we send the exp once we have completed the quest
    private GameObject m_gameController;
    private LevelSystem m_levelSystem;

    //Giving the prefab the quest name + desc based on original form
    void Start()
    {
        //grabbing levelsystem script
        m_gameController = GameObject.FindGameObjectWithTag("GameController");
        m_levelSystem = m_gameController.GetComponent<LevelSystem>();

        m_currentAmount = 0;
        m_canvasGroup = GetComponent<CanvasGroup>();

        questNameText.text = questName;

        if(maxAmount != 0)
        {
            questDescriptionText.text = string.Format("{0} ({1}/{2})", questDescription, m_currentAmount, maxAmount);
        }
        else
        {
            questDescriptionText.text = string.Format("{0}", questDescription);
        }

        questValueText.text = string.Format("{0} exp", expValue);
        
    }
    //if specified amount quest, use this to add one to the total
    public void AddOne()
    {
        m_currentAmount += 1;
        if(m_currentAmount < maxAmount)
        {
            questDescriptionText.text = string.Format("{0} ({1}/{2})", questDescription, m_currentAmount, maxAmount);
        }
        else
        {
            CompletedQuest();
        }
    }
    //when quest is completed, make semitransparent and not interactable.
    public void CompletedQuest()
    {
        questDescriptionText.text = string.Format("{0} (Completed!)", questDescription);
        m_canvasGroup.interactable = false;
        m_canvasGroup.alpha = 0.75f;
        completed = true;
        //send exp
        m_levelSystem.AddExperience(expValue);
    }
}
