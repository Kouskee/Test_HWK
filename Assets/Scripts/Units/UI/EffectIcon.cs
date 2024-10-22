using Units.Effect;
using UnityEngine;
using TMPro;

namespace Units.UI
{
    public class EffectIcon : MonoBehaviour
    {
        public TMP_Text Icon;
        public TMP_Text DurationTxt;

        private int _duration;
        public bool Over => _duration <= 0;

        public void Init(EffectType effectType, int duration)
        {
            _duration = duration;
            Icon.text = effectType.ToString();
            DurationTxt.text = duration.ToString();
        }

        public void Step()
        {
            if (_duration > 0)
                _duration--;
            DurationTxt.text = _duration.ToString();
        }
    }
}