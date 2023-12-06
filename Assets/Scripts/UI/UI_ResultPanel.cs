using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Класс выводит результаты  броска в испытании.
public class UI_ResultPanel : MonoBehaviour
{
    [SerializeField] private ResultTest _resultTest;

    [SerializeField] private Button _nextBotton;
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _resultScoreText;

    [SerializeField] private float _sizeTextLerpRate;

    [SerializeField] private Color _colorWin;
    [SerializeField] private Color _colorLose;

    private void Start()
    {       
        _resultTest.OnResultTest += OnResultTest;
        _nextBotton.onClick.AddListener(OnClick);

        _resultText.enabled = false;
        _resultScoreText.enabled = false;
        _nextBotton.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _resultTest.OnResultTest -= OnResultTest;
        _nextBotton.onClick.RemoveListener(OnClick);
    }

    public void DisableResultTest()
    {
        _resultText.enabled = false;
        _resultScoreText.enabled = false;
        _nextBotton.gameObject.SetActive(false);
    }

    private void OnClick()
    {
        _nextBotton.gameObject.SetActive(false);
        _resultText.enabled = false;
        _resultScoreText.enabled = false;
    }

    private void OnResultTest(ResultCheck resultCheck, int result)
    {
        _nextBotton.gameObject.SetActive(true);
        _resultText.enabled = true;
        _resultScoreText.enabled = CheckCisibleScore(result);

        switch (resultCheck)
        {
            case ResultCheck.CrtWin:
                _resultText.color = _colorWin;
                _resultText.text = "Критический успех!";
                _resultScoreText.enabled = false;
                break;

            case ResultCheck.Win:
                _resultText.color = _colorWin;
                _resultText.text = "Успех!";
                break;

            case ResultCheck.CrtLose:
                _resultText.color = _colorLose;
                _resultText.text = "Критическая неудача";
                _resultScoreText.enabled = false;
                break;

            case ResultCheck.Lose:
                _resultText.color = _colorLose;
                _resultText.text = "Неудача";
                break;
        }
       // _resultText.fontSize = Mathf.Lerp(_resultText.fontSize, 40f, Time.deltaTime);

        StartCoroutine(SizeFont());
        _resultScoreText.text = (result + 1).ToString();
    }



    private bool CheckCisibleScore(int result)
    {
        if (result < 20)
        { 
            return false;
        }

        return true;
    }

    private IEnumerator SizeFont()
    {
        float t = 2;
        float defaultFontSize = _resultText.fontSize;
        while (t > 0)
        {

            _resultText.fontSize += Time.deltaTime * 10f;
             t -= Time.deltaTime;
            yield return null;
        }
        _resultText.fontSize = defaultFontSize;
    }
}
