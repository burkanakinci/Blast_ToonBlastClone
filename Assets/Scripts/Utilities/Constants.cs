public class Constants
{
    public const string PLAYER_DATA = "PlayerData";
}

public enum NeighboringState
{
    OnRight = 0,
    OnLeft = 1,
    OnUp = 2,
    OnDown = 3
}
public enum ListOperation
{
    Adding,
    Subtraction
}
public class PooledObjectTags
{
    public const string Unblastable = "Unblastable";
    public const string Blastable = "Blastable";
}
public enum BlastableType
{
    BlastableBlue = 0,
    BlastableGreen = 1,
    BlastablePink = 2,
    BlastablePurple = 3,
    BlastableRed = 4,
    BlastableYellow = 5,

    Unblastable=6,
}
public enum BlastableSpriteType
{
    Level1_Blastable = 0,
    Level2_Blastable = 1,
    Level3_Blastable = 2,
    Level4_Blastable = 3
}
public enum PlayerStates
{
    Idle = 0,
    Win = 1,
    Fail = 2,
    General
}
