using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Theory_of_information_calc.command;
using Theory_of_information_calc.models;
using static System.Net.Mime.MediaTypeNames;

namespace Theory_of_information_calc.view_models
{

    public class MainViewModel : BaseViewModel
    {
        private double _entropy = 0;
        private string _text;
        private double _maxEntropy;
        private KeyValuePair<string, short> _selectedLang;
        private double _absRedundancy;
        private double _relRedundancy;
        private string _selectedHistoryString;

        private RelayCommand _textChangeCommand;
        private RelayCommand _importText;

        public Dictionary<string, short> Lang { get; private set; } = new Dictionary<string, short>()
        {
            { "Английский", 25 },
            { "Русский", 33}
        };

        public double EntropyProp
        {
            get { return _entropy; }
            set
            {
                _entropy = value;
                OnPropertyChanged();
            }
        }

        public string InputText
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public double MaxEntropy
        {
            get { return _maxEntropy; }
            set
            {
                _maxEntropy = value;
                OnPropertyChanged();
            }
        }

        public KeyValuePair<string, short> SelectedLang
        {
            get { return _selectedLang; }
            set
            {
                _selectedLang = value;
                OnPropertyChanged();
            }
        }

        public string SelectedHistoryString
        {
            get { return _selectedHistoryString; }
            set
            {
                _selectedHistoryString = value;
                OnPropertyChanged();
            }
        }

        public double AbsoluteRedundancy
        {
            get { return _absRedundancy; }
            set
            {
                _absRedundancy = value;
                OnPropertyChanged();
            }
        }

        public double RelativeRedundancy
        {
            get { return _relRedundancy; }
            set
            {
                _relRedundancy = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand TextChangeCommand
        {
            get
            {
                return _textChangeCommand ??
                    (_textChangeCommand = new RelayCommand(obj =>
                    {
                        if (StringIsValid(InputText))
                        {
                            EntropyProp = MaxEntropy = AbsoluteRedundancy = RelativeRedundancy = 0;
                            return;
                        }

                        if (StringIsValid(SelectedLang.Key))
                        {
                            MessageBox.Show("Выберите язык", "Ошибка");
                            return;
                        }

                        EntropyProp = EntropyCalc.CalculationEntropy(InputText.ToLower());
                        MaxEntropy = EntropyCalc.CalculateMaxEntropy(SelectedLang.Value);
                        AbsoluteRedundancy = EntropyCalc.CalculateAbsoluteRedundancy(MaxEntropy, EntropyProp);
                        RelativeRedundancy = EntropyCalc.CalculateRelativeRedundancy(MaxEntropy, AbsoluteRedundancy);
                    }));
            }
        }

        public RelayCommand ImportText
        {
            get
            {
                return _importText ??
                    (_importText = new RelayCommand(async obj =>
                    {
                        OpenFileDialog ofd = new OpenFileDialog();
                        ofd.Filter = "Текстовые файлы (*.txt) | *.txt";

                        if (ofd.ShowDialog() == true)
                            InputText = await FileReader.Read(ofd.FileName);
                    }));
            }
        }

        private bool StringIsValid(string str)
        {
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }

        public MainViewModel() 
        {
            EntropyProp = MaxEntropy = AbsoluteRedundancy = RelativeRedundancy = 0;
        } 
    }
}
