using Units;
using Units.Abilities;

public class Mediator
{
    private Character[] _characters;

    public Mediator(params Character[] characters)
    {
        _characters = characters;
    }

    public void Send(AbilityData ability, Character character)
    {
        for (int i = 0; i < _characters.Length; i++)
        {
            if (_characters[i] == character) continue;
            _characters[i].ReceiveAbility(ability);
            return;
        }
    }
}
