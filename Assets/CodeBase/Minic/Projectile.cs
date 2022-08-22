using System;
using UnityEngine;

namespace CodeBase.Minic
{
    public class Projectile : MonoBehaviour
    {
        private GameObject _positionEnemy;
        private float projectileSpeed = 2f;
        private int _layerMask;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Mob");
        }

        public GameObject PositionEnemy
        {
            set { _positionEnemy = value; }
        }
        
        private void Update()
        {
            Vector3 directionToTarget = (_positionEnemy.transform.position - transform.position).normalized;
            transform.Translate(directionToTarget * projectileSpeed * Time.deltaTime);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            
            if(((1 << other.gameObject.layer) & _layerMask) != 0)
            {

                Debug.Log(other.transform.parent.gameObject.name + " ---- dsfsdfsdf" );
                //other.transform.GetComponent<IHealth>().TakeDamage(Damage);
                //Destroy Object
                Destroy(gameObject);
            }
            
            
        }
    }
}
