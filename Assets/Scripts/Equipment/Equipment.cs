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
    
    public StatScriptable Stat => _stat;
    public int EquipmentLevel => _equipmentLevel;
    public EEquipmentType EquipmentType => _equipmentType;
    public Sprite EquipmentIcon => _equipmentIcon;

    private void Awake()
    {
        int curFloor = GameManager.Instance.CurFloor;
        SetLevel(curFloor);
        var sr = GetComponent<SpriteRenderer>();
        sr.sprite = _equipmentIcon;
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
                return; // UI 위 클릭 무시(선택)

            var player = FindObjectOfType<PlayerStat>();
            if (player == null) return;

            player.SetEquipment(this);

            var go = GameObject.Find("WeaponLoc");
            var t = go.transform;
            transform.SetParent(t, worldPositionStays: false);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
