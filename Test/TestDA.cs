using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Model.Service;

namespace CodePorter.Test
{
    public class TestDA
    {
        //public TravelTicket GetTravelTicketBy(int pri)
        //{
        //    DbCommand dbCmd = DbObject.GetSqlStringCommand(ConsumeRecordDetail_ForPayment_SelectByTransactionID);
        //    DbObject.AddInParameter(dbCmd, "@TransactionID", DbType.String, transactionID);

        //    TravelTicket consumeRecordInfo = null;
        //    using (IDataReader dataReader = DbObject.ExecuteReader(dbCmd))
        //    {
        //        if (dataReader.Read())
        //        {
        //            consumeRecordInfo = new TicketConsumeRecordInfo();
        //            consumeRecordInfo.TransactionID = DbFieldHelper.GetString(dataReader, "ID");
        //            consumeRecordInfo.OrderID = DbFieldHelper.GetInt32(dataReader, "OrderID");
        //            consumeRecordInfo.ConsumeType = EnumHelper.GetField<TicketConsumeType>(DbFieldHelper.GetInt32(dataReader, "ConsumeType"));
        //            consumeRecordInfo.CustomerID = DbFieldHelper.GetString(dataReader, "CustomerID");
        //            consumeRecordInfo.OrderName = DbFieldHelper.GetString(dataReader, "OrderName");
        //            consumeRecordInfo.OrderType = EnumHelper.GetField<OrderType>(DbFieldHelper.GetInt32(dataReader, "OrderType"));
        //            consumeRecordInfo.TotalAmount = DbFieldHelper.GetDecimal(dataReader, "TotalAmount");
        //            consumeRecordInfo.IsEnabled = DbFieldHelper.GetInt(dataReader, "IsEnabled");
        //        }
        //    }
        //    return consumeRecordInfo;
        //}

        //public string Update(TravelTicket travelTicket)
        //{
        //    if(travelTicket.PartiallyUpdateParams == null) 

        //}
    }
}
