using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class Answer : MonoBehaviour
{
    public int index;
    Controller m_ctrl;
    // Start is called before the first frame update
    void Start()
    {
        m_ctrl = FindObjectOfType<Controller>();
    }

   
    public void Click()
    {
        m_ctrl.checkClickAns(index);

    }
   

}
