using System;

namespace Units.Abilities
{
    public class AbilitiesSystem
    {
        private readonly Ability[] _abilities;
        private readonly Action<AbilityData> _sendUsed;

        public int CountOfAbilities => _abilities.Length;

        public AbilitiesSystem(AbilitiesConfig config, Action<AbilityData> sendUsed)
        {
            _sendUsed = sendUsed;
            _abilities = new Ability[config.Abilities.Length];
            for (int i = 0; i < _abilities.Length; i++)
            {
                _abilities[i] = new(config.Abilities[i]);
            }
        }

        public void Step()
        {
            for (int i = 0; i < _abilities.Length; i++)
            {
                if (!_abilities[i].ReadyToUse)
                    _abilities[i].Step();
            }
        }

        public bool CanUse(int index)
        {
            if (index < 0 || index >= CountOfAbilities) 
                return false;

            return _abilities[index].ReadyToUse;
        }

        public void Use(int index)
        {
            if (index < 0 || index >= CountOfAbilities) return;

            _sendUsed.Invoke(_abilities[index].Data);
            _abilities[index].Use();
        }

        public void ResetValues()
        {
            for (int i = 0; i < _abilities.Length; i++)
            {
                _abilities[i].ResetValues();
            }
        }
    }
}