using System.Collections.Generic;
using Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : EntityBase
    {
        public long StoreId { get; private set; }
        public string Name { get; private set; }
        public string Mobile { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public int Type { get; private set; }
        public long OwnerId { get; private set; }
        public string OwnerName { get; private set; }

        public long ParentId { get; private set; }
        public Comment Parent { get; private set; }
        public List<Comment> Children { get; private set; }


        public Comment(long storeId,string name, string mobile, string message, int type, long ownerId,string ownerName, long parentId)
        {
            StoreId = storeId;
            Name = name;
            Mobile = mobile;
            Message = message;
            Type = type;
            OwnerId = ownerId;
            OwnerName = ownerName;
            ParentId = parentId;
            IsConfirmed= false;
        }

        public void ConfirmedComment() => IsConfirmed = true;
        public void DisConfirmedComment() => IsConfirmed = false;
    }
}
