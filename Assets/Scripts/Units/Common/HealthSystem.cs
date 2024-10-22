using System;
using UnityEngine.UI;

namespace Units
{
    public class HealthSystem
    {
        private readonly Image _healthBar;

        private int _health;
        private const int HEALTH_MAX = 100;

        public HealthSystem(Image healthBar)
        {
            _healthBar = healthBar;
            _health = HEALTH_MAX;
        }

        public void ApplyDamage(int damage)
        {
            _health = Math.Clamp(_health - damage, 0, HEALTH_MAX);
            _healthBar.fillAmount = _health / 100f;
            if (_health <= 0)
                Manager.CharacterDead.Invoke();
        }

        public void ApplyHeal(int heal)
        {
            _health = Math.Clamp(_health + heal, 0, HEALTH_MAX);
            _healthBar.fillAmount = _health / 100f;
        }

        public void ResetValues()
        {
            _health = HEALTH_MAX;
            _healthBar.fillAmount = 1;
        }
    }
}
