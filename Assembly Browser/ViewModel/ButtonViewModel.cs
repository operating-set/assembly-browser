using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using AssemblyAnalyzer;
using AssemblyAnalyzer.Exception;
using Container = AssemblyAnalyzer.Containers.Container;

namespace Assembly_Browser.ViewModel
{
    class ButtonViewModel : INotifyPropertyChanged
    {
        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                OnPropertyChanged("FileName");
            }
        }

        public ButtonCommand ButtonCommand { get; set; }
        public ButtonViewModel()
        {
            ButtonCommand = new ButtonCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void OnExecute()
        {

            var fileDialog = new OpenFileDialog
            {
                //Filter = "Assemblies|*.dll;*.exe",
                Title = "Select assembly",
                Multiselect = false
            };

            var isOpen = fileDialog.ShowDialog();

            if (isOpen == null)
            {
                FileName = "File hasn't been chosen.";
                return;
            }

            if (isOpen.Value)
            {
                FileName = fileDialog.FileName;
                CreateTree(FileName);
            }
        }

        private List<Container> _namespaces;
        public List<Container> Namespaces
        {
            get
            {
                return _namespaces;
            }
            set
            {
                _namespaces = value;
                OnPropertyChanged(nameof(Namespaces));
            }
        }
        private readonly AssemblyBrowser assemblyBrowser = new AssemblyBrowser();

        private void CreateTree(string FileName)
        {
            Namespaces = null; 
            try
            {
                Namespaces = assemblyBrowser.GetAssemblyInfo(FileName);
            }

            catch (Exception e)
            {
                MessageBox.Show("");
                return;
            }

            OnPropertyChanged("Signature");
            OnPropertyChanged("Members");
            OnPropertyChanged(nameof(Namespaces));
            MessageBox.Show("Managed");

            //OnPropertyChanged("Signature");
        }

    }
}
