using CodeBase.StaticData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToWaypoints : MonoBehaviour
{
    ISpawnerService _spawnerService;
    private WayPoint[] _waypoints;
    LinkedList<GameObject> _linkedMinicStaticData;
    List<GameObject> _listMinicStaticData;

    private int index;
    private int currentWayPoint = 0;
    private float speed = 1.0f;
    private float accuracy = 0.3f;
    private float rotationSpeed = 1.0f;

    public int Index
    {
        set { index = value; }
    }

    public void Construct(WayPoint[] waypoints, LinkedList<GameObject> linkedMinicStaticData, List<GameObject> listMinicStaticData)
    {
        _waypoints = waypoints;
        _linkedMinicStaticData = linkedMinicStaticData;
        _listMinicStaticData = listMinicStaticData;
        
    }
    public void Construct(ISpawnerService spawnerService)
    {
        _spawnerService = spawnerService;

    }

    private void Start()
    {
        _spawnerService.ChangedListMobsGO += Reindex;
    }


    public void Reindex(int index)
    {
        if(this.index > index)
        {
            this.index -= 1;
        }
       
    }
    private void OnDestroy()
    {
        _spawnerService.DeleteMobFromList(index);
    }
    private void Update()
    {
        if (_waypoints.Length == 0)
        {
            Debug.Log("ERR: _waypoints.Length == 0");
            return;
        }

        var wayPointTransfrom = _waypoints[currentWayPoint].transform.position;
        Vector3 lookAtGoal = new Vector3(wayPointTransfrom.x, transform.position.y, wayPointTransfrom.z);
        Vector3 direction = lookAtGoal - transform.position;
        transform.LookAt(wayPointTransfrom);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        

        if(direction.magnitude < accuracy)
        {
            currentWayPoint++;
            if(currentWayPoint >= _waypoints.Length)
            {
                Debug.Log("ERR: waypoints ended");
                return;
            }
        }


        if (index > 0 && _listMinicStaticData[index - 1] != null)
        {
            if ((_listMinicStaticData[index - 1].transform.position - transform.position).magnitude > 1f)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
        } else
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }


    }

    

}
