using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Models.Entities;

public class Entity<TKey> : Entity
{
    [Key]
    public TKey Id { get; set; }
}

public class Entity
{
}