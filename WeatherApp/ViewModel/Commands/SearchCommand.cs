using System;
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

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SearchCommand(WeatherViewModel vm)
        {
            WeatherVM = vm;
        }

        /// <summary>
        /// evaluate parameter so that search only executes when query is not empty
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            string query = parameter as string;

            if (string.IsNullOrWhiteSpace(query))
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            WeatherVM.MakeQuery();
        }
    }
}
