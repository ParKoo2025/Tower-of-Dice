using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public enum EDamageType
{
    Attack, CriticalAttack, Heal, Size
}

public class DamageManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;
    
    private Queue<TMP_Text> _textPool = new Queue<TMP_Text>();

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            _textPool.Enqueue(InstantiateText());
        }

        CombatManager.Instance.OnChangeHealth += OnChangeHealth;
    }

    private TMP_Text InstantiateText()
    {
        var text = Instantiate<TMP_Text>(_damageText, transform);
        text.gameObject.SetActive(false);
        return text;
    }

    private void OnChangeHealth(Transform damagedTransform, EDamageType damageType, float value)
    {
        TMP_Text text;
        if (_textPool.Count == 0)
        {
            text = InstantiateText();
        }
        else
        {
            text = _textPool.Dequeue();
        }

        text.text = $"{value}";

        switch (damageType)
        {
            case EDamageType.Attack:
                text.color = Color.red;
                break;
            case EDamageType.CriticalAttack:
                text.color = Color.yellow;
                break;
            case EDamageType.Heal:
                text.color = Color.green;
                break;
            default:
                Debug.LogError($"Unknown damage type {damageType}");
                break;
        }
        
        Vector3 randomOffset = Random.insideUnitCircle * new Vector2(1f, 5f) + new Vector2(0f, 5f); 
        text.transform.position = damagedTransform.position + randomOffset;
        text.DOFade(1f, 0f);
        text.gameObject.SetActive(true);
        
        var anim = DOTween.Sequence();
        anim.Append(text.transform.DOMoveY(10f, 1.5f).SetRelative(true));
        anim.Join(text.DOFade(0f, 1.5f));
        anim.OnComplete(() =>
        {
            text.gameObject.SetActive(false);
            _textPool.Enqueue(text);
        });

        anim.Play();
    }
}
