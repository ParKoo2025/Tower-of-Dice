public interface IPassive
{
    public string Name { get; }
    /// <summary>
    /// 패시브를 얻었을 때 할 행동 구현
    /// </summary>
    public void Activate(Combatant owner);
    
    /// <summary>
    /// 패시브가 사라졌을 때 할 행동 구현
    /// </summary>
    public void Deactivate();
}