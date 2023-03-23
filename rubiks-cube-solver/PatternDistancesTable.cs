namespace RubiksCubeSolver;

internal sealed class PatternDistancesTable
{
    // Each distance value is stored as 4 bits
    private readonly byte[] distances;

    public PatternDistancesTable(string filename)
    {
        distances = File.ReadAllBytes(filename);
    }

    // They're functionally the same, trust me 👍
    //public int GetDistance(int patternIndex) =>
    //    (patternIndex % 2 == 0)
    //        ? distances[patternIndex / 2] & 0xf
    //        : distances[patternIndex / 2] >> 4;
    public int GetDistance(int patternIndex) =>
        (distances[patternIndex >> 1] >> ((patternIndex & 0x1) << 2)) & 0xf;
}
