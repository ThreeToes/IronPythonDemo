using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using PythonDemo.Scripting;

namespace PythonDemo.Models
{
    public class ContactManager : INotifyPropertyChanged
    {
        private ObservableCollection<Contact> _contacts;
        private CallbackManager _callbacks;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="callbacks">Callback manager to use</param>
        public ContactManager(CallbackManager callbacks)
        {
            Contacts = new ObservableCollection<Contact>();
            _callbacks = callbacks;
        }

        /// <summary>
        /// List of contacts
        /// </summary>
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

        /// <summary>
        /// Called when the contacts list changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Only really here to fire events on our callback manager
        /// </remarks>
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
