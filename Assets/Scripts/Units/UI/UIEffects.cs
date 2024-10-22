using Units.Effect;
using UnityEngine;
using System.Collections.Generic;

namespace Units.UI
{
    public class UIEffects : MonoBehaviour
    {
        [SerializeField] private EffectIcon _prefab;
        [SerializeField] private Transform _parentAndOrigin;

        private PoolObjects<EffectIcon> _poolIcons;

        private List<EffectIcon> _activeIcons;

        private void Awake()
        {
            _poolIcons = new(_prefab, _parentAndOrigin);
            _activeIcons = new List<EffectIcon>();
        }

        public void ShowEffect(EffectType effectType, int duration)
        {
            for (int i = 0; i < _activeIcons.Count; i++)
            {
                if (_activeIcons[i].Icon.text != effectType.ToString())
                    continue;
                _activeIcons[i].Init(effectType, duration);
                return;
            }

            var icon = _poolIcons.Get();
            icon.Init(effectType, duration);
            _activeIcons.Add(icon);
        }

        public void ClearEffect(EffectType effectType)
        {
            for (int i = 0; i < _activeIcons.Count; i++)
            {
                if (_activeIcons[i].Icon.text != effectType.ToString())
                    continue;
                _poolIcons.Release(_activeIcons[i]);
                _activeIcons.RemoveAt(i--);
                return;
            }
        }

        public void Step()
        {
            for (int i = 0; i < _activeIcons.Count; i++)
            {
                _activeIcons[i].Step();

                if (_activeIcons[i].Over)
                {
                    _poolIcons.Release(_activeIcons[i]);
                    _activeIcons.RemoveAt(i--);
                    continue;
                }

            }
        }

        public void ResetValues()
        {
            for (int i = 0; i < _activeIcons.Count; i++)
            {
                _poolIcons.Release(_activeIcons[i]);
                _activeIcons.RemoveAt(i--);
            }
        }
    }
}