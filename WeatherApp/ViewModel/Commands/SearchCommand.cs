﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {

        public WeatherViewModel WeatherVM { get; set; }

        public event EventHandler CanExecuteChanged;

        public SearchCommand(WeatherViewModel vm)
        {
            WeatherVM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            WeatherVM.MakeQuery();
        }
    }
}
