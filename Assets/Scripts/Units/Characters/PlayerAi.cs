using System;
using Random = UnityEngine.Random;

namespace Units
{
    public class PlayerAi : Character
    {
        public override void StartTurn(Action endTurn)
        {
            base.StartTurn(endTurn);
            CastRandomAbility();
        }

        private void CastRandomAbility()
        {
            int count = 0;
            while (count < 5)
            {
                for (int i = 0; i < _abilities.CountOfAbilities; i++)
                {
                    if (!_abilities.CanUse(i) || Random.value > .5f)
                        continue;
                    _abilities.Use(i);
                    return;
                }
                count++;
            }
        }

        public override void StopTurn()
        {
            base.StopTurn();
        }
    }
}