using NFWCommonsLibrary.Internationalization;
using System;

namespace PowerSquare.Models
{
    class CalculationModel
    {
        public int Number { get; set; }
        public string Operation { get; set; }
        public double Result { get; set; }
        public string Formula { get; set; }


        public CalculationModel(int number, string operation)
        {
            Number = number;
            Operation = operation;

            switch (operation)
            {
                case "SQRT":
                    Result = Math.Sqrt(Number);
                    Formula = string.Format(Translate.ResString("Formula_1_SQRT"), Number, Result);
                    break;
                case "PWR2":
                    Result = Math.Pow(Number, 2);
                    Formula = string.Format(Translate.ResString("Formula_2_PWR2"), Number, Result);
                    break;
                case "PWR3":
                    Result = Math.Pow(Number, 3);
                    Formula = string.Format(Translate.ResString("Formula_3_PWR3"), Number, Result);
                    break;
                default:
                    Result = 0;
                    Formula = string.Format(Translate.ResString("Formula_0_NA"), Number, Result);
                    break;
            }
        }

        public override string ToString()
        {
            return Formula;
        }
    }
}
