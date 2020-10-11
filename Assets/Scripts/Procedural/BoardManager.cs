using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


/*
 * TODO : ajout d'une deuxieme porte
 * Ajout des objets 
 * Ajout d'un spawner
 * Revoir le systeme de GetRandom => plus beau ??
 * Voir les autres trucs procedural
 */

//Objectif du script => generer aleatoirement les sprites du niveau 
/*
 * Regles :
 * 2 Sortie est disposé en haut
 * Entrée en bas
 * Obstacle = banni sa zone
 */
public class BoardManager : MonoBehaviour
{
    //Class count => va permettre de créer des nombres aléatioires bien limités
    [Serializable]
    public class Count
    {
        public int min;
        public int max;

        public Count(int p_min, int p_max)
        {
            min = p_min;
            max = p_max;
        }
    }

    public int m_numberColumns = 8;
    public int m_numberRows = 8;

    public class Sprite
    {
        public GameObject spriteToDisplay;
        public int zoneToBan;
        public Sprite(GameObject p_sprite, int p_zone)
        {
            spriteToDisplay = p_sprite;
            zoneToBan = p_zone;
        }
    }

    //Sprites :
    //TODO : random nuber obstacle et objets
    public GameObject[] m_floorsSprite;
    public GameObject[] m_wallSprite; // 0 => top, 1=> Rigth, 2=> Left, 3 => Down
    public GameObject[] m_wallCornerSprite;//0=>TopL, 1=>TopR, 2=>DownL, 3=>DownR
    public GameObject[] m_obstaclesSprites;
    //public GameObject[] m_ennemiSprites;
    //public Sprite[] m_obstSprite;
    public GameObject m_entrySprite;
    public GameObject m_exitSprite_base;
    public GameObject m_exitSprite_open;
    //public GameObject m_exitSecondSprite;
    public GameObject[] m_objects;
    private Vector2 m_exit_position;
    public GameObject[] m_enemies;
    private GameObject m_player;
    private GameObject m_exit_door;

    GameObject environment;

    private void Start()
    {
        environment = GameObject.Find("Environment");
    }

    private void Update()
    {
        Vector2 exit_position = new Vector2(m_exit_position.x, m_exit_position.y);
        if(Vector2.Distance(exit_position, m_player.transform.position) <= 1f)
        {
            GameManager.m_instance.LoadNextLevel();
        }
    }

    //TODO : Ajout des portes + fenetre

    //Positions des cases pouvant etre utilisées pour placer un element => impossible de mettre deux elemeents sur la meme case
    private static List<Vector2> m_gridPositions = new List<Vector2>();
    //private BasicDonjon m_basicDonjonScript;
    
    //Initialisation de la liste des obstacles

    public static List<Vector2> GetGridPos()
    {
        return m_gridPositions;
    }

    //Nettoie la liste
    void InitialiseList()
    {
        m_gridPositions.Clear();
        for(int c= 1 ; c < m_numberColumns -1 ; c++ )
        {
            for (int r= 1 ; r < m_numberRows -1; r++)
            {
                m_gridPositions.Add(new Vector2(c,r));
            }
        }
    }

    //TODO : fonction qui retourne le code dans wallsprites
    private void SetUp()
    {
        Debug.Log("setup");
        for (int x = -1; x <m_numberColumns+1; x++)
        {
            for (int y = -1; y < m_numberRows +1; y++)
            {
                //Prepare l'instanciation d'un sprite floor
                GameObject to_inst = m_floorsSprite[Random.Range(0,m_floorsSprite.Length)];
                //On regarde si la possition n'est pas un bord de map
                if (x == -1 || x == m_numberColumns || y ==  -1 || y == m_numberRows)
                {
                    if(x == -1 && y == m_numberRows)// Corner TL
                    {
                        to_inst = m_wallCornerSprite[0];
                    }
                    else if (x ==  -1 && y == -1)// Corner DL
                    {
                        to_inst = m_wallCornerSprite[2];
                    }
                    else if (x == m_numberColumns && y == m_numberRows)//Corner TR
                    {
                        to_inst = m_wallCornerSprite[1];
                    }
                    else if (x == m_numberColumns && y == -1)//Corner DR 
                    {
                        to_inst = m_wallCornerSprite[3];
                    }
                    else if (x == - 1) //left
                    {
                        to_inst = m_wallSprite[2];
                    }
                    else if(y ==- 1) //down
                    {
                        to_inst = m_wallSprite[3];
                    }
                    else if(y == m_numberRows) // top
                    {
                        to_inst = m_wallSprite[0];
                    }     
                    else if(x == m_numberColumns)
                    {
                        to_inst = m_wallSprite[1];
                    }
                    
                }
                Instantiate(to_inst, new Vector2(x, y), Quaternion.identity, environment.transform);
            }
        }
        Debug.Log("setup done !");
    }

    private Vector2 GetRandomPosition(bool need_toRemove)
    {
        int randomIndex = Random.Range(0, m_gridPositions.Count);
        Vector2 randomPosition = m_gridPositions[randomIndex];
        if(need_toRemove)
        {
            m_gridPositions.RemoveAt(randomIndex);
            //m_basicDonjonScript.SetValue(randomPosition, 1);
        }
        return randomPosition;
    }

    private void DisplayObjectAtRandomPosition(GameObject[] spritesToDisplay, Count count)
    {
        int numberOfObjectToDisplay = Random.Range(count.min, count.max);
        for(int i=0; i< numberOfObjectToDisplay; i++)
        {
            GameObject inst = spritesToDisplay[Random.Range(0, spritesToDisplay.Length)];
            bool to_remove = true;
            Vector2 v_temp = GetRandomPosition(to_remove);
            Instantiate(inst, v_temp, Quaternion.identity, environment.transform);
        }
    }

    
    public void SetupScene (int level, GameObject player)
    {
        // destroy previous scene
        DestroyPreviousScene();
        //m_basicDonjonScript = new BasicDonjon(m_numberColumns, m_numberRows);
        SetUp();
        //m_basicDonjonScript.toString();
        InitialiseList();
        // Obstacles
        DisplayObjectAtRandomPosition(m_obstaclesSprites, new Count(5, 9));
        // Objects
        DisplayObjectAtRandomPosition(m_objects, new Count(1, 2));
        // Enemies
        int ennemiCount = 2 + (int)Mathf.Log(level, 2f);
        DisplayObjectAtRandomPosition(m_enemies, new Count(ennemiCount, ennemiCount));

        //Instanciation de l'entrée
        int entry_x = Random.Range(1, m_numberColumns - 2);
        int entry_y = -1;
        //m_player.transform.Translate(new Vector2(entry_x, entry_y));

        Instantiate(m_entrySprite, new Vector2(entry_x, entry_y), Quaternion.identity, environment.transform);

        //Instanciation du joueur
        if (level == 1)
        {
            m_player = Instantiate(
                player,
                new Vector2(
                       entry_x,
                       entry_y + 1
                        ),
                Quaternion.identity
                );
        }
        else
        {
            m_player.transform.position = new Vector2(entry_x, entry_y + 1);
        }

        //Instanciation de la sortie
        m_exit_position = new Vector2(Random.Range(
                     1, m_numberColumns - 2), m_numberRows);
        m_exit_door = Instantiate(
            m_exitSprite_base,
            new Vector2(m_exit_position.x, m_exit_position.y),
            Quaternion.identity,
            environment.transform);

    }

    private void DestroyPreviousScene()
    {
        foreach (Transform child in environment.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void OpenDoor()
    {
        Destroy(m_exit_door);
        Instantiate(
            m_exitSprite_open,
            new Vector2(m_exit_position.x, m_exit_position.y),
            Quaternion.identity,
            environment.transform);
    }

}
