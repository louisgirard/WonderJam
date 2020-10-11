using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    const int _BOARD_TO_DO = 10;

    //attributs
    public static GameManager m_instance = null; //instance => accessible par les autres script
    private BoardManager m_boardScript;
    private int m_level = 1;
    private List<EnemyHealth> m_enemies;
    public float m_levelStartDelay = 2f;
    private GameObject m_levelImage;
    private bool m_isSetUp = false;
    bool doorOpened = false;

    [SerializeField] GameObject playerPrefab;

    private void Start()
    {
        if (m_instance == null)
        { 
            m_instance = this;
        }
        DontDestroyOnLoad(gameObject);
        m_enemies = new List<EnemyHealth>();
        m_boardScript = GetComponent<BoardManager>();
        m_levelImage = GameObject.Find("LevelImage");
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
        doorOpened = false;
        m_levelImage.SetActive(true);
        Invoke("HideLevelImage", m_levelStartDelay);
        m_enemies.Clear();
        m_boardScript.SetupScene(m_level, playerPrefab);
        m_isSetUp = true;
    }

    public void LoadNextLevel()
    {
        m_isSetUp = false;
        m_level++;
        InitGame();
    }

    private void HideLevelImage()
    {
        m_levelImage.SetActive(false);
        m_isSetUp = true;
    }


    public void AddEnemy(EnemyHealth e)
    {
        m_enemies.Add(e);
    }

    public void RemoveEnemy(EnemyHealth e)
    {
        m_enemies.Remove(e);
    }

    private void Update()
    {
        if (!m_isSetUp) return;
        if (m_enemies.Count == 0 && !doorOpened)
        {
            m_boardScript.OpenDoor();
            doorOpened = true;
        }
        
        //GameOver
    }

    public void GameOver()
    {
        m_levelImage.SetActive(true);
    }
}

