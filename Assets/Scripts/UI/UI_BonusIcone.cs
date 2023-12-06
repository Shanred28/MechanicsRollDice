using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Класс устанавливает нужный бонус.
public class UI_BonusIcone : MonoBehaviour
{
    [SerializeField] private TMP_Text _numbBonusText;
    [SerializeField] private TMP_Text _nameBonusText;
    [SerializeField] private Image _iconImage;

    private int _bonusVelue;
    public int BonusVelue => _bonusVelue;

    public void SetBonus(int numb, string name , Sprite icon)
    {
        _bonusVelue = numb;
        _numbBonusText.text ="+" + numb.ToString();
        _nameBonusText.text = name;
        _iconImage.sprite = icon;
    }
        
}
