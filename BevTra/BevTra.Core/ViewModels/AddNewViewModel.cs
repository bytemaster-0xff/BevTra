using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevTra.Core.ViewModels
{
    public class AddNewViewModel : BevTraViewModel
    {
        public AddNewViewModel()
        {
            SaveCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() => Save());
            CancelCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() => Cancel());

            SetSmallCommand = new RelayCommand(() => Ounces = 8);
            SetMediumCommand = new RelayCommand(() => Ounces = 12);
            SetLargeCommand = new RelayCommand(() => Ounces = 16);



            NewFluid = new Models.Fluid();
        }

        public Models.Fluid NewFluid
        {
            get; private set;
        }

        public async void Save()
        {
            if (await PerformNetworkAction(() =>
             {

             }))
                Navigation.GoBack();
                
        }

        public void Cancel()
        {

        }

        public int Ounces
        {
            get { return NewFluid.Ounces; }
            set
            {
                NewFluid.Ounces = value;
                RaisePropertyChanged(() => Ounces);
            }
        }

        public String BeverageType
        {
            get { return NewFluid.Contents; }
            set
            {
                BeverageType = value;
                RaisePropertyChanged(() => BeverageType);
            }
        }

        public RelayCommand SetSmallCommand { get; private set; }
        public RelayCommand SetMediumCommand { get; private set; }
        public RelayCommand SetLargeCommand { get; private set; }

        public RelayCommand SetWaterCommand { get; private set; }
        public RelayCommand SetBlendedCommand { get; private set; }
        public RelayCommand SetSportsDrinkCommand { get; private set; }
        public RelayCommand SetTeaCommand { get; private set; }
   
        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; } 
    }
}
