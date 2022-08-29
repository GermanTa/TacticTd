using UnityEngine;

namespace CodeBase.Minic {
    public abstract class UnitComponent : MonoBehaviour {
        protected UnitBase _unitComponents;

        protected void InjectDependencies(UnitBase unitComponents) {
            _unitComponents = unitComponents;
        }

        public virtual void Dispose() { }
    }
}