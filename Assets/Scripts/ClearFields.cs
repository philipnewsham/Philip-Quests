using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ClearFields : MonoBehaviour {

    private InputField[] m_inputFields;

	void Start ()
    {
        m_inputFields = GetComponentsInChildren<InputField>();
	}
    //clears all the fields when done
    public void Clearing()
    {
        for (int i = 0; i < m_inputFields.Length; i++)
        {
            m_inputFields[i].text = "";
        }
    }

    public void ClearAmount()
    {
        m_inputFields[2].text = "0";
    }

}
