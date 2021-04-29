using System;
using WebTutorialsApp.Common.EntityClasses;

namespace WebTutorialsApp.Domain.Entities
{
    public class PostFile : EntityClass
    {
        #region PROPERTIES
        public byte[] Content { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }

        #endregion PROPERTIES

        #region CONSTRUCTORS
        protected PostFile() { }
        public PostFile(Guid id, DateTime createdAt, DateTime updatedAt, byte[] content, Guid postId)
            : base(id, createdAt, updatedAt)
        {
            Content = content;
            PostId = postId;
        }
        #endregion CONSTRUCTORS
    }
}
