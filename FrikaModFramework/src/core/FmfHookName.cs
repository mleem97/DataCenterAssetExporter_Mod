using System;
using System.Text;

namespace FrikaMF.Core;

/// <summary>
/// Builds canonical FMF hook strings: <c>FMF.&lt;DOMAIN&gt;.&lt;Action&gt;</c> with optional subject suffix.
/// </summary>
public static class FmfHookName
{
    private const string Prefix = "FMF";

    /// <summary>
    /// Creates a hook name from domain and PascalCase action, e.g. <c>FMF.GAMEPLAY.JobCompleted</c>.
    /// </summary>
    public static string Create(FmfDomain domain, string action)
    {
        return Create(domain, action, null);
    }

    public static string Create(FmfDomain domain, string action, string subject)
    {
        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentException("Action is required.", nameof(action));

        var domainPart = DomainToSegment(domain);
        var sb = new StringBuilder(Prefix.Length + 1 + domainPart.Length + 1 + action.Trim().Length + 32);
        sb.Append(Prefix).Append('.').Append(domainPart).Append('.').Append(action.Trim());

        if (!string.IsNullOrWhiteSpace(subject))
            sb.Append('.').Append(subject.Trim());

        return sb.ToString();
    }

    private static string DomainToSegment(FmfDomain domain)
    {
        return domain switch
        {
            FmfDomain.Gameplay => "GAMEPLAY",
            FmfDomain.Player => "PLAYER",
            FmfDomain.Employee => "EMPLOYEE",
            FmfDomain.Customer => "CUSTOMER",
            FmfDomain.Server => "SERVER",
            FmfDomain.Rack => "RACK",
            FmfDomain.Network => "NETWORK",
            FmfDomain.Power => "POWER",
            FmfDomain.Ui => "UI",
            FmfDomain.System => "SYSTEM",
            _ => throw new ArgumentOutOfRangeException(nameof(domain), domain, null)
        };
    }
}
