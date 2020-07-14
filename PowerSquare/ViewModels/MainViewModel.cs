using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using NFWCommonsLibrary.ViewModels;
using NFWCommonsLibrary.Internationalization;
using PowerSquare.Models;
using PowerSquare.DataAccess;
using NFWCommonsLibrary.Input;
using NFWCommonsLibrary.Deployment;

namespace PowerSquare.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly IMainSqlConnector sqlConnector;

        public MainViewModel()
        {
            sqlConnector = new MainSqlConnector(this);
            calculations = new ObservableCollection<CalculationModel>();

            sqlConnector.GetAllCalculations();

            operationModeEntries = new ObservableCollection<OperationModeEntryModel>
            {     
                new OperationModeEntryModel(Translate.ResString("OperationMode_1"), Translate.ResString("OperationModeDisplay_1")),
                new OperationModeEntryModel(Translate.ResString("OperationMode_2"), Translate.ResString("OperationModeDisplay_2")),
                new OperationModeEntryModel(Translate.ResString("OperationMode_3"), Translate.ResString("OperationModeDisplay_3"))
            };

            if (string.IsNullOrEmpty(OperationMode))
            {
                OperationMode = OperationModeEntries.FirstOrDefault().ToString();
            }

            //Number = 1;

            History.Filter = HistoryFilter;
        }

        public string Version
        {
            get { return DeploymentAction.GetRunningVersion(); }
        }

        private ObservableCollection<CalculationModel> calculations;
        public ObservableCollection<CalculationModel> Calculations 
        { 
            get 
            { 
                return calculations; 
            } 
            set
            {
                calculations = value;
                OnPropertyChanged(nameof(Calculations));
            }
        }

        public ICollectionView History 
        {
            get { return CollectionViewSource.GetDefaultView(Calculations); }
        }

        private string filterString = string.Empty;
        public string FilterString
        {
            get { return filterString; }
            set
            {
                filterString = value;
                OnPropertyChanged(nameof(FilterString));
                History.Refresh();
            }
        }

        private bool HistoryFilter(object item)
        {  
            CalculationModel calc = item as CalculationModel;
            return calc.Formula.Contains(FilterString);
        }

        public int Number { get; private set; }
        private string strNumberWrapper;
        
        // TODO: Insert translatable error message
        //private string errMessageValidation = Translate.ResString("ValidationError_1_Poitive_Integer");
        
        [Display(Name = "Nr.")]
        [Required]
        [Range(1,999999)]
        public string strNumber
        {
            get { return strNumberWrapper ?? Number.ToString(); }
            set
            {
                ValidateProperty(value);

                if (int.TryParse(value, out int parsedInt))
                {
                    strNumberWrapper = null;

                    Number = parsedInt;
                    OnPropertyChanged(nameof(Number));
                }
                else
                {
                    strNumberWrapper = value;
                }

                Result = String.Empty;
            }
        }

        private ObservableCollection<OperationModeEntryModel> operationModeEntries;
        public ObservableCollection<OperationModeEntryModel> OperationModeEntries
        {
            get { return operationModeEntries; }
            set
            {
                operationModeEntries = value;
                OnPropertyChanged(nameof(OperationModeEntries));
            }
        }

        public string OperationMode
        {
            get
            {
                return Properties.Settings.Default.OperationMode;
            }
            set
            {
                OnPropertyChanged(nameof(OperationMode));
                Properties.Settings.Default.OperationMode = value;
            }
        }

        private bool boolCalcEnabled;
        public bool CalcEnabled
        {
            get { return boolCalcEnabled; }

            private set
            {
                boolCalcEnabled = value;
                OnPropertyChanged(nameof(CalcEnabled));
            }
        }

        private ICommand icmdCalcClicked;
        public ICommand btnCalcClickCommand
        {
            get
            {
                return icmdCalcClicked ?? (icmdCalcClicked =
                                                new CommandHandler(
                                                    btnCalcClicked,
                                                    btnCalcClickedCanExecute)
                                                );
            }
        }

        public bool btnCalcClickedCanExecute()
        {
            if (HasErrors)
            {
                CalcEnabled = false;
                Result = Translate.ResString("Result_1_Input_Error");
                Status = Translate.ResString("Status_2_Error");
                return false;
            }

            CalcEnabled = true;
            return true;
        }

        public void btnCalcClicked()
        {
            
            if (sqlConnector.StoreCalculation())
            {
                CalculationModel calcModel = new CalculationModel(Number, OperationMode);
                Calculations.Add(calcModel);
                Result = calcModel.Formula;
                Status = Translate.ResString("Status_1_Ready");
            }
            
        }

        private string strResult = string.Empty;
        public string Result
        {
            get { return strResult; }
            set
            {
                strResult = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        private ICommand icmdClearClicked;
        public ICommand btnClearClickCommand
        {
            get
            {
                return icmdClearClicked ?? (icmdClearClicked =
                                                new CommandHandler(
                                                    btnClearClicked,
                                                    btnCalcClearCanExecute)
                                                );
            }
        }

        public bool btnCalcClearCanExecute()
        {
            return true;
        }

        public void btnClearClicked()
        {
            if (sqlConnector.ClearAllCalculations())
            {
                Calculations.Clear();
                Status = Translate.ResString("Status_3_Cleared"); ;
            }
        }

        private string strStatus;
        public string Status
        {
            get { return strStatus; }
            set
            {
                strStatus = value;
                OnPropertyChanged(nameof(Status));
            }
        }
    }
}
