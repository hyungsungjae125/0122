using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform.Modules
{
    public class DB
    {
        private WebClient client;
        private NameValueCollection collection;

        public DB()
        {
            client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.Encoding = Encoding.UTF8;
        }
        public ListViewItem[] Select()
        {
            ListViewItem[] result;//검색해온 리스트를 담아둘 곳

            string url = Program.url + "select";
            Stream stream = client.OpenRead(url);
            StreamReader sr = new StreamReader(stream);
            string str = sr.ReadToEnd();
            ArrayList obj = JsonConvert.DeserializeObject<ArrayList>(str);
            result = new ListViewItem[obj.Count];
            for (int i = 0; i < obj.Count; i++)
            {

                JObject jo = (JObject)obj[i];
                string[] arr = new string[jo.Count];

                foreach (JProperty jp in jo.Properties())
                {
                    switch (jp.Name)
                    {
                        case "nNo":
                            arr[0] = jp.Value.ToString();
                            break;
                        case "nTitle":
                            arr[1] = jp.Value.ToString();
                            break;
                        case "nContents":
                            arr[2] = jp.Value.ToString();
                            break;
                        case "mName":
                            arr[3] = jp.Value.ToString();
                            break;
                        case "regDate":
                            arr[4] = jp.Value.ToString();
                            break;
                        case "modDate":
                            arr[5] = jp.Value.ToString();
                            break;
                    }
                }
                result[i] = new ListViewItem(arr);
            }
            return result;
        }
        public bool Insert(string nTitle, string nContents, string mName)
        {
            //==============입력받은 값에 대해서 추가해주는 부분=====================================
            collection = new NameValueCollection();
            collection.Add("nTitle", nTitle);
            collection.Add("nContents", nContents);
            collection.Add("mName", mName);

            string url = Program.url + "insert";
            string method = "POST";
            byte[] result = client.UploadValues(url, method, collection);
            string strResult = Encoding.UTF8.GetString(result);
            if (strResult == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Update(string nNo, string nTitle, string nContents, string mName)
        {
            //==============입력받은 값에 대해서 수정해주고 수정날짜를 바꿔주는 부분======================================
            collection = new NameValueCollection();
            collection.Add("nNo", nNo);
            collection.Add("nTitle", nTitle);
            collection.Add("nContents", nContents);
            collection.Add("mName", mName);

            string url = Program.url + "update";
            string method = "POST";
            byte[] result = client.UploadValues(url, method, collection);
            string strResult = Encoding.UTF8.GetString(result);
            if (strResult == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Delete(string nNo, string mName)
        {
            //==============입력받은 값에 대해서 delYn을 'Y'로 고쳐주고 수정날짜를 바꿔주는 부분=========================
            collection = new NameValueCollection();
            collection.Add("nNo", nNo);
            collection.Add("mName", mName);

            string url = Program.url + "delete";
            string method = "POST";
            byte[] result = client.UploadValues(url, method, collection);
            string strResult = Encoding.UTF8.GetString(result);
            if (strResult == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
