using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameManager manager;

    void Awake()
    {
        if(GameManager.m_instance == null)
        {
            Instantiate(manager);
        }
    }
}
