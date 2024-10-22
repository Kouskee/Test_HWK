using UnityEngine;

namespace Units.Abilities
{
    [CreateAssetMenu(fileName = "Ability Config", menuName = "Configs/Abilities")]
    public class AbilitiesConfig : ScriptableObject
    {
        [SerializeField] private AbilityData[] _abilities;
        public AbilityData[] Abilities => _abilities;
    }
}