using System;

namespace Units.Effect
{
    public class EffectsSystem
    {
        private int _protectValue;
        private int _protectDuration;

        private int _treatmentValue;
        private int _treatmentDuration;

        private int _ignitingValue;
        private int _ignitingDuration;

        private readonly Action<int> _applyDamage;
        private readonly Action<int> _applyHeal;
        private readonly Action<EffectType> _brokeEffect;

        public EffectsSystem(Action<int> applyDamage, Action<int> applyHeal, Action<EffectType> brokeEffect)
        {
            _applyDamage = applyDamage;
            _applyHeal = applyHeal;
            _brokeEffect = brokeEffect;
        }

        public void SetEffect(EffectType type, int value, int duration)
        {
            switch (type)
            {
                case EffectType.Protective:
                    SetValue(ref _protectValue, value, ref _protectDuration, duration);
                    break;
                case EffectType.Treatment:
                    SetValue(ref _treatmentValue, value, ref _treatmentDuration, duration);
                    break;
                case EffectType.Igniting:
                    SetValue(ref _ignitingValue, value, ref _ignitingDuration, duration);
                    break;
            }
        }

        public int GetDamageWithEffects(int damage)
        {
            var protectTemp = _protectValue;
            if(_protectValue <= damage)
                CleanseEffect(EffectType.Protective);
            return Math.Clamp(damage - protectTemp, 0, int.MaxValue);
        }

        public void CleanseEffect(EffectType type)
        {
            switch (type)
            {
                case EffectType.Protective:
                    SetValue(ref _protectValue, 0, ref _protectDuration, 0);
                    _brokeEffect.Invoke(EffectType.Protective);
                    break;
                case EffectType.Treatment:
                    SetValue(ref _treatmentValue, 0, ref _treatmentDuration, 0);
                    break;
                case EffectType.Igniting:
                    SetValue(ref _ignitingValue, 0, ref _ignitingDuration, 0);
                    break;
            }
        }

        public void Step()
        {
            CheckDurationAndChangeValue(ref _protectDuration, ref _protectValue);
            CheckDurationAndChangeValue(ref _treatmentDuration, ref _treatmentValue, _applyHeal);
            CheckDurationAndChangeValue(ref _ignitingDuration, ref _ignitingValue, _applyDamage);
        }

        private void CheckDurationAndChangeValue(ref int duration, ref int value, Action<int> applyValue = null)
        {
            if (duration > 0)
            {
                duration--;
                applyValue?.Invoke(value);
            }
            else if (duration == 0)
                value = 0;
        }

        public void ResetValues()
        {
            SetValue(ref _protectValue, 0, ref _protectDuration, 0);
            SetValue(ref _treatmentValue, 0, ref _treatmentDuration, 0);
            SetValue(ref _ignitingValue, 0, ref _ignitingDuration, 0);
        }

        public void SetValue(ref int value, int valueValue, ref int duration, int durationValue)
        {
            duration = durationValue;
            value = valueValue;
        }
    }
}