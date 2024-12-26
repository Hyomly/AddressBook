using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct UserInfo
{
    public enum eJob
    {
        학생 = 0,
        공무원,
        회사원,
        자영업,
        기타,
        None
    }
    public string m_name;
    public bool m_marry;
    public string m_address;
    public int m_age;
    public eJob m_job;
    

    public UserInfo(string name, bool marry, string address, int age, int job)
    {
        m_name = name;
        m_marry = marry;
        m_address = address;
        m_age = age;
        m_job = (eJob)job;
    }
}

public class UserData : MonoBehaviour
{
    static UserData m_instance;

    public static UserData Instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<UserData>();

            return m_instance;

        }

    }
    
    [SerializeField]
    Toggle m_marryToggle;    
    [SerializeField]
    Slider m_ageSlider;
    [SerializeField]
    Text m_ageText;
    [SerializeField]
    GameObject m_dataPrefab;
    [SerializeField]
    Transform m_transParent;
    GameObject m_selectObj;

    string m_name;
    
    string m_address;
    string m_findName;
    int m_job;
    int m_age = 20;
    int m_findIdx = -1;
    bool m_isMarried;
    bool m_nameCheck = false;

    List<UserInfo> m_userList  = new List<UserInfo>();
    List<GameObject> m_userObj = new List<GameObject>();
    public List<UserInfo> UserList => m_userList;
    public bool NameCheck => m_nameCheck;

    public void IsNameCheck(bool value)
    {
        m_nameCheck = value;
    }
    public void UserName(string name)
    {
        m_name = name;
        IsNameCheck(true);
    }

    public void UserMarry(bool marry)
    {
       m_isMarried = marry;         
    }

    public void UserAddress(string address, string address2)
    {
        m_address = address + " " + address2;
    }
    public void UserAge(float age)
    {
        m_ageText.text = age.ToString();
        m_age = (int)age;
    }
    public void UserJob(int job)
    {
        m_job = job;
    }
    

    //회원 추가하기
    public void AddUser()
    {
        m_userList.Add(new UserInfo(m_name, m_isMarried, m_address, m_age, m_job));        
        DataObj();
        m_findIdx = -1;
    }

    //찾은 이름의 정보 주기
    public void SetUserData(string findName)
    {
        if(findName != null)
        {
            var idx = m_userList.FindIndex(name => name.m_name.Equals(findName));
            if(idx != -1)
            {
                m_findIdx = idx;
                var user = m_userList[idx];
                UIManager.Instance.SelectUserData(user.m_name, user.m_marry, user.m_address, user.m_age, (int)user.m_job);
            }
        }
    }

    

    //변경할 내용을 기존내용에 덮어쓰기(수정하기)
    public void EditUser()
    {
        if(m_findIdx != -1)
        {
            var editUser = m_userList[m_findIdx];
            editUser.m_name = m_name;
            editUser.m_marry = m_isMarried;
            editUser.m_address = m_address;
            editUser.m_age = m_age;
            editUser.m_job = (UserInfo.eJob)m_job;
            m_userList[m_findIdx] = editUser;
            m_userObj[m_findIdx].GetComponent<ShowData>().SetUserData(m_findIdx);
            m_findIdx = -1;
        }
        
    }



    //선택한 오브젝트 알아오기
    public void GetObj(GameObject obj, string findName)   
    {
        m_selectObj = obj;
        m_findName = findName;
    }

    //선택한 오브젝트, 등록된 내용 지우기
    public void DeleteUser()
    {
        if(m_findName != null)
        {
            var idx = m_userList.FindIndex(name => name.m_name.Equals(m_findName));
            m_userList.RemoveAt(idx);
            m_userObj.RemoveAt(idx);
            Destroy(m_selectObj);
        }
       
    }

    //모든 오브젝트, 내용 지우기
    public void DeleteAllUser() 
    {
        m_userList.Clear();
        m_userObj.Clear();
        foreach (Transform child in m_transParent)
        {
            Destroy(child.gameObject);
        }
    }

    //보여줄 datapanel 생성하기 
    void DataObj()
    {
        var obj = Instantiate(m_dataPrefab);
        obj.transform.SetParent(m_transParent);
        obj.GetComponent<ShowData>().SetUserData(m_userList.Count-1);  
        m_userObj.Add(obj);
    }

   

    public void SaveData()
    {
        Debug.Log("저장");
        JsonWrapper.SaveList(m_userList, "/UserDataText.json");
        
    }
    public void LoadData()
    {
        Debug.Log("불러오기");
        List<UserInfo> datas = JsonWrapper.LoadList<UserInfo>("/UserDataText.json");
       
        for(int i = 0; i < datas.Count; i++)
        {
            Debug.Log(datas[i].m_name);
            m_userList.Add(new UserInfo(datas[i].m_name, datas[i].m_marry, datas[i].m_address, datas[i].m_age, (int)datas[i].m_job));
            var obj = Instantiate(m_dataPrefab);
            obj.transform.SetParent(m_transParent);
            obj.GetComponent<ShowData>();
            m_userObj.Add(obj);
            m_userObj[i].GetComponent<ShowData>().SetUserData(i);
        }
        
        
    }

    private void Start()
    {
        
        UserMarry(false);
        m_marryToggle.onValueChanged.AddListener(UserMarry);
        m_ageSlider.onValueChanged.AddListener(UserAge);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("회원수 :" + m_userList.Count);
            for (int i = 0; i < m_userList.Count; i++)
            {
                Debug.Log("회원이름: " + m_userList[i].m_name);
            }
            Debug.Log("오브젝트 수 :" + m_userObj.Count);
        }
      
    }
}
