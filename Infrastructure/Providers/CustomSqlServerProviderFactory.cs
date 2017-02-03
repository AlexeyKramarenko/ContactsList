using NI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Providers
{
    public class CustomSqlServerProviderFactory : IDbProviderFactory
    {
        public string ParamPlaceholderFormat { get; set; }
        string GetCmdParameterPlaceholder(string paramName)
        {
            if (ParamPlaceholderFormat == null)
                return paramName;
            return String.Format(ParamPlaceholderFormat, paramName);
        }

        public string AddCommandParameter(IDbCommand cmd, object value)
        {
            var param = new SqlParameter();
            param.ParameterName = String.Format("@p{0}", cmd.Parameters.Count);
            param.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(param);
            return GetCmdParameterPlaceholder(param.ParameterName);
        }
        public IDbSqlBuilder CreateSqlBuilder(IDbCommand dbCommand)
        {
            return new DbSqlBuilder(dbCommand, this);
        }
        public IDbCommand CreateCommand()
        {
            return new SqlCommand();
        }
        public IDbConnection CreateConnection()
        {
            return new System.Data.SqlClient.SqlConnection();
        }

        #region Not implemented
        public string AddCommandParameter(IDbCommand cmd, DataColumn column, DataRowVersion sourceVersion)
        {
            throw new NotImplementedException();
        }
        public IDbDataAdapter CreateDataAdapter(EventHandler<RowUpdatingEventArgs> onRowUpdating, EventHandler<RowUpdatedEventArgs> onRowUpdated)
        {
            throw new NotImplementedException();
        }
        public object GetInsertId(IDbConnection connection)
        {
            throw new NotImplementedException();
        }
        #endregion

    }

}
