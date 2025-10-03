namespace Chess;

public record StockfishGameInstanceConf
{
    public int Strength { get; set; }
    public Color PlayingAs { get; set; }

    public StockfishGameInstanceConf(int strength, Color playingAs)
    {
        if ((strength >= 0 && strength <= 20) || (strength >= 1320 && strength <= 4000))
        {
            Strength = strength;
            PlayingAs = playingAs;
        }
        else
        {
            throw new Exception($"Attempted to create stockfish info with invalid strength: {strength}");
        }
    }
}
