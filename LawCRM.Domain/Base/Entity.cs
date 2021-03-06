﻿using System.ComponentModel;

namespace Template.Domain.Base
{
    public class Entity : INotifyPropertyChanged
    {
        public virtual string Id { get; set; }

        public virtual void OnPropertyChanged(string methodName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(methodName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
