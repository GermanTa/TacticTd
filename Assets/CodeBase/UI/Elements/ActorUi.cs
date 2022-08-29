using CodeBase.infrastructure.Logic;
using CodeBase.Minic;
using UnityEngine;

namespace CodeBase.UI.Elements {
    public class ActorUi : UnitComponent {
        public HpBar HpBar;
        private UnitHealth _health;
        private void OnDestroy() => _health.HealthChanged -= UpdateHpBar;

        public void InjectDependencies(UnitBase unitComponents, UnitHealth health) {
            InjectDependencies(unitComponents);
            _health = health;
            _health.HealthChanged += UpdateHpBar;
        }

        private void UpdateHpBar() {
            HpBar.SetValue(_health.CurrentHp, _health.MaxHp);
        }
    }
}