public class Define
{
    public enum KeyEvent
    {
        Left,
        Right,
        Up,
        Down,
        ESC,
        Tab,
        Enter,
        Debug
    }

    public enum Layers 
    {
        Default,
        BackGround,
        Player_Back,
        Fence,
        Building,
        Player_Front,
        Reserve
    }

    public enum TimeOfDay 
    {
        Morning,
        Day,
        Night
    }

    public enum UpgradeType { 
        CrossBow = 0,
        Staff = 1,
        Garlic = 2,
        Unity = 3,
        Hp = 4,
        Speed = 5,
    }

    /// <summary>
    /// 언젠간 필요하겠지...?
    /// </summary>
    public enum MonsterType
    {

    }
}
