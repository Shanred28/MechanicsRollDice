using TMPro;
using UnityEngine;

public class PopupBonus : MonoBehaviour
{
    [SerializeField] private TMP_Text _bonusText;
    [SerializeField] private float _timeDestroy;
    [SerializeField] private float _speed;

    private void Update()
    {
        if (_timeDestroy > 0)
        {
            _timeDestroy -= Time.deltaTime;
            transform.position += Vector3.up * _speed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetBonusValue(int numbValue)
    {
        _bonusText.text ="+" + numbValue.ToString();
    }
}
