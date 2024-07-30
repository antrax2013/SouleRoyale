using Xunit.Gherkin.Quick;
using NFluent;

namespace SouleRoyale.Tests.TestCases;

[FeatureFile(@"./Features/Soule.feature")]
public sealed class SouleTests : Feature
{
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
    private Soule _soule;
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

    [Given(@"I ask to create a new soule")]
    public void I_ask_to_create_a_new_soule() { }

    [When(@"I create a new soule")]
    public void I_create_a_new_soule()
    {
        _soule = new Soule();
    }

    [And(@"The soule position is 0")]
    public void The_soule_position_is_0()
    {
        Check.That(_soule.Position).IsEqualTo(0);
    }
}
