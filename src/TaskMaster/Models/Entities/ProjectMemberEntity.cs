using TaskMaster.Models.Enums;

namespace TaskMaster.Models.Entities;

public class ProjectMemberEntity : Entity<Guid>
{
    public ProjectEntity Project { get; set; }
    public UserEntity User { get; set; }
    public UserRole Role { get; set; }
}