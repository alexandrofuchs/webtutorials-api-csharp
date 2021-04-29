using System.Diagnostics.CodeAnalysis;
using WebTutorialsApp.Common.Contracts;
using WebTutorialsApp.Common.Notifications;
using WebTutorialsApp.Common.ValueObjects;

namespace WebTutorialsApp.Domain.ValueObjects
{
    public class Bool : ValueObject
    {
        #region PROPERTIES
        public bool Value { get; private set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS 
        public Bool(bool? value)
        {
            Value = value ?? false;
            Verify();
        }
        #endregion CONSTRUCTORS 

        #region METHODS
        private void Verify()
        {
            AddNotifications(new Contract<Notification>()
                .Requires());
        }
        public void Alter(bool value)
        {
            Value = value;
            Verify();
        }
        #endregion METHODS
    }
}
