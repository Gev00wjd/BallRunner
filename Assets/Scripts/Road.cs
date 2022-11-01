using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    private float _timer;
    
    
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private GameObject obstraclePrefab;
    [SerializeField] private GameObject Player;
    
    private List<GameObject> roads = new List<GameObject>();
    private List<GameObject> obstracles = new List<GameObject>();
    
    
    private float _maxSpeed;
    private float _speed = 0;
    public int maxRoadCount;
    public int maxObstraclesCount;

    private void Start()
    {
        _maxSpeed = PlayerPrefs.GetInt("MaxSpeed");
        GameObject pl = Instantiate(Player, Vector3.zero, Quaternion.identity, transform.parent);
        pl.transform.localPosition = Vector3.zero;
        ResetLevel();
        StartLevel();
    }


    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 15)
        {
            _maxSpeed += 5;
            _speed = _maxSpeed;
            _timer = 0;
        }
        
        if(_speed == 0) return;
        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, _speed * Time.deltaTime);
        }
        foreach (GameObject obstracle in obstracles)
        {
            obstracle.transform.position -= new Vector3(0, 0, _speed * Time.deltaTime);
        }

        if (roads[0].transform.position.z < -20)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
            CreateNextRoad();
        }

        if (obstracles[0].transform.position.z < -5)
        {
            Destroy(obstracles[0]);
            obstracles.RemoveAt(0);
            CreateNextObstracle();
        }
    }
    public void ChangeMaxSpeed(int count)
    {
        _maxSpeed = count;
        PlayerPrefs.SetInt("MaxSpeed", count);
    }
    void StartLevel()
    {
        _speed = _maxSpeed;
    }
    void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;
        if (roads.Count > 0) pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 50);
        GameObject rd = Instantiate(roadPrefab, pos, Quaternion.identity);
        rd.transform.SetParent(transform);
        roads.Add(rd);
    }
    void CreateNextObstracle()
    {
        float RandomNum = Random.Range(10, 57);
        Vector3 pos = new Vector3(0,RandomNum,0);
        if (obstracles.Count > 0) pos = new Vector3(obstracles[obstracles.Count - 1].transform.position.x, RandomNum, obstracles[obstracles.Count - 1].transform.position.z + 35);
        GameObject ob = Instantiate(obstraclePrefab, pos, Quaternion.identity);
        ob.transform.SetParent(transform);
        obstracles.Add(ob);
    }

    void ResetLevel()
    {
        _speed = 0;
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
        while (obstracles.Count > 0)
        {
            Destroy(obstracles[0]);
            obstracles.RemoveAt(0);
        }
        for (int i = 0; i < maxObstraclesCount; i++)
        {
            CreateNextObstracle();
        }
    }

}
