using UnityEngine;

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
    [SerializeField]
    private StatScriptable _stat;
    
    [SerializeField]
    private int _equipmentLevel = 0;
    
    [SerializeField]
    private EEquipmentType _equipmentType;
    
    [SerializeField]
    private Sprite _equipmentIcon;
    
    public StatScriptable Stat => _stat;
    public int EquipmentLevel => _equipmentLevel;
    public EEquipmentType EquipmentType => _equipmentType;
    public Sprite EquipmentIcon => _equipmentIcon;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
