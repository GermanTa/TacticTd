using CodeBase.Enemy;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator))]
public class MovingToWaypoints : MonoBehaviour {
    ISpawnerService _spawnerService;
    private WayPoint[] _waypoints;
    List<GameObject> _listMinicStaticData;
    public EnemyAnimator Animator;

    private MovingState _currentState;
    public MovingState CurrentState => _currentState;


    public string id;
    private int currentWayPoint = 0;
    private float speed = 1.0f;
    private float accuracy = 0.3f;
    private float rotationSpeed = 10f;
    


    public void Construct(WayPoint[] waypoints) {
        _waypoints = waypoints;
        if (_waypoints.Length == 0) {
            Debug.LogError("ERR: _waypoints.Length == 0");
        }
    }
 
    private void OnDestroy() {
        //_spawnerService.DeleteMobFromList(id);
    }

    private void Start()
    {
        
    }

    private void Update() {

        if (_currentState == MovingState.Staying) {
            Animator.Staying();
            return;
        }

        if (currentWayPoint >= _waypoints.Length) {
            Animator.Staying();
            return;
        }

        Animator.Move();

        var wayPointTransfrom = _waypoints[currentWayPoint].transform.position;
        Vector3 lookAtGoal = new Vector3(wayPointTransfrom.x, transform.position.y, wayPointTransfrom.z);
        Vector3 direction = lookAtGoal - transform.position;
        //transform.LookAt(wayPointTransfrom);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);


        if (direction.magnitude < accuracy) {
            currentWayPoint++;
            if (currentWayPoint >= _waypoints.Length) {
                Debug.Log("ERR: waypoints ended");
                return;
            }
        }
        
        transform.Translate(0, 0, speed * Time.deltaTime);
        
    }

    public void SetState(MovingState state) {
        _currentState = state;
    }
}

public enum MovingState {
    Walking,
    Staying,
}