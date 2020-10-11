using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: refactor the movement by adding the donjon if obstacle = on prends pas cette position 
public class EnnemiAI : MonoBehaviour
{
    private enum State
    {
        Chasing,
        //Pe plus 
    }

    public int m_force;
    public int m_health;
    private GameObject m_target;
    private float m_speed = 100f;
    //private List<List<A_Star.Node>> m_grid;
    private BoxCollider2D bC;
    private Rigidbody2D rb2D;

    private void Start()
    {
        GameManager.m_instance.AddEnnemis(this);
        m_target = GameObject.FindGameObjectWithTag("Player");
        bC = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        Debug.Log(rb2D);
        Debug.Log(m_target);
        //m_grid = new List<List<A_Star.Node>>();
    }

    /*public void Init(List<List<A_Star.Node>> grid)
    {
        m_grid = grid;
        string a = "";
        foreach (List<A_Star.Node> n in grid)
        {
            foreach(A_Star.Node nbis in n)
            {
                a += nbis.Position.ToString();
            }
            a += "\n";
        }
        Debug.Log(a);
    }*/

    public void Died()
    {
        Destroy(this);
    }

    public void ActionToDo()
    {
        Debug.Log("Acction to do");
        //Si l'ennemie est proche de sa cible
        if(Vector2.Distance(m_target.transform.position, transform.position) <= 0.4f)
        {
            //Attaque de l'ennemie
        }
        else
        {
            /*Stack<>
            TryToMove(xDir, yDir);*/
        }
    }

    //Mouvement
    public void TryToMove(Stack<A_Star.Node> path)
    {
        Debug.Log("Try to move");
       foreach(A_Star.Node n in path)
        {
            Debug.Log(n.Position*64);
            Move(n.Position*64);
        }
    }

    bool Move(Vector2 target_position)
    {
        Debug.Log("move");
        Debug.Log(rb2D.position);
        rb2D.MovePosition(rb2D.position + target_position * m_speed * Time.deltaTime);
        return true;
    }

    private void FixedUpdate()
    {
        Debug.Log(m_target);
        Debug.Log(m_target.transform.position);
        List<List<A_Star.Node>> m_grid = BoardManager.m_grid;
        if(m_grid != null)
        {
            A_Star.Astar _a = new A_Star.Astar(m_grid);
            string a = "";
            foreach (List<A_Star.Node> n in m_grid)
            {
                foreach (A_Star.Node nbis in n)
                {
                    a += nbis.Position.ToString();
                }
                a += "\n";
            }
            Debug.Log(a);
            Vector2 start = transform.position;
            Stack<A_Star.Node> l = _a.FindPath(start, m_target.transform.position);
            Move(l.Pop().Position*64);
        }
        
    }
}
