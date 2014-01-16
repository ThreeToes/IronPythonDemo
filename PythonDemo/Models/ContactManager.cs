using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PythonDemo.Scripting;

namespace PythonDemo.Models
{
    public class ContactManager : INotifyPropertyChanged
    {
        private ObservableCollection<Contact> _contacts;
        private CallbackManager _callbacks;

        public ContactManager(CallbackManager callbacks)
        {
            Contacts = new ObservableCollection<Contact>();
            _callbacks = callbacks;
        }

        public ObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                _contacts.CollectionChanged += ContactsOnCollectionChanged;
                OnPropertyChanged("Contacts");
            }
        }

        private void ContactsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    _callbacks.FireEvent("ContactAdded", newItem);
                }
            }else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldItem in e.OldItems)
                {
                    _callbacks.FireEvent("ContactRemoved", oldItem);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
