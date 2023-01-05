using System;

namespace CarRental.Infrastructure;

public abstract record Entity
{
    public Guid Id { get; set; }
    
    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}