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

public class Equipment : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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

    private ERarity _rarity;
    
    private Image _raritySprite;
    [SerializeField]
    private Image _equipmentSprite;
    
    private Stat _finalStat = new Stat();
    
    private bool isEquipped = false;

    public bool IsEquipped
    {
        get { return isEquipped; }
        set { isEquipped = value; }
    }
    public ERarity Rarity
    {
        get { return _rarity; }
        set { _rarity = value; }
    }
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

    public void OnDestroyEquipment()
    {
        switch (_rarity)
        {
            case ERarity.Common:
                Debug.Log($"[Equipment] 파괴됨 (Common): {name}");
                break;
            case ERarity.Uncommon:
                Debug.Log($"[Equipment] 파괴됨 (Uncommon): {name}");
                break;
            case ERarity.Rare:
                Debug.Log($"[Equipment] 파괴됨 (Rare): {name}");
                break;
            case ERarity.Epic:
                Debug.Log($"[Equipment] 파괴됨 (Epic): {name}");
                break;
            case ERarity.Legendary:
                Debug.Log($"[Equipment] 파괴됨 (Legendary): {name}");
                break;
            case ERarity.Mythic:
                Debug.Log($"[Equipment] 파괴됨 (Mythic): {name}");
                break;
            default:
                Debug.Log($"[Equipment] 파괴됨 (Unknown): {name}");
                break;
        }

        Destroy(gameObject);
    }
    
    //장비 드래그 & 드롭

    private Vector3 DefaultPos;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (!isEquipped)
        {
            DefaultPos = transform.position;
            GetComponent<Image>().raycastTarget = false;
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!isEquipped)
        {
            Vector3 curPos = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = curPos;
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (!isEquipped)
        {
            transform.position = DefaultPos;
            GetComponent<Image>().raycastTarget = true;
        }
    }
}


