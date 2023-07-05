using Dybal.Utils.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Dybal.Utils.Extensions.DependencyInjection.Services;

[RegisterTransientService]
[RegisterTransientService<ITestTransientService>]
public class TestTransientService : ITestTransientService
{
}