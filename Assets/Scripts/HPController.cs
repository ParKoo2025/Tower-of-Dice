using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [SerializeField] private Transform _hpUp;
    [SerializeField] private Transform _hpDown;

    public void SetHealth(float currentHP, float totalHP)
    {
        _hpUp.DOScaleX(Mathf.Clamp01(currentHP / totalHP), 0.1f);
    }

    public void SetAttackSpeed(float currentSpeed, float totalSpeed)
    {
        _hpDown.DOScaleX(1f - Mathf.Clamp01(currentSpeed / totalSpeed), 0f);
    }

}
