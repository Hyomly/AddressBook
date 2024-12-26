using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager m_instance;

    public static UIManager Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<UIManager>();

            return m_instance;

        }

    }

    [SerializeField]
    InputField m_nameField;
    [SerializeField]
    Toggle m_marryToggle;
    [SerializeField]
    Toggle m_singleToggle;
    [SerializeField]
    Dropdown m_address1;
    [SerializeField]
    Text m_userAddressText;
    [SerializeField]
    Slider m_ageSlider;
    [SerializeField]
    Toggle[] m_jobs;
   

    bool m_IsAdd = false;
    public bool IsAdd => m_IsAdd;

    public void OnAdd()
    {
        m_IsAdd = true;
        PopupManager.Instance.UserEditorPop();
    }
   
    public void OnEdit()
    {
        m_IsAdd = false;
        PopupManager.Instance.FindPop();       
    }
    
    public void OnDelete()
    {
        PopupManager.Instance.DeletePop();
    }
    
    public void OnDeleteAll()
    {
        PopupManager.Instance.DeleteAllPop();
    }
    public void OnSave()
    {
        UserData.Instance.SaveData();
    }
    public void OnLoad()
    {
        UserData.Instance.LoadData();
    }
    //수정할 User data불러오기
    public void SelectUserData(string name, bool marry, string address, int age , int job)
    {
        m_nameField.text = name;
        m_marryToggle.isOn = marry ;
        m_userAddressText.text = address;
        m_ageSlider.value = age;
        m_jobs[job].isOn = true;
    }

    //초기화
    public void InitData()
    {
        m_nameField.text = null;
        m_singleToggle.isOn = true;
        m_address1.value = 0;
        m_userAddressText.text = "";
        m_ageSlider.value = 20;
        m_jobs[0].isOn = true;
        UserData.Instance.IsNameCheck(false);
    }

    public bool EssentialCheck()
    {
        if (m_nameField.text != "" && m_userAddressText.text != "")
        {
            return true;
        }
        return false;
    }



}

