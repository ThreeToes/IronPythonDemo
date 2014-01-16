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
        private ScriptingManager _scriptingManager;
        private Contact _currentlySelected;
        public ContactManager ContactManager { get; private set; }
        public MainWindow()
        {
            _callbackManager = new CallbackManager();
            _scriptingManager = new ScriptingManager(_callbackManager);
            ContactManager = new ContactManager(_callbackManager);
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddContactClick(object sender, RoutedEventArgs e)
        {
            ContactManager.Contacts.Add(new Contact()
                                            {
                                                FirstName = "New Contact"
                                            });
        }

        private void RemoveContact(object sender, RoutedEventArgs e)
        {
            if(CurrentlySelected == null)return;
            ContactManager.Contacts.Remove(CurrentlySelected);
        }

        private void RunScript(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "script"; // Default file name 
            dlg.DefaultExt = ".py"; // Default file extension 
            dlg.Filter = "Python Scripts (.py)|*.py"; // Filter files by extension 

            // Show open file dialog box 
            var result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                _scriptingManager.RunFile(filename);
            }
        }
    }
}
