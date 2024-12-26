using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NameCheck : MonoBehaviour
{
    [SerializeField]
    GameObject m_nameCheckPanel;
    [SerializeField]
    InputField m_nameField;
    [SerializeField]
    Text m_usableName;

    string m_inputName;
   

    void CheckName()
    {
        m_inputName = m_nameField.text;

        
        var user = UserData.Instance.UserList;
        if(user.Count == 0 && m_inputName != "" )
        { 
            m_usableName.text = "사용가능한 이름입니다.";
            UserData.Instance.UserName(m_inputName);
            return;
        }
        else
        {
            m_usableName.text = "사용 불가능한 이름입니다.";
        }
        if(user != null)
        {
            for (int i = 0; i < user.Count; i++)
            {
                if (user[i].m_name == m_inputName)
                {
                    m_usableName.text = "중복된 이름입니다.";

                    break;
                }
                else if (m_inputName == "")
                {
                    m_usableName.text = "사용 불가능한 이름입니다.";
                }

                else
                {
                    m_usableName.text = "사용가능한 이름입니다.";
                    UserData.Instance.UserName(m_inputName);
                }
            }
        }
        
        
    }

    public void OnCheckButton()
    {
        CheckName();
        m_nameCheckPanel.SetActive(true);
    }
    public void OnOK_NameCheck()
    {
        m_nameCheckPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_inputName = m_nameField.GetComponent<InputField>().text;
    }
}