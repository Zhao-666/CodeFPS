public enum Env
{
    PC,
    Phone
}

public static class GameConfig
{
    //游戏运行环境配置
    public static readonly Env Environment = Env.Phone;
}