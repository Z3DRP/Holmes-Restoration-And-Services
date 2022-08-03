using Holmes_Services.Models.DomainModels;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;

namespace Holmes_Services.Data_Access.Repos
{
    public static class MessageRepo
    {
        private static string _con = DbConnector.GetConnection();
        private static IEnumerable<HolmesMessage> _msgs;

        public static bool AddMessage(HolmesMessage message)
        {
            string procedure = "[sp_add_msg]";
            int rowsAffected;
            var parameters = new
            {
                senderId = message.Sender_Id,
                msgTxt = message.Message,
                dateSent = message.Send_Date
            };

            using (IDbConnection db = new MySqlConnection(_con))
            {
                rowsAffected = db.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
            }

            return rowsAffected > 0 ? true : false;
        }
        private static void InitMsgs() => _msgs = new List<HolmesMessage>();

    }
}
