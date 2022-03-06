using UnityEngine;

public class StarColorMap
{
    private static Star ZETA_PUPPIS = Star.CreateInstance(StarType.A, new Color32(136, 136, 136, 255), StarName.ZETA_PUPPIS);
    private static Star SPICA = Star.CreateInstance(StarType.A, new Color32(111, 126, 183, 255), StarName.SPICA);
    private static Star VEGA = Star.CreateInstance(StarType.A, new Color32(206, 244, 219, 255), StarName.VEGA);
    private static Star MIRFAK = Star.CreateInstance(StarType.A, new Color32(250, 252, 205, 255), StarName.MIRFAK);
    private static Star CAPELLA = Star.CreateInstance(StarType.A, new Color32(247, 249, 52, 255), StarName.CAPELLA);
    private static Star ALDEBARAN = Star.CreateInstance(StarType.A, new Color32(242, 136, 60, 255), StarName.ALDEBARAN);
    private static Star BETELGEUSE = Star.CreateInstance(StarType.A, new Color32(245, 6, 1, 255), StarName.BETELGEUSE);

    public static Star GetStarByNumberTry(int intentos)
    {
        return intentos switch
        {
            1 => ZETA_PUPPIS,
            2 => SPICA,
            3 => VEGA,
            4 => MIRFAK,
            5 => CAPELLA,
            6 => ALDEBARAN,
            7 => BETELGEUSE,
            _ => BETELGEUSE
        };
    }
}

public enum StarType
{
    O,
    B,
    A,
    F,
    G,
    K,
    M
}

public enum StarName
{
    ZETA_PUPPIS,
    SPICA,
    VEGA,
    MIRFAK,
    CAPELLA,
    ALDEBARAN,
    BETELGEUSE,
}

public class Star
{
    private StarType type;
    public Color32 color;
    private StarName name;

    private Star(StarType type, Color32 color, StarName name)
    {
        this.type = type;
        this.color = color;
        this.name = name;
    }

    public static Star CreateInstance(StarType type, Color32 color, StarName name)
    {
        return new Star(type, color, name);
    }
}