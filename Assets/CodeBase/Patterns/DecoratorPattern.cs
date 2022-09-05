using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Patterns {
    public class DecoratorPattern {
        
    }

    public class InventoryItem {
        public List<BaseAttackDecorator> decs = new List<BaseAttackDecorator> {
            new DamageMultiplier(2f),
            new DamageMultiplier(3f),
            new DamageMultiplier(0.5f),
            new CritChanceMultiplier(2f),
        };
    }

    public class Inventory {
        public Character _character;
        
        public void Wear(InventoryItem item) {
            
            var result = _character;
            foreach (var dec in item.decs) {
                result = dec.Decorate(result);
            }

            _character = result;
        }
        
        public void Drop(InventoryItem item) {
            
            var result = _character;
            foreach (var dec in item.decs) {
                result = dec.Decorate(result);
            }

            _character = result;
        }
    }

    public class Monobeh : MonoBehaviour {
        private void Start() {
            var baseAttack = new Character {
                damage = 5,
                type = DamageType.Physical
            };
            List<BaseAttackDecorator> _decs = new List<BaseAttackDecorator> {
                new DamageMultiplier(2f),
                new DamageMultiplier(3f),
                new DamageMultiplier(0.5f),
                new CritChanceMultiplier(2f),
            };

        }
    }

    public class Character {
        public float damage;
        public float criticalChance;
        public DamageType type;
    }

    public abstract class BaseAttackDecorator {
        public abstract Character Decorate(Character character);

        public abstract Character DeDecorate(Character character);
    }

    public class DamageMultiplier : BaseAttackDecorator {
        private float _multiplier;

        public DamageMultiplier(float multiplier) {
            _multiplier = multiplier;
        }

        public override Character Decorate(Character character) {
            var newAttack = new Character {
                damage = character.damage,
                type = character.type,
            };

            newAttack.damage *= _multiplier;
            return newAttack;
        }

        public override Character DeDecorate(Character character) {
            throw new NotImplementedException();
        }
    }
    
    public class CritChanceMultiplier : BaseAttackDecorator {
        private float _multiplier;

        public CritChanceMultiplier(float multiplier) {
            _multiplier = multiplier;
        }

        public override Character Decorate(Character character) {
            var newAttack = new Character {
                damage = character.damage,
                type = character.type,
            };

            newAttack.criticalChance *= _multiplier;
            return newAttack;
        }

        public override Character DeDecorate(Character character) {
            throw new NotImplementedException();
        }
    }

    [Flags]
    public enum DamageType {
        Physical = 1 << 0, //0001
        Fire = 1 << 1,//0010
        Ice = 1 << 2,//0100
        Poison = 1 << 3, //1000
        MagicDamage = Fire | Ice //0110
    } 
}