using System;
using UnityEngine;

namespace Units.Abilities
{
    [Serializable]
    public class AbilityData
    {
        public AbilityType Type;
        [Range(0, 15)] public int Value;
        [Range(0, 15)] public int ValueByDuration;
        [Range(0, 10)] public int Duration;
        [Range(0, 10)] public int Cooldown;
    }
}