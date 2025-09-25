using UnityEngine;

public class GetAllStatPassive : IPassive
{
    private float _amount = 10f;
    private Combatant _owner;
    
    public string Name { get; } = "GetAllStatPassive";
    
    public void Activate(Combatant owner)
    {
        _owner = owner;

        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            var stat = (EStatType)i;
            if (stat == EStatType.ATK_SPD)
            {
                _owner.AddStat(stat, _amount / 100);
            }
            else
            {
                _owner.AddStat(stat, _amount);
            }
        }
        Debug.Log(Name);
    }

    public void Deactivate()
    {
        for (int i = 0; i < (int)EStatType.Size; i++)
        {
            var stat = (EStatType)i;
            if (stat == EStatType.ATK_SPD)
            {
                _owner.AddStat(stat, -_amount / 100);
            }
            else
            {
                _owner.AddStat(stat, -_amount);
            }
        }
    }
}
