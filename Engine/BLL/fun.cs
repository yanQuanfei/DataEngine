using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DataEngine.Models;

namespace Engine
{
    public class fun
    {
        public int AddMsg()
        {
            using (IDbConnection conn = DapperContext.MsSqlConnection())
            {
                List<MsgFlow> list = new List<MsgFlow>();
                for (int i = 0; i < 5; i++)
                {
                    MsgFlow msg = new MsgFlow();
                    msg.Initiator = "闫全飞";
                    msg.UserJID = "1042";
                    msg.RecordID = 1;
                    msg.Classify = 1;
                    msg.LaunchTime = DateTime.Now.ToString();
                    msg.State = (int)MsgState.Nil;

                    list.Add(msg);
                }
                string sqlCommandText = @"INSERT INTO MsgFlow(Initiator,UserJID,RecordID,State,Classify,LaunchTime)VALUES(@Initiator,@UserJID,@RecordID,@State,@Classify,@LaunchTime)";
                int result = conn.Execute(sqlCommandText, list);
                return result;
            }
        }
    }
}
