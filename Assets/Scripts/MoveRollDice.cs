using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MoveRollDice : MonoBehaviour
{
    public event UnityAction OnStopRoll;

    [SerializeField] private Transform[] _sids;

    [SerializeField] private int _startSide;

    [SerializeField] private Transform _targetDiceVisualModel;
    [SerializeField] private Transform _targetDice;

    [Header("Motion")]
    [SerializeField] private float _speedRoll;
    [SerializeField] private float _brakingRoll;
    [SerializeField] private float _brakingMove;
    [SerializeField] private float _speedMove;
    //Значения скорости вращения, на которой возвращается на точку старта.
    [SerializeField] private float _speedReturnStartPoint;

    [Header("Boundary")]
    [SerializeField] private BoundaryMoveDice _boundaryMoveDice;
    [SerializeField] private float _offsetBoundary;

    [SerializeField] private ParticleSystem _particleSystem;

    

    private Transform _aim;
    private bool _isRolling;
    private int numberOnDice;
    private Vector3 _startPos;

    private void Start()
    {
        _aim = Camera.main.transform;
        _startPos = transform.position;
        SetSide(_startSide);
    }

    //Устанавливает нужную сторону кубика к камере. 
    public void SetSide(int numberOnDice)
    {
        _targetDiceVisualModel.localRotation = Quaternion.FromToRotation(_sids[numberOnDice].localPosition, _aim.transform.position);

        var angle = Vector3.SignedAngle(_sids[numberOnDice].transform.up, transform.up, transform.forward);

        transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
    }

    //Точка входа для запуска броска кубика.
    public void StartRollDice(int number)
    {
        if (_isRolling == true) return;

        StartCoroutine(RollDice());
        numberOnDice = number;
        _isRolling = true;      
    }

    //Вращает и передвигает кубик
    private IEnumerator RollDice()
    {             
        float speedRoll = _speedRoll;
        float speedMove = _speedMove;
        float radius = _boundaryMoveDice.radius;
        Vector3 targetMoveDice = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0).normalized;

        while (speedRoll > 0)
        {
            _targetDiceVisualModel.Rotate( new Vector3(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360)), speedRoll * Time.deltaTime);
            
            if (speedRoll > _speedRoll * _speedReturnStartPoint)
            {
                transform.Translate(targetMoveDice * speedMove * Time.deltaTime);
            }
            else
            {              
                transform.position = Vector3.MoveTowards(transform.position, _startPos, speedMove * Time.deltaTime);
            }

            if (transform.position.magnitude + _offsetBoundary >= radius)
            {
                targetMoveDice =  new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0).normalized;
            }

            speedMove -= Time.deltaTime * _brakingMove;
            speedRoll -= Time.deltaTime * _brakingRoll;

            yield return null;
        }
       
        SetSide(numberOnDice);
        OnStopRoll?.Invoke();
        _particleSystem.Play();
        _isRolling = false;
    }
}
