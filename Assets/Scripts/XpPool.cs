using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class XpPool : MonoBehaviour
{
    public static XpPool instance;

    [SerializeField] private GameObject xpGemBlue;
    [SerializeField] private GameObject xpGemYellow;
    [SerializeField] private GameObject xpGemRed;

    [SerializeField] private GameObject blueObj;
    [SerializeField] private GameObject yellowObj;
    [SerializeField] private GameObject redObj;
    
    
    private List<GameObject> blueXpPoolList = new List<GameObject>();
    private List<GameObject> yellowXpPoolList = new List<GameObject>();
    private List<GameObject> redXpPoolList = new List<GameObject>();
    
    private int blueIndex;
    private int yellowIndex;
    private int redIndex;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }
    
    private void Start()
    {
        StartCoroutine(CreatePool());
    }

    IEnumerator CreatePool()
    {
        for (int i = 0; i < 1500; i++)
        {
            GameObject xpBlue = Instantiate(xpGemBlue,blueObj.transform);
            xpBlue.GetComponent<XpGem>()._xpPool = this;
            blueXpPoolList.Add(xpBlue);
            xpBlue.SetActive(false);
            yield return new WaitForFixedUpdate();
            
            GameObject xpYellow = Instantiate(xpGemYellow,yellowObj.transform);
            xpYellow.GetComponent<XpGem>()._xpPool = this;
            yellowXpPoolList.Add(xpYellow);
            xpYellow.SetActive(false);
            yield return new WaitForFixedUpdate();
            
            GameObject xpRed = Instantiate(xpGemRed,redObj.transform);
            xpRed.GetComponent<XpGem>()._xpPool = this;
            redXpPoolList.Add(xpRed);
            xpRed.SetActive(false);
            yield return new WaitForFixedUpdate();
        }
    }

    public GameObject TakeBlueXpFromPool()
    {
        if (blueIndex >= blueXpPoolList.Count)
        {
            blueIndex = 0;
        }
        GameObject xp = blueXpPoolList[blueIndex++];
        xp.SetActive(true);
        return xp;
    }
    public GameObject TakeYellowXpFromPool()
    {
        if (yellowIndex >= yellowXpPoolList.Count)
        {
            yellowIndex = 0;
        }
        GameObject xp = yellowXpPoolList[yellowIndex++];
        xp.SetActive(true);
        return xp;
    }
    public GameObject TakeRedXpFromPool()
    {
        if (redIndex >= redXpPoolList.Count)
        {
            redIndex = 0;
        }
        GameObject xp = redXpPoolList[redIndex++];
        xp.SetActive(true);
        return xp;
    }

    public void ReturnXpToPool(GameObject xp)
    {
        xp.SetActive(false);
        xp.GetComponent<XpGem>().moveToward = false;
        xp.transform.position = Vector3.zero;
        xp.transform.rotation = quaternion.identity;
    }
}
