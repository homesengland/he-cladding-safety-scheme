using AutoFixture;

namespace HE.Remediation.WebApp.Tests;

public abstract class TestBase
{
    protected Fixture Fixture;

    protected TestBase()
        : this(new Fixture())
    {
    }

    protected TestBase(Fixture fixture)
    {
        Fixture = fixture;
    }
}