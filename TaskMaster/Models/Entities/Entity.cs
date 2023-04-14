using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Models.Entities;

public class Entity<TKey>
{
    [Key]
    public TKey Id { get; set; }
}