using System.Collections.Immutable;

namespace RubiksCubeSolver;

enum FaceRotationModifier : byte
{
    Clockwise = 8,
    Double = 16,
    AntiClockwise = 24,
}

enum FaceRotation : byte
{
    R = FaceRotationModifier.Clockwise,
    L,
    U,
    D,
    F,
    B,
    R2 = FaceRotationModifier.Double,
    L2,
    U2,
    D2,
    F2,
    B2,
    RPrime = FaceRotationModifier.AntiClockwise,
    LPrime,
    UPrime,
    DPrime,
    FPrime,
    BPrime,
}

static class FaceRotationExtensions
{
    public static readonly ImmutableArray<FaceRotation> Rotations = (
        (FaceRotation[])Enum.GetValues(typeof(FaceRotation))
    ).ToImmutableArray();

    public static FaceRotation ReverseRotation(this FaceRotation fr) =>
        (FaceRotation)((32 - ((int)fr & 0x18)) | ((int)fr & 0x7));
}
