using CodeBase.infrastructure.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class ActorUi : MonoBehaviour
  {
    public HpBar HpBar;
    private IHealth _helth;
    private void OnDestroy() => _helth.HealthChanged -= UpdateHpBar;
    public void Construct(IHealth health)
    {
      _helth = health;

      _helth.HealthChanged += UpdateHpBar;
    }

    private void UpdateHpBar()
    {
      HpBar.SetValue(_helth.CurrentHp, _helth.MaxHp);
    }
  }
}