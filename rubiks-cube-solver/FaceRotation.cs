namespace RubiksCubeSolver;

enum FaceRotationModifier : byte
{
    Clockwise = 0,
    AntiClockwise = 12,
    Double = 6,
}

enum FaceRotation : byte
{
    R = 1,
    L = 2,
    U = 3,
    D = 4,
    F = 5,
    B = 6,
    RPrime = FaceRotationModifier.AntiClockwise + R,
    LPrime = FaceRotationModifier.AntiClockwise + L,
    UPrime = FaceRotationModifier.AntiClockwise + U,
    DPrime = FaceRotationModifier.AntiClockwise + D,
    FPrime = FaceRotationModifier.AntiClockwise + F,
    BPrime = FaceRotationModifier.AntiClockwise + B,
    R2 = FaceRotationModifier.Double + R,
    L2 = FaceRotationModifier.Double + L,
    U2 = FaceRotationModifier.Double + U,
    D2 = FaceRotationModifier.Double + D,
    F2 = FaceRotationModifier.Double + F,
    B2 = FaceRotationModifier.Double + B,
}
