﻿namespace KidsWallet.Persistence;

public sealed record EfCoreSettings
{
    public string? Host { get; init; }
    
    public string? Port { get; init; }
    
    public string? DefaultDatabase { get; init; }
    
    public string? Database { get; init; }
    
    public string? User { get; init; }
    
    public string? Password { get; init; }
    
    public string ConnectionString =>
        $"Server={Host};Port={Port};Database={Database};User Id={User};Password={Password}";
    
    public string DefaultConnectionString =>
        $"Server={Host};Port={Port};Database={DefaultDatabase};User Id={User};Password={Password}";
}