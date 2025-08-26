using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EEquipmentType
{
    Weapon,
    Helmet,
    Armor,
    Boots,
    Size
}

public class Equipment : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TextMeshPro _equipmentLevelTMP;
    
    [Header("Data")]
    [SerializeField]
    private StatScriptable _stat;
    [SerializeField]
    private int _equipmentLevel;
    [SerializeField]
    private EEquipmentType _equipmentType;
    [SerializeField]
    private Sprite _equipmentIcon;
    
    private Stat _finalStat = new Stat();
    
    public StatScriptable Stat => _stat;
    public int EquipmentLevel => _equipmentLevel;
    public EEquipmentType EquipmentType => _equipmentType;
    public Sprite EquipmentIcon => _equipmentIcon;
    public Stat FinalStat => _finalStat;

    private void Awake()
    {
        int curFloor = GameManager.Instance.CurFloor;
        SetLevel(curFloor);
        ApplyFloorPenalty();
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = _equipmentIcon;
    }
    
    public void ApplyFloorPenalty()
    {
        int curFloor = GameManager.Instance.CurFloor;
        
        float penaltyRate = 1f - ((curFloor - _equipmentLevel) * 0.1f);

        if (penaltyRate < 0f) penaltyRate = 0f; // 최소 0

        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            _finalStat[(EStatType)i] = _stat[(EStatType)i] * penaltyRate;
        }
    }

    public void SetLevel(int level)
    {
        _equipmentLevel = level;
        RefreshTMP();
    }

    private void RefreshTMP()
    {
        _equipmentLevelTMP.text = _equipmentLevel.ToString();
    }
    
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            var player = FindObjectOfType<PlayerStat>();
            if (player == null) return;

            player.SetEquipment(this);

            // 수정 필요
            var go = GameObject.Find("WeaponLoc");
            var t = go.transform;
            transform.SetParent(t, worldPositionStays: false);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
