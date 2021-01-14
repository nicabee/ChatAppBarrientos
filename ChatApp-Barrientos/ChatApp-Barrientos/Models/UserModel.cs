﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;


namespace ChatApp_Barrientos.Models
{
    public class UserModel : INotifyPropertyChanged
    {

        string _uid { get; set; }
        public string uid { get { return _uid; } set { _uid = value; OnPropertyChanged(nameof(uid)); } }
        string _email { get; set; }
        public string email { get { return _email; } set { _email = value; OnPropertyChanged(nameof(email)); } }

        string _name { get; set; }
        public string name { get { return _name; } set { _name = value; OnPropertyChanged(nameof(name)); } }


        int _userType { get; set; }
        public int userType { get { return _userType; } set { _userType = value; OnPropertyChanged(nameof(userType)); } }

        string _created_at { get; set; }
        public string created_at { get { return _created_at; } set { _created_at = value; OnPropertyChanged(nameof(created_at)); } }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        List<string> _contacts { get; set; }

        public List<string> contacts { get { return _contacts; } set { _contacts = value; OnPropertyChanged(nameof(contacts)); } }

    }
}
