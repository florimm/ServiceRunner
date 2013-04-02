using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace GSM
{
    public class SMSRepository
    {
        public void Insert(SMSData data)
        {
            using(IDbConnection db = new SqlConnection("conn"))
            {
                string sql = @"Insert into SMSData (FromPhone,Content,DateTime,Readet) values(@FromPhone,@Content,@DateTime,@Readet);
                             SELECT CAST(SCOPE_IDENTITY() as int)";
                data.ID = db.Query<int>(sql, new {data.FromPhone, data.Content, data.DateTime, data.Readet}).Single();
            }
        }
        public void Update(SMSData data)
        {
            using (IDbConnection db = new SqlConnection("conn"))
            {
                string sql = @"update SMSData set FromPhone = @FromPhone,Content=@Content,DateTime = @DateTime,Readet = @Readet where ID = @ID";
                data.ID = db.Execute(sql, new { data.FromPhone, data.Content, data.DateTime, data.Readet, data.ID });
            }
        }
        public List<SMSData> Get(DateTime fromDate, DateTime toDate)
        {
            using (IDbConnection db = new SqlConnection("conn"))
            {
                string sql = @"select * from SMSData where DateTime between @fromDate and @toDate";
                return db.Query<SMSData>(sql, new { fromDate, toDate }).ToList();
            }
        }
        public List<SMSData> Get(bool readet = false)
        {
            using (IDbConnection db = new SqlConnection("conn"))
            {
                string sql = @"select * from SMSData where Readet = @readet";
                return db.Query<SMSData>(sql, new { readet}).ToList();
            }
        }
    }
}