using System;
using WebTutorialsApp.Common.Contracts;
using WebTutorialsApp.Common.Notifications;
using WebTutorialsApp.Common.ValueObjects;

namespace WebTutorialsApp.Domain.ValueObjects
{
    public class Sequence : ValueObject
    {
        #region PROPERTIES
        public int Value { get; private set; }
        #endregion PROPERTIES

        #region CONSTRUCTORS 
        public Sequence(int value)
        {
            Value = value;
            Verify();
        }
        #endregion CONSTRUCTORS 

        #region METHODS
        private void Verify()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNumber(Value, "Sequence", "The sequence must be an integer number!"));                
        }
        public void Alter(int value)
        {
            Value = value;
            Verify();
        }
        #endregion METHODS
    }
}
