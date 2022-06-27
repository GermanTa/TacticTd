using System;
using System.Linq;
using UnityEngine;

namespace CodeBase.Minic.BoneArcher
{
  public class AttackBoneArcher : MonoBehaviour
  {
    public Animator Animator;
    public MinicAnimator BoneArcherAnimator;
    public float AttackCooldown = 3f;
    public float Damage = 1f;
    private Collider[] _hits = new Collider[3];
    private int _layerMask;
    public GameObject spawnPointProjectile;
    public Projectile prefabProjectile;
    private bool _isAttacking = false;
    private Transform _enemyTransform;
    private void Awake()
    {
      _layerMask = 1 << LayerMask.NameToLayer("Mob");
    
    }
    private void Update()
    {
      //Hit(out Collider hit);
     
      if(!_isAttacking)
        StartAttack();
      
    }

    private void OnAttack()
    {
      Debug.Log("OnAttack");
      if (Hit(out Collider hit))
      {
       
      }
    }
    private void OnAttackEnded()
    {
      Debug.Log("OnAttackEnded");
      //_attaclCooldown = AttackCooldown;
      _isAttacking = false;
    }

    public void CreateProjectile()
    {
      Projectile projectile = Instantiate(prefabProjectile,spawnPointProjectile.transform.position,Quaternion.identity);
      projectile.PositionEnemy = _hits[0].gameObject;

    }
    

    private void StartAttack()
    {
      if ( Hit(out Collider hit))
      {
        _isAttacking = true;
        transform.LookAt(_hits[0].transform.position);
        CreateProjectile();
      
        BoneArcherAnimator.PlayAttack();
      }
      
    }
    private bool Hit(out Collider hit)
    {
     
      var hitAmount = Physics.OverlapSphereNonAlloc(transform.position, 5, _hits,_layerMask);
      hit = _hits.FirstOrDefault();
      return hitAmount > 0;
 
    }
    
  }
  
}
  

