using System.Collections.Generic;
using Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : EntityBase
    {
        public long StoreId { get; private set; }
        public string Name { get; private set; }
        public string Mobile { get; private set; }
        public int Score { get; private set; }
        public int Type { get; private set; }
        public long OwnerId { get; private set; }
        public string OwnerName { get; private set; }

        public Comment(long storeId,string name, string mobile, int score, int type, long ownerId,string ownerName)
        {
            StoreId = storeId;
            Name = name;
            Mobile = mobile;
            Score = score;
            Type = type;
            OwnerId = ownerId;
            OwnerName = ownerName;
        }
    }
}
