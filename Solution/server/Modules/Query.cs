using System;
using System.Collections;
using MySql.Data.MySqlClient;
using server;

namespace server.Modules
{
    public static class Query
    {
        public static ArrayList GetSelect(){
            DataBase db = new DataBase();
            string sql = "select n.nNo, n.nTitle, n.nContents, m.mName,"
            +" DATE_FORMAT(n.regDate, '%Y-%m-%d %H:%i') as regDate,"
            +" DATE_FORMAT(n.modDate, '%Y-%m-%d %H:%i') as modDate from Notice as n inner join Member as m "
            +"on (n.mNo = m.mNo and m.delYn = 'N') where n.delYn = 'N';";
            MySqlDataReader sdr = db.Reader(sql);
            ArrayList list=new ArrayList();
            while(sdr.Read()){
                Hashtable ht = new Hashtable();
                for(int i =0 ;i<sdr.FieldCount;i++)
                {
                    ht.Add(sdr.GetName(i),sdr.GetValue(i));
                }
                list.Add(ht);
            }
            db.ReaderClose(sdr);
            db.Close();
            return list;
        }
        public static bool GetInsert(string nTitle,string nContents,string mName)
        {
            string mNo = GetmNoSelect(mName);
            if(mNo=="")
            {
                return false;
            }
            DataBase db = new DataBase();
            string sql = string.Format("insert into Notice (mNo,nTitle,nContents)values({0},'{1}','{2}');",mNo,nTitle,nContents);
            
            if(db.NonQuery(sql)){
                db.Close();
                return true;
            }else{
                db.Close();
                return false;
            }
 
        }
        public static bool GetUpdate(string nNo,string nTitle,string nContents,string mName)
        {
            string mNo = GetmNoSelect(mName);
            if(mNo=="")
            {
                return false;
            }
            DataBase db = new DataBase();
            string sql = string.Format("update Notice set nTitle = '{1}', nContents = '{2}', modDate = CURRENT_TIMESTAMP"
                                      +" where nNo = {0} and mNo = {3};",nNo,nTitle,nContents,mNo);
            
            if(db.NonQuery(sql)){
                db.Close();
                return true;
            }else{
                db.Close();
                return false;
            }
        }

        public static bool GetDelete(string nNo,string mName)
        {
            string mNo = GetmNoSelect(mName);
            if(mNo=="")
            {
                return false;
            }
            DataBase db = new DataBase();
            string sql = string.Format("update Notice set delYn = 'Y', modDate = CURRENT_TIMESTAMP where nNo = {0} and mNo = {1};",nNo,mNo);
            
            if(db.NonQuery(sql)){
                db.Close();
                return true;
            }else{
                db.Close();
                return false;
            }
        }
        

        public static string GetmNoSelect(string mName){
            DataBase db = new DataBase();
            string sql = string.Format("select mNo from Member where mName = '{0}' and delYn='N'",mName);
            MySqlDataReader sdr = db.Reader(sql);
            string result ="";
            
            while(sdr.Read()){
                result=sdr.GetValue(0).ToString();
            }
            db.ReaderClose(sdr);
            db.Close();
            return result;
        }
    }
}