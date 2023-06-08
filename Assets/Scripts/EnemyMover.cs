using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public List<Node> Path=new List<Node>();
    public List<Vector2Int> pathVector;
    GameObject pathLane;
    Pathfinder pathfinder;
    GridManager gridManager;
    public Vector3 endPosition;
    public Vector3 whileendPosition;
    public float maintimer;
    // Start is called before the first frame update
    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
        FindObjectOfType<Pathfinder>().regeneratepath += RegeneratePath;
    }
    void OnEnable()
    {
        Debug.Log("on enable");
        RegeneratePath();
    }
  //create aniotgher methodf for findpath and startcoroutine move
   public void RegeneratePath()
    {

        StopAllCoroutines();
        FindPath();
        ReturnToStart();
        StartCoroutine(Move());
    }

    private void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(Path[0].coordinates);
    }

    private void FindPath()
    {


        Path.Clear();
        pathVector.Clear();
        //thgis has to be called again
        //Debug.Log("in find new path");
        Vector2Int coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        Path = pathfinder.GetNewPath(coordinates);
        foreach(Node path in Path)
        {
            pathVector.Add(path.coordinates);
        }
        //Debug.Log("the coordinates "+Path[1].coordinates);
    }

    IEnumerator Move()
    {

        //foreach to for
        // for(int i=0;i<Path.count;i++)
        //path with node
        //foreach (Node waypointnode in Path)
      //i should start from 1
        for (int i = 1; i < Path.Count; i++)
        {
           
            //if (i == 1)
            //{
            //    Debug.Log("the i " + i);
            //}
            Vector3 startPosition = transform.position;
            endPosition = gridManager.GetPositionFromCoordinates(Path[i].coordinates);
            float timer = 0;
            transform.LookAt(endPosition);
            while(timer<=1f)
            {

                transform.position = Vector3.Lerp(startPosition,endPosition,timer);
                //Debug.Log("the value of i "+i);
                //Debug.Log("the endposition "+ endPosition);
                //whileendPosition = endPosition;
                timer += Time.deltaTime;
                maintimer = timer;
                yield return new WaitForEndOfFrame();
            }
        }
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("here in log");
    //}
    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("here in log");
    }
}
