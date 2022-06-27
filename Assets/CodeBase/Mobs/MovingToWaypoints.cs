using CodeBase.StaticData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToWaypoints : MonoBehaviour
{
    private WayPoint[] _waypoints;
    LinkedList<MonsterStaticData> _linkedMinicStaticData;
    private int currentWayPoint = 0;

    private float speed = 1.0f;
    private float accuracy = 0.3f;
    private float rotationSpeed = 1.0f;

    public void Construct(WayPoint[] waypoints, LinkedList<MonsterStaticData> linkedMinicStaticData)
    {
        _waypoints = waypoints;
        _linkedMinicStaticData = linkedMinicStaticData;
    }

    private void Update()
    {
        if(_waypoints.Length == 0)
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

        transform.Translate(0,0,speed * Time.deltaTime);
    }

}
