using NFWCommonsLibrary.DataAccess;
using NFWCommonsLibrary.Extensions;
using PowerSquare.Models;
using PowerSquare.ViewModels;
using System.Data.SqlClient;

// In order to use this app without DB access, I've commented out all affected parts.

namespace PowerSquare.DataAccess
{
    class MainSqlConnector : IMainSqlConnector
    {
        private readonly MainViewModel viewModel;
        public MainSqlConnector(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool GetAllCalculations()
        {
            //SqlDataReader sqlDataReader = SqlAction.GetQuery(
            //    "SELECT Number, Operation FROM PowerSquare;"
            //    );

            //if (sqlDataReader.HasRows)
            //{
            //    viewModel.Calculations.Clear();
            //    while (sqlDataReader.Read())
            //    {
            //        viewModel.Calculations.Add(new CalculationModel(
            //            sqlDataReader.SafeGetInt32(0),
            //            sqlDataReader.SafeGetString(1)
            //            ));
            //    }
            //}
            return true;
        }

        public bool ClearAllCalculations()
        {
            //return SqlAction.ExecuteNonQuery(
            //    "DELETE FROM PowerSquare;"
            //    );
            return true;
        }

        public bool StoreCalculation()
        {
            //return SqlAction.ExecuteNonQuery(
            //    "INSERT INTO PowerSquare VALUES(@Parameter0, @Parameter1);",
            //    viewModel.Number,
            //    viewModel.OperationMode
            //    );
            return true;
        }
    }
}
