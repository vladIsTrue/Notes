using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Notes.Model 
{
    class dataModel : INotifyPropertyChanged
    {
        public dataModel() { }
        public dataModel(string name, string content) 
        {
            _nameNote= name;
            _contentNote= content;
        }

        private string _nameNote;
        private string _contentNote;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string NameNote
        {
            get { return _nameNote; }
            set 
            {
                if (_nameNote == value)
                    return;
                _nameNote = value;
                OnPropertyChanged("NameNote");
            }
        }
        public string ContentNote
        {
            get { return _contentNote; }
            set 
            {
                if (_contentNote == value)
                    return;
                _contentNote = value;
                OnPropertyChanged("ContenNote");
            }
        }
    }
}
