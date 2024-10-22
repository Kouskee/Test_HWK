using System.Threading;
using System.Threading.Tasks;
using Units;
using UnityEngine;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    [SerializeField] private Character _playerPrefab;
    [SerializeField] private Character _aiPrefab;

    private Character[] _players;
    private int _currentPlayerIndex = 0;
    private CancellationToken _token;
    private CancellationTokenSource _cts;

    public static UnityEvent CharacterDead = new();

    private void Awake()
    {
        CharacterDead.AddListener(() => Restart());
    }

    private void Start()
    {
        _cts = new CancellationTokenSource();
        _token = _cts.Token;

        _players = new Character[2];
        _players[0] = Instantiate(_playerPrefab);
        _players[1] = Instantiate(_aiPrefab);

        var mediator = new Mediator(_players);
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i].Init(mediator);
        }

        StartTurn();
    }

    private void StartTurn()
    {
        _players[_currentPlayerIndex].StartTurn(EndTurn);
    }

    private async void EndTurn()
    {
        if(_token.IsCancellationRequested) return;
        _players[_currentPlayerIndex].StopTurn();
        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Length;
        await Task.Delay(1000); // artificial deceleration
        StartTurn();
    }

    public void Restart()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i].ResetValues();
        }
    }

    private void OnApplicationQuit()
    {
        _cts.Cancel();
    }
    private void OnDestroy()
    {
        _cts.Cancel();
    }
}