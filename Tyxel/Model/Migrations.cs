namespace Tyxel.Model;

internal static class Migrations
{
    static IReadOnlyList<Action<ProjectConfig>> MigrationActions { get; } = new Action<ProjectConfig>[]
    {
        data => data.Version = 1
    };

    public static int CurrentVersion => MigrationActions.Count;

    public static void ApplyMigrations(this ProjectConfig cfg)
    {
        while (cfg.Version < CurrentVersion)
            MigrationActions[cfg.Version](cfg);
    }
}
