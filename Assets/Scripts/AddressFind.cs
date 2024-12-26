using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddressFind : MonoBehaviour
{
    [SerializeField]
    GameObject m_findAddress;
    [SerializeField]
    Dropdown m_address1;
    [SerializeField]
    Dropdown m_address2;
    [SerializeField]
    Text m_userAddressText;

    string m_userAddress1;
    string m_userAddress2;


    public void OnFindAddress()
    {
        m_findAddress.SetActive(true);
        SetAddress2();  
    }
    public void OnOk()
    {
        m_findAddress.SetActive(false);
        m_userAddress1 = m_address1.options[m_address1.value].text;
        m_userAddress2 = m_address2.options[m_address2.value].text;

        Debug.Log(m_userAddress1 + m_userAddress2);

        ShowAddress();

        UserData.Instance.UserAddress(m_userAddress1, m_userAddress2);
    }
    public void ShowAddress()
    {
        m_userAddressText.text = m_userAddress1 + " " + m_userAddress2;
    }
    public void SetAddress2()
    {
        if (m_address1.value == 0)
        {
            m_address2.options[0].text = "종로구";
            m_address2.options[1].text = "성동구";
            m_address2.options[2].text = "서초구";
        }
        else if (m_address1.value == 1)
        {
            m_address2.options[0].text = "수원시";
            m_address2.options[1].text = "용인시";
            m_address2.options[2].text = "화성시";
        }
        else if (m_address1.value == 2)
        {
            m_address2.options[0].text = "중구";
            m_address2.options[1].text = "동구";
            m_address2.options[2].text = "동구";

        }
    }
    public void OnAddress1()
    {
        Debug.Log(m_address1.value);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        m_findAddress.SetActive(false);
        m_address1.value = 0;
       
    }
    
}
