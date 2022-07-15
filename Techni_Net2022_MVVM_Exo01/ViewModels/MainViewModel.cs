using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.MVVM.Commands;
using Tools.MVVM.ViewModels;

namespace Techni_Net2022_MVVM_Exo01.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        #region Fields
        private int _Speed;
        private bool _IsStart;
        private bool _IsCrash;

        // Collection Observable qui permet à la vue de se mettre à jours après que son contenu soit modifier
        private ObservableCollection <string> _LogEvents;

        private IRelayCommand? _StartCommand;
        private IRelayCommand? _SpeedUpCommand;
        private IRelayCommand? _SpeedDownCommand;
        private IRelayCommand? _TurnLeftCommand;
        private IRelayCommand? _TurnRightCommand;

        private Random _Random;
        #endregion

        public MainViewModel()
        {
            _Random = new Random();

            _LogEvents = new ObservableCollection<string>();
            _LogEvents.Add("Start Simulation !");
        }

        #region Properties
        public int Speed
        {
            get { return _Speed; }
            set 
            { 
                _Speed = value;
                RaisePropertyChanged();
            }
        }
        public bool IsStart
        {
            get { return _IsStart; }
            set 
            { 
                _IsStart = value;
                RaisePropertyChanged();
            }
        }

        public bool IsCrash { get { return _IsCrash; } set { _IsCrash = value; } }

        public ObservableCollection<string> LogEvent
        {
            get { return _LogEvents; }
        }
        #endregion

        #region RelayCommand
        public IRelayCommand StartCommand
        {
            get { return _StartCommand ?? (_StartCommand = new RelayCommand(EngineSwitch, CheckCmdEngineLock)); }
        }
        public IRelayCommand SpeedUpCommand
        {
            get { return _SpeedUpCommand ?? (_SpeedUpCommand = new RelayCommand(SpeedUp, CheckEngineStart)); }
        }
        public IRelayCommand SpeedDownCommand
        {
            get { return _SpeedDownCommand ?? (_SpeedDownCommand = new RelayCommand(SpeedDown, CheckEngineStart)); }
        }
        public IRelayCommand TurnLeftCommand
        {
            get { return _TurnLeftCommand ?? (_TurnLeftCommand = new RelayCommand(TurnLeft, CheckEnableTurn)); }
        }
        public IRelayCommand TurnRightCommand
        {
            get { return _TurnRightCommand ?? (_TurnRightCommand = new RelayCommand(TurnRight, CheckEnableTurn)); }
        }
        #endregion

        #region Methods
        override protected void RaiseAllCanExecuteChanged()
        {
            // Cette méthode sera appeler lors de l'execution de "RaisePropertyChanged"
            StartCommand.RaiseCanExecuteChanged();
            SpeedUpCommand.RaiseCanExecuteChanged();
            SpeedDownCommand.RaiseCanExecuteChanged();
            TurnLeftCommand.RaiseCanExecuteChanged();
            TurnRightCommand.RaiseCanExecuteChanged();
        }

        private void EngineSwitch()
        {
            IsStart = !IsStart;

            LogEvent.Insert(0, IsStart ? "Demarrage du moteur" : "Arrêt du moteur");
        }

        private void SpeedUp()
        {
            Speed += 10;

            LogEvent.Insert(0, "Le vehicule accélére");
        }

        private void SpeedDown()
        {
            if(Speed > 0)
            {
                Speed -= 10;

                LogEvent.Insert(0, "Le vehicule decélére");
            }
            else
            {
                LogEvent.Insert(0, "Le vehicule est a l'arrêt");
            }
        }

        private void TurnLeft()
        {
            LogEvent.Insert(0, "Le vehicule tourne à gauche");

            DriftedSurvival();
        }

        private void TurnRight()
        {
            LogEvent.Insert(0, "Le vehicule tourne à droite");

            DriftedSurvival();
        }

        private bool CheckEngineStart()
        {
            return IsStart && !IsCrash;
        }

        private bool CheckEnableTurn()
        {
            return CheckEngineStart() && Speed > 0;
        }

        private bool CheckCmdEngineLock()
        {
            return Speed == 0 && !IsCrash;
        }

        private void DriftedSurvival()
        {
            if(Speed >= 130)
            {
                IsCrash = _Random.Next(3) == 1;

                if(IsCrash)
                {
                    LogEvent.Insert(0, "Le vehicule s'est crashé  (╯°□°）╯︵ ┻━┻ ");
                    Speed = 0;
                }
            }
        }
        #endregion
    }
}
