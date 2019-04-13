namespace DbUp.Migration
{
    public interface IDatabaseConnectionSettings
    {
        string Host { get; }

        uint Port { get; }

        string Name { get; }

        string User { get; }

        string Password { get; }

        bool Pooling { get; }
    }
}
