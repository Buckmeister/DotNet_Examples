namespace PowerSquare.DataAccess
{
    interface IMainSqlConnector
    {
        bool ClearAllCalculations();
        bool GetAllCalculations();
        bool StoreCalculation();
    }
}