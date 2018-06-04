using System.ComponentModel;

namespace Template.Domain.Base
{
    public class ChildEntity : INotifyPropertyChanged
    {
        public virtual string ParentId { get; set; }

        public virtual void OnPropertyChanged(string methodName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(methodName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
