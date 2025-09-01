using UnityEngine;
using System.Collections.Generic;
using System;

public enum ERarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythic,
    Size
}

public enum EEquipments
{
    ATK_Damage_Weapon,
    CRT_Damage_Weapon,
    Size
}

[Serializable]
public struct WeightedRarity
{
    public ERarity Rarity;
    [Range(0f, 100f)] public float Weight; 
}

[Serializable]
public struct WeightedSubKind
{
    [Tooltip("서브타입 식별자 (예: Sword, Axe, Bow, Staff …)")]
    public EEquipments Id;
    [Range(0f, 100f)] public float Weight; 
}

[Serializable]
public struct EquipmentTypeConfig
{
    public EEquipmentType Type;

    [Tooltip("이 장비 타입 자체의 등장 확률 가중치")]
    [Range(0f, 100f)] public float TypeWeight; 

    [Tooltip("이 타입 안에서 뽑을 하위 종(보통 4개 권장)")]
    public List<WeightedSubKind> SubKinds;
}

public class EquipmentManager : SingletonBehavior<EquipmentManager>
{
    private EquipmentFactory _factory;

    private Dictionary<EEquipments, StatScriptable> _equipmentStats;

    [Header("레어도")]
    [SerializeField] private WeightedRarity[] _weightedRarity;
    
    [Header("장비 종류")]
    [SerializeField] private EquipmentTypeConfig[] _typeConfigs;
    
    [SerializeField] private Canvas _canvas;

    private void Start()
    {
        _factory = GetComponent<EquipmentFactory>();
        _equipmentStats = new Dictionary<EEquipments, StatScriptable>();
        for (int i = 0; i < (int)EEquipments.Size; ++i)
        {
            var stat = Resources.Load<StatScriptable>($"Stat/Equipments/{((EEquipments)i).ToString()}");
            _equipmentStats[(EEquipments)i] = stat;
        }
        
        Debug.Log("Awake완료-------------------------------------------");
    }
    
    public Equipment SpawnRandom()
    {
        Debug.Log("SpawnRandom 시작");
        ERarity rarity = RollRarity();
        EEquipmentType type = RollType(out var typeCfg);
        EEquipments subKind = RollSubKind(typeCfg);

        var eq = _factory.GetEquipment(rarity, subKind);
        if (eq == null)
        {
            Debug.LogError($"[EquipmentManager] Factory가 장비 생성에 실패: rarity={rarity}, subKind={subKind}. " +
                           $"프리팹 매핑과 Equipment 컴포넌트 부착 여부를 확인하세요.");
            return null;
        }
        eq.EquipmentType = type;
        eq.Stat = _equipmentStats[subKind];
        eq.Rarity = rarity;
        
        eq.transform.SetParent(_canvas.transform, false);
        return eq;
    }
    
    private ERarity RollRarity()
    {
        if (_weightedRarity == null || _weightedRarity.Length == 0)
        {
            Debug.LogWarning("[EquipmentManager] _weightedRarity 비어있음. 기본값 Common 사용.");
            return ERarity.Common;
        }

        float total = 0f;
        for (int i = 0; i < _weightedRarity.Length; i++)
            total += Mathf.Max(0f, _weightedRarity[i].Weight);

        if (total <= 0f) return _weightedRarity[0].Rarity;

        float r = UnityEngine.Random.Range(0f, total);
        float cum = 0f;
        for (int i = 0; i < _weightedRarity.Length; i++)
        {
            cum += Mathf.Max(0f, _weightedRarity[i].Weight);
            if (r < cum)
                return _weightedRarity[i].Rarity;
        }
        return _weightedRarity[_weightedRarity.Length - 1].Rarity;
    }
    
    private EEquipmentType RollType(out EquipmentTypeConfig pickedConfig)
    {
        pickedConfig = default;

        if (_typeConfigs == null || _typeConfigs.Length == 0)
        {
            Debug.LogWarning("[EquipmentManager] _typeConfigs 비어있음. 기본값 반환.");
            return default;
        }

        float total = 0f;
        for (int i = 0; i < _typeConfigs.Length; i++)
            total += Mathf.Max(0f, _typeConfigs[i].TypeWeight);

        if (total <= 0f)
        {
            pickedConfig = _typeConfigs[0];
            return pickedConfig.Type;
        }

        float r = UnityEngine.Random.Range(0f, total);
        float cum = 0f;
        for (int i = 0; i < _typeConfigs.Length; i++)
        {
            cum += Mathf.Max(0f, _typeConfigs[i].TypeWeight);
            if (r < cum)
            {
                pickedConfig = _typeConfigs[i];
                return pickedConfig.Type;
            }
        }

        pickedConfig = _typeConfigs[_typeConfigs.Length - 1];
        return pickedConfig.Type;
    }
    
    private EEquipments RollSubKind(EquipmentTypeConfig typeCfg)
    {
        if (typeCfg.SubKinds == null || typeCfg.SubKinds.Count == 0)
        {
            Debug.LogWarning($"[EquipmentManager] {typeCfg.Type} 의 SubKinds 비어있음.");
            return default;
        }

        float total = 0f;
        for (int i = 0; i < typeCfg.SubKinds.Count; i++)
            total += Mathf.Max(0f, typeCfg.SubKinds[i].Weight);

        if (total <= 0f)
            return typeCfg.SubKinds[0].Id;

        float r = UnityEngine.Random.Range(0f, total);
        float cum = 0f;
        for (int i = 0; i < typeCfg.SubKinds.Count; i++)
        {
            cum += Mathf.Max(0f, typeCfg.SubKinds[i].Weight);
            if (r < cum)
                return typeCfg.SubKinds[i].Id;
        }
        return typeCfg.SubKinds[typeCfg.SubKinds.Count - 1].Id;
    }
}
