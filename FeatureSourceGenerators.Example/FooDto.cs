using FluentValidation;

namespace FeatureSourceGenerators.Example;

public sealed partial record FooDto(
    int Id,
    string Name
)
{
    private static void AddValidation(AbstractValidator<FooDto> v)
    {
        v.RuleFor(e => e.Id).NotEmpty();
        v.RuleFor(e => e.Name).NotEmpty();
    }
}