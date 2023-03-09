using System.Runtime.CompilerServices;
using VerifyTests;

namespace FeatureSourceGenerators.IntegrationTests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Enable();
    }
}