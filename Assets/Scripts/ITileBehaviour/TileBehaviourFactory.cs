using UnityEngine;

public static class TileBehaviourFactory
{
    public static ITileBehaviour Create(ETileStyle eTileStyle, ETileType eTileType)
    {
        if (eTileStyle == ETileStyle.C)
        {
            return CreateCTile(eTileType);
        }
        else
        {
            return CreateTTile(eTileType);
        }
    }

    private static ITileBehaviour CreateCTile(ETileType eTileType)
    {
        return null;
    }

    private static ITileBehaviour CreateTTile(ETileType eTileType)
    {
        ITileBehaviour ret = null;
        switch (eTileType)
        {
            case ETileType.Basic:
                ret = new BasicTileBehaviour();
                break;
            case ETileType.Passive:
                ret = new PassiveTileBehaviour();
                break;
            case ETileType.Roulette:
                ret = new RouletteTileBehaviour();
                break;
            case ETileType.Ladder:
                ret = new LadderTileBehaviour();
                break;
            case ETileType.Toss:
                ret = new TossTileBehaviour();
                break;
            case ETileType.Treasure:
                ret = new TreasureTileBehaviour();
                break;
            case ETileType.Battle:
                ret = new BattleTileBehaviour();
                break;
            default:
                Debug.LogError("UnDefined Behaviour");
                break;
        }

        return ret;
    }
    
}