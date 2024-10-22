using System;
using UnityEngine.UI;

namespace Units
{
    public class Player : Character
    {
        public Button[] _abilityButtons;
        private Action _endTurn;

        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < _abilityButtons.Length; i++)
            {
                var index = i;
                _abilityButtons[i].onClick.AddListener(
                    () => UseAbilityOnClick(index));
            }
        }

        public override void StartTurn(Action endTurn)
        {
            for (int i = 0; i < _abilityButtons.Length; i++)
            {
                _abilityButtons[i].interactable = _abilities.CanUse(i);
            }

            _effects.Step();
            _abilities.Step();
            _UIEffects.Step();
            _endTurn = endTurn;
        }

        private void UseAbilityOnClick(int index)
        {
            _abilities.Use(index);
            _endTurn.Invoke();
        }

        public override void StopTurn()
        {
            for (int i = 0; i < _abilityButtons.Length; i++)
            {
                _abilityButtons[i].interactable = false;
            }
            base.StopTurn();
        }
    }
}
