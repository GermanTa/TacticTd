
using CodeBase.infrastructure.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class MovingToCells : MonoBehaviour
    {
     
        
   
        
        private bool oTheCellPlayer = false;
        private int currentWp = 0;
        private float rotSpeed = 25f;
        private double accuracy = 0.1f;
        private float speed = 1f;

        RaycastHit hit;
        private void Update()
        {

            //Ray ray = new Ray( transform.position + new Vector3(0,0.5f,0),transform.forward);
            //Debug.DrawRay(transform.position + new Vector3(0,0.5f,0), transform.forward, Color.blue);
        
            //if (!Physics.Raycast(ray, out hit, 0.7f))
            //{
                  
            //    Vector3 lookAtGoal = new Vector3(_pathNodes[currentWp].xIndex, transform.position.y, _pathNodes[currentWp ].yIndex);
            //    Vector3 direction = lookAtGoal - transform.position;
            //    transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
                
            //    if (direction.magnitude < accuracy)
            //    {
            //        currentWp++;
            
            //    }
                
            //    Animator.Move();
            //    transform.Translate(0,0,speed * Time.deltaTime);
                
            //}
            //else
            //{
            //    Animator.StopMoving();
            //    Debug.Log("Figth");
            //}
         


    }
        
       
    }
}

