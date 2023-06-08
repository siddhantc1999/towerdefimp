using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyprefab;
    [SerializeField] int poolLength;
    public GameObject[] enemylist;
    [SerializeField] Vector3 startposition;
    public int j;
    private void Awake()
    {
        enemylist = new GameObject[poolLength];
        for(int i=0;i<poolLength;i++)
        {
          
            enemylist[i]=Instantiate(enemyprefab,Vector3.zero,Quaternion.identity);
            enemylist[i].SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyInstantiate());    
    }

   IEnumerator EnemyInstantiate()
    {
        while(true)
        {
            

            EnableObjectPool();
            yield return new WaitForSeconds(1f);
            
            //yield return new WaitForSeconds(1f);
        }
    }

    private void EnableObjectPool()
    {
        for (int j = 0; j < poolLength; j++)
        {
            //i value is incrementing itself
            Debug.Log("the value of i " + j);
            //Debug.Log("gameobject active " + enemylist[i].activeInHierarchy);
            if (enemylist[j].activeSelf == false)
            {

                //Debug.Log("the value of i "+i);
                enemylist[j].SetActive(true);
                enemylist[j].transform.position = startposition;
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
