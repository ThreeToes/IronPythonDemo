using System;
using System.ComponentModel;
using System.Windows;
using PythonDemo.Models;
using PythonDemo.Scripting;

namespace PythonDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Currently selected contact
        /// </summary>
        public Contact CurrentlySelected
        {
            get { return _currentlySelected; }
            set
            {
                _currentlySelected = value;
                _callbackManager.FireEvent("ActiveContactChanged", _currentlySelected);
                OnPropertyChanged("CurrentlySelected");
            }
        }

        private readonly CallbackManager _callbackManager;
        private readonly ScriptingManager _scriptingManager;
        private Contact _currentlySelected;
        
        /// <summary>
        /// Contact manager logic
        /// </summary>
        public ContactManager ContactManager { get; private set; }
        
        /// <summary>
        /// Logic
        /// </summary>
        public MainWindow()
        {
            _callbackManager = new CallbackManager();
            ContactManager = new ContactManager(_callbackManager);
            _scriptingManager = new ScriptingManager(_callbackManager, ContactManager);
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Called when the add contact button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddContactClick(object sender, RoutedEventArgs e)
        {
            ContactManager.Contacts.Add(new Contact()
                                            {
                                                FirstName = "New Contact"
                                            });
        }

        /// <summary>
        /// Called when the remove contact button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveContact(object sender, RoutedEventArgs e)
        {
            if(CurrentlySelected == null)return;
            ContactManager.Contacts.Remove(CurrentlySelected);
        }

        /// <summary>
        /// Called when the run script button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunScript(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
                          {
                              FileName = "script", 
                              DefaultExt = ".py", 
                              Filter = "Python Scripts (.py)|*.py"
                          };

            // Show open file dialog box 
            var result = dialog.ShowDialog();

            if (result == true)
            {
                // Open document 
                string filename = dialog.FileName;
                //Run the script
                _scriptingManager.RunFile(filename);
            }
        }
    }
}
