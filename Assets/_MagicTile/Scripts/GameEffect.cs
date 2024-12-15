using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem _completeLevel;
    [SerializeField] ParticleSystem _getCombo;
    [SerializeField] TextMeshProUGUI _comboText;
    [SerializeField] List<string> comboTexts;
    public void PlayCompleteLevelVFX()
    {
        _completeLevel.Play();
    }
    public void StopCompleteLevelVFX()
    {
        _completeLevel.Stop();
    }
    public async void OnGetScoreEffect(bool isPerfect)
    {
        _getCombo.Stop();
        _getCombo.Play();

        float randomRotation = Random.Range(-20, 20);
        _comboText.rectTransform.rotation = Quaternion.Euler(0, 0, randomRotation);

        if (isPerfect)
        {
            _comboText.text = "PERFECT !";
        }
        else
        {
            _comboText.text = "GOOD !";
        }
        //_comboText.text = comboTexts[Random.Range(0, comboTexts.Count)];
        await Task.Delay(1000);
        _comboText.text = "";
    }
}
