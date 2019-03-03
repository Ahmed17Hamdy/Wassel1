
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Wassel.Models;
using Wassel.Services;

namespace Wassel.ViewModels
{
    public class CarsViewModel : INotifyPropertyChanged

    {
        public CarsViewModel()
        {
            CartypeGetter();
           // CarGetter();
        }
        private ObservableCollection<Car> _cars;
        public ObservableCollection<Car> Cars
        {
            get
            {
                return _cars;
            }
            set
            {
                _cars = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Cartype> _cartypes;
        public ObservableCollection<Cartype> CarTypes
        {
            get
            {
                return _cartypes;
            }
            set
            {
                _cartypes = value;
                OnPropertyChanged();
            }
        }
        public async Task CartypeGetter()
        {
            CarServices serv = new CarServices();
            var ResBack = await serv.GetCarstype();
            foreach (var modelitem in ResBack)
            {

                var baramIcon = modelitem.icon;
                modelitem.icon = "http://waselksa.alsalil.net/users/images/" + baramIcon;
            }
            CarTypes = ResBack;
        }

        public async Task CarGetter()
        {
            CarServices serv = new CarServices();
            var ResBack = await serv.GetAllCars();
            Cars = ResBack;

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
