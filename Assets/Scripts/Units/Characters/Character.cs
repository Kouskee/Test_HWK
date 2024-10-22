using Units.Abilities;
using Units.Effect;
using System;
using UnityEngine;
using UnityEngine.UI;
using Units.UI;

namespace Units
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected AbilitiesConfig _abilitiesConfig;
        [SerializeField] protected Image _healthBar;
        [SerializeField] protected UIEffects _UIEffects;

        protected AbilitiesSystem _abilities;
        protected HealthSystem _health;
        protected EffectsSystem _effects;

        protected Mediator _mediator;

        public void Init(Mediator mediator)
        {
            _mediator = mediator;
        }

        protected virtual void Awake()
        {
            _abilities = new(_abilitiesConfig, UseAbility);
            _health = new(_healthBar);
            _effects = new(_health.ApplyDamage, _health.ApplyHeal, _UIEffects.ClearEffect);
        }

        public virtual void StartTurn(Action endTurn)
        {
            _effects.Step();
            _abilities.Step();
            _UIEffects.Step();
            endTurn.Invoke();
        }

        public virtual void StopTurn() { }

        private void UseAbility(AbilityData ability)
        {
            switch (ability.Type)
            {
                case AbilityType.Attack:
                    _mediator.Send(ability, this);
                    break;
                case AbilityType.Defense:
                    _effects.SetEffect(EffectType.Protective, ability.Value, ability.Duration);
                    _UIEffects.ShowEffect(EffectType.Protective, ability.Duration);
                    break;
                case AbilityType.Healing:
                    _effects.SetEffect(EffectType.Treatment, ability.ValueByDuration, ability.Duration);
                    _UIEffects.ShowEffect(EffectType.Treatment, ability.Duration);
                    break;
                case AbilityType.Cleansing:
                    _effects.CleanseEffect(EffectType.Igniting);
                    _UIEffects.ClearEffect(EffectType.Igniting);
                    break;
            }
        }

        public void ReceiveAbility(AbilityData ability)
        {
            switch (ability.Type)
            {
                case AbilityType.Attack:
                    var damage = _effects.GetDamageWithEffects(ability.Value);
                    _health.ApplyDamage(damage);
                    if (ability.Duration != 0)
                    {
                        _effects.SetEffect(EffectType.Igniting, ability.ValueByDuration, ability.Duration);
                        _UIEffects.ShowEffect(EffectType.Igniting, ability.Duration);
                    }
                    break;
            }
        }

        public void ResetValues()
        {
            _abilities.ResetValues();
            _health.ResetValues();
            _effects.ResetValues();
            _UIEffects.ResetValues();
        }
    }
}