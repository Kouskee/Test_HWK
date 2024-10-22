namespace Units.Abilities
{
    public class Ability
    {
        private AbilityData _data;
        public AbilityData Data => _data;

        private int _cooldown;
        public bool ReadyToUse => _cooldown <= 0;

        public Ability(AbilityData data)
        {
            _data = data;
        }

        public void Use()
        {
            _cooldown = Data.Cooldown;
        }

        public void Step()
        {
            if (_cooldown > 0)
                _cooldown--;
        }

        public void ResetValues() => _cooldown = 0;
    }
}