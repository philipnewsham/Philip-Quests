using UnityEngine;
using System.Collections;

public class RefreshBoard : MonoBehaviour
{
    private GameObject[] currentQuests;

    //cleans up the board of completed quests
    public void CleanUp()
    {
        currentQuests = GameObject.FindGameObjectsWithTag("Quest");
        for (int i = 0; i < currentQuests.Length; i++)
        {
            if (currentQuests[i].GetComponent<QuestDetails>().completed)
            {
                Destroy(currentQuests[i]);
            }
        }
    }
    
}
