using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobSelect : MonoBehaviour
{
    

    [SerializeField]
    Toggle m_student;
    [SerializeField]
    Toggle m_publicOfficial;
    [SerializeField]
    Toggle m_employee;
    [SerializeField]
    Toggle m_selfEmployed;
    [SerializeField]
    Toggle m_etc;
    
    int m_job;
    
   
    public void Student(bool value)
    {
        if(value)
            m_job = (int)UserInfo.eJob.학생;
        UserData.Instance.UserJob(m_job);
    }

    public void PublicOfficial(bool value)
    {
        if (value)
            m_job = (int)UserInfo.eJob.공무원;
        UserData.Instance.UserJob(m_job);
    }

    public void Employee(bool value)
    {
        if (value)
            m_job = (int)UserInfo.eJob.회사원;
        UserData.Instance.UserJob(m_job);
    }
    public void SelfEmployed(bool value)
    {
        if (value)
            m_job = (int)UserInfo.eJob.자영업;
        UserData.Instance.UserJob(m_job);
    }
    public void ETC(bool value)
    {
        if (value)
        {
            m_job = (int)UserInfo.eJob.기타;
        }
        UserData.Instance.UserJob(m_job);
    }
   
    // Start is called before the first frame update
    void Start()
    {
        Student(true);
        m_student.onValueChanged.AddListener(Student);
        m_publicOfficial.onValueChanged.AddListener(PublicOfficial);
        m_employee.onValueChanged.AddListener(Employee);
        m_selfEmployed.onValueChanged.AddListener(SelfEmployed);
        m_etc.onValueChanged.AddListener(ETC);
    }
    

}
