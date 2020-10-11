using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    const int _BOARD_TO_DO = 10;

    //attributs
    public static GameManager m_instance = null; //instance => accessible par les autres script
    private BoardManager m_boardScript;
    private int m_level = 3;
    private List<EnnemiAI> m_ennemies;
    public float m_levelStartDelay = 2f;
    private GameObject m_levelImage;
    private bool m_isSetUp = true;
    

    private void Awake()
    {
        if (m_instance == null)
        { 
            m_instance = this;
        }
        DontDestroyOnLoad(gameObject);
        m_ennemies = new List<EnnemiAI>();
        m_boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    private void OnLevelWasLoaded(int level)
    {
        m_level++;
        InitGame();
    }
    void InitGame()
    {
        Debug.Log("InitGame");
        m_isSetUp = true;
        m_levelImage = GameObject.Find("levelImage");
        m_levelImage.SetActive(true);
        Invoke("HideLevelImage", m_levelStartDelay);
        m_ennemies.Clear();
        m_boardScript.setupScene(m_level);
    }

    private void HideLevelImage()
    {
        m_levelImage.SetActive(false);
        m_isSetUp = false;
    }


    public void AddEnnemis(EnnemiAI e)
    {
        m_ennemies.Add(e);
    }

    private void Update()
    {
        if (m_isSetUp) return;
        if(m_ennemies.Count == 0 )
        {
            //ToDo : ouverture des portes
            m_boardScript.OpenDoor();

        }
        else
        {
            foreach (EnnemiAI e in m_ennemies)
            {
                //e.ActionToDo();
            }
        }
        
        //GameOver
    }

    public void GameOver()
    {
        m_levelImage.SetActive(true);
    }
}

