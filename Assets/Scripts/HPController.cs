using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [SerializeField] private Image _hpUp;
    [SerializeField] private Image _hpDown;
    
    private float _totalHP;
    private float _currentHP;
    
    public void SetHP(float currentHP, float totalHP)
    {
        _totalHP = totalHP;
        _currentHP = currentHP;

        _hpUp.fillAmount = _currentHP / _totalHP;
    }

}
