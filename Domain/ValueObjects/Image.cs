using WebTutorialsApp.Common.Contracts;
using WebTutorialsApp.Common.Notifications;
using WebTutorialsApp.Common.ValueObjects;

namespace WebTutorialsApp.Domain.ValueObjects
{
    public class Image : ValueObject
    {
        #region PROPERTIES
        public byte[] Value { get; private set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS 
        public Image(byte[] value)
        {
            Value = value ?? null;
            Verify();
        }
        #endregion CONSTRUCTORS 

        #region METHODS
        private void Verify()
        {
            AddNotifications(new Contract<Notification>()
                .Requires());
        }
        public void Alter(byte[] value)
        {
            Value = value;
            Verify();
        }
        #endregion METHODS
    }
}
