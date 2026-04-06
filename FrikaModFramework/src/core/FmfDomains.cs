namespace FrikaMF.Core;

/// <summary>
/// Logical domains for FMF hook names: <c>FMF.&lt;Domain&gt;.&lt;Event&gt;</c>.
/// Align with <see cref="FmfHookName"/> and <c>FrikaModFramework/fmf_hooks.json</c>.
/// </summary>
public enum FmfDomain
{
    Gameplay,
    Player,
    Employee,
    Customer,
    Server,
    Rack,
    Network,
    Power,
    Ui,
    System
}
