using UnityEngine;
using UnityEngine.Events;

//Класс для сбора евентов от кубика, запуск броска кубика.
public class DiceController : MonoBehaviour
{
    public event UnityAction<int> OnStartRollDice;
    public event UnityAction OnEndRollDice;

    private ResultOfDiceRoll _diceRollResult = new ResultOfDiceRoll();
    [SerializeField] private MoveRollDice _moveRollDice;
    [SerializeField] private ParticleSystem _particleSystemSucces;
    [SerializeField] private ParticleSystem _particleSystemLose;

    #region UnityEvent
    private void Start()
    {
        _moveRollDice.OnStopRoll += OnStopRoll;
    }

    private void OnDestroy()
    {
        _moveRollDice.OnStopRoll -= OnStopRoll;
    }
    #endregion

    #region Public API
    private void OnStopRoll()
    {
        OnEndRollDice?.Invoke();
    }

    public void StartRollDice()
    {        
        var result = _diceRollResult.ResultDiceRoll();
        OnStartRollDice?.Invoke(result);
        _moveRollDice.StartRollDice(result);
    }

    public void RollDiceFinishNumb(int bonus)
    {
        _moveRollDice.SetSide(bonus);
    }

    public void StartParticle(bool win)
    {
        if (win == true)
        {
            SucessStartParticle();
        }
        if (win == false)
        {
            LoseStartParticle();
        }       
    }
    #endregion

    #region Methods
    private void SucessStartParticle()
    {
        _particleSystemSucces.Play();
    }
    private void LoseStartParticle()
    {
        _particleSystemLose.Play();
    }
    #endregion
}
