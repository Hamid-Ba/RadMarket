using Framework.Domain;

namespace AccountManagement.Domain.AdminRoleAgg
{
    public class AdminRole : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public AdminRole(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Edit(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
