using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Sydy.Gambling.Football.MVVM
{
    public abstract class ObservableObject : INotifyPropertyChanged, ISerializable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual T GetValue<T>(ref T field, [CallerMemberName] string? propertyName = null) => field;

        protected virtual bool SetValue<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value) is bool boolean && !boolean)
            {
                field = value;
                OnPropertyChanged(propertyName);
            }

            return !boolean;
        }

        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            GetObjectData(info, context);
        }

        private void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
    }
}
