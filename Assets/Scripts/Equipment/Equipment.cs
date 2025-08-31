using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

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
    private TextMeshProUGUI _equipmentLevelTMP;
    
    [Header("Data")]
    [SerializeField]
    private StatScriptable _stat;
    [SerializeField]
    private int _equipmentLevel;
    [SerializeField]
    private EEquipmentType _equipmentType;
    
    private Image _raritySprite;
    [SerializeField]
    private Image _equipmentSprite;
    
    private Stat _finalStat = new Stat();

    public Sprite RaritySprite
    {
        get { return _raritySprite.sprite; }
        set { _raritySprite.sprite = value; }
    }

    public Sprite EquipmentSprite
    {
        get { return _equipmentSprite.sprite; }
        set { _equipmentSprite.sprite = value; }
    }

    public EEquipmentType EquipmentType
    {
        get { return _equipmentType; }
        set { _equipmentType = value; }
    }

    public StatScriptable Stat
    {
        get { return _stat; }
        set { _stat = value; }
    }
    public int EquipmentLevel => _equipmentLevel;
    public Stat FinalStat => _finalStat;

    private void Awake()
    {
        _raritySprite = GetComponent<Image>();
    }

    private void Start()
    {
        int curFloor = GameManager.Instance.CurFloor;
        SetLevel(curFloor);
        ApplyFloorPenalty();
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
        _equipmentLevelTMP.text = _equipmentLevel.ToString();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData == null) return;

        // 우클릭만 반응
        if (eventData.button != PointerEventData.InputButton.Right) return;

        // UI 위 클릭 필터는 EventSystem이 이미 처리하므로 별도 체크 불필요
        var player = FindObjectOfType<PlayerStat>();
        if (player == null) return;

        player.SetEquipment(this);

        // WeaponLoc로 붙이기 (UI 슬롯 가정)
        var go = GameObject.Find("WeaponLoc");
        if (go == null)
        {
            Debug.LogWarning("[Equipment] WeaponLoc not found.");
            return;
        }

        var t = go.transform;
        transform.SetParent(t, worldPositionStays: false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}


