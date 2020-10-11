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
    public GameObject m_objet;
    public GameObject m_player;
    public EnnemiAI m_ennemi;
    public static List<List<A_Star.Node>> m_grid;
    private Vector2 m_exit_position;


    //TODO : Ajout des portes + fenetre

    //Positions des cases pouvant etre utilisées pour placer un element => impossible de mettre deux elemeents sur la meme case
    private static List<Vector2> m_gridPositions = new List<Vector2>();
    private Dictionary<Vector2, bool> m_nonWalkableList = new Dictionary<Vector2, bool>();
    //private BasicDonjon m_basicDonjonScript;
    
    //Initialisation de la liste des obstacles

    public static List<Vector2> GetGridPos()
    {
        return m_gridPositions;
    }

    //Nettoie la liste
    void initialiseList()
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
    private void setUp()
    {
        Debug.Log("setup");
        for (int x = -1; x <m_numberColumns+1; x++)
        {
            for (int y = -1; y < m_numberRows +1; y++)
            {
                int v_temp = 0;
                //Prepare l'instanciation d'un sprite floor
                GameObject to_inst = m_floorsSprite[Random.Range(0,m_floorsSprite.Length)];
                //On regarde si la possition n'est pas un bord de map
                if (x == -1 || x == m_numberColumns || y ==  -1 || y == m_numberRows)
                {
                    v_temp = 1;
                    if(x == -1 && y == m_numberRows)// Corner TL
                    {
                        to_inst = m_wallCornerSprite[0];
                        m_nonWalkableList.Add(new Vector2(x, y), false);
                    }
                    else if (x ==  -1 && y == -1)// Corner DL
                    {
                        to_inst = m_wallCornerSprite[2];
                        m_nonWalkableList.Add(new Vector2(x, y), false);
                    }
                    else if (x == m_numberColumns && y == m_numberRows)//Corner TR
                    {
                        to_inst = m_wallCornerSprite[1];
                        m_nonWalkableList.Add(new Vector2(x, y), false);
                    }
                    else if (x == m_numberColumns && y == -1)//Corner DR 
                    {
                        to_inst = m_wallCornerSprite[3];
                        m_nonWalkableList.Add(new Vector2(x, y), false);
                    }
                    else if (x == - 1) //left
                    {
                        to_inst = m_wallSprite[2];
                        m_nonWalkableList.Add(new Vector2(x, y), false);
                    }
                    else if(y ==- 1) //down
                    {
                        to_inst = m_wallSprite[3];
                        m_nonWalkableList.Add(new Vector2(x, y), false);
                    }
                    else if(y == m_numberRows) // top
                    {
                        to_inst = m_wallSprite[0];
                        m_nonWalkableList.Add(new Vector2(x, y), false);
                    }     
                    else if(x == m_numberColumns)
                    {
                        to_inst = m_wallSprite[1];
                        m_nonWalkableList.Add(new Vector2(x, y), false);
                    }
                    
                }
                GameObject inst = Instantiate(to_inst, new Vector2(x, y), Quaternion.identity) as GameObject;
                //m_basicDonjonScript.SetValue(new Vector2(x, y),v_temp);
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
            m_nonWalkableList.Add(randomPosition, false);
        }
        return randomPosition;
    }

    private void DisplayOjectAtRandomPosition(GameObject[] spritesToDisplay, Count count)
    {
        int numberOfObjectToDisplay = Random.Range(count.min, count.max);
        for(int i=0; i< numberOfObjectToDisplay; i++)
        {
            GameObject inst = spritesToDisplay[Random.Range(0, spritesToDisplay.Length)];
            bool to_remove = false;
            if (inst.CompareTag("Obstacle"))
                to_remove = true;
            Vector2 v_temp = GetRandomPosition(to_remove);
            Instantiate(inst, v_temp, Quaternion.identity);
        }
    }

    
    public void setupScene (int level)
    {
        //m_basicDonjonScript = new BasicDonjon(m_numberColumns, m_numberRows);
        setUp();
        //m_basicDonjonScript.toString();
        initialiseList();
        DisplayOjectAtRandomPosition(m_obstaclesSprites, new Count(5, 9));
        //m_basicDonjonScript.toString();
        int ennemiCount = (int)Mathf.Log(level, 2f);
        //DisplayOjectAtRandomPosition(m_ennemiSprites, new Count(ennemiCount, ennemiCount));

        //Instanciation de l'entrée
        int entry_x = Random.Range(1, m_numberColumns - 2);
        int entry_y =  - 1;
        Debug.Log("spawn : " + entry_x + " y : " + entry_y);
        m_player.transform.Translate(new Vector2(entry_x, entry_y));
        
        Instantiate(
            m_entrySprite, 
            new Vector2(
                   entry_x, 
                   entry_y
                    ), 
            Quaternion.identity
            );

        //Instanciation du joueur
        Instantiate(
            m_player,
            new Vector2(
                   entry_x,
                   entry_y + 1
                    ),
            Quaternion.identity
            );

        //Instanciation de la sortie
        m_exit_position = new Vector2(Random.Range(
                     1, m_numberColumns - 2), m_numberRows);
        Instantiate(
            m_exitSprite_base, 
            new Vector2(m_exit_position.x,m_exit_position.y), 
            Quaternion.identity);

        m_grid = ConstructGrid();
    }


    public void OpenDoor()
    {
        Instantiate(
            m_exitSprite_open,
            new Vector2(m_exit_position.x, m_exit_position.y),
            Quaternion.identity);
    }
    /*private void FindPath()
    {
        Vector2 target = new Vector2(4, 4);
        A_Star.Astar _a = new A_Star.Astar(ConstructGrid());
        Vector2 start = new Vector2(0, 0);
        Debug.Log("Start :" + start);
        Debug.Log("Target :" + target);
        Stack<A_Star.Node> l =_a.FindPath(start, target);
        foreach(A_Star.Node n in l)
        {
            Debug.Log("chemin passe par :" + n.Position);
        }
        
    }*/

    private List<List<A_Star.Node>> ConstructGrid()
    {
        List<List<A_Star.Node>> l_temp = new List<List<A_Star.Node>>();
        bool walkable = false;

        for(int i=0; i < m_numberColumns;i++)
        {
            l_temp.Add(new List<A_Star.Node>());
            for(int j=0;j<m_numberRows;j++)
            {
                if (m_nonWalkableList.TryGetValue(new Vector2(i, j), out walkable))
                {
                    walkable = false;
                }
                else
                {
                    walkable = true;
                }
                l_temp[i].Add(new A_Star.Node(new Vector2(i, j), walkable));
            }
        }
        return l_temp;
    }

}
