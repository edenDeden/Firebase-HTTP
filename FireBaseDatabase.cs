﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FirebaseClass
{
    public class FireBaseDatabase
    {
        /// <summary>
        /// the Base URL of the Firebase Project (without the '/' at the end)
        /// </summary>
        public string BaseURL { get; }

        public string AuthKey = "TgcED9oR7IhwiQMB2jqDoMdpQccjh5daQYTeeTTp";


        public FireBaseDatabase(string dataBaseUrl)
        {
            BaseURL = dataBaseUrl;
        }

        public FireBaseDatabase(string dataBaseUrl,string authKey) :this(dataBaseUrl)
        {
            AuthKey = authKey;
        }


        /// <summary>
        /// Sends the obj as json to the given path
        /// </summary>
        /// <param name="jsonPath"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool SendData(string jsonPath,object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            HttpWebRequest request;
            if(AuthKey == "")
                request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath);
            else
                request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath + "?auth=" + AuthKey);

            request.Method = "PUT";
            request.ContentType = "application/json";
            var buffer = Encoding.UTF8.GetBytes(json);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            var response = request.GetResponse();
            json = (new StreamReader(response.GetResponseStream())).ReadToEnd();

            return true;
        }

        /// <summary>
        /// returns the object from the json in the path provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonPath"></param>
        /// <returns></returns>
        public T GetData<T>(string jsonPath)
        {
            HttpWebRequest request;
            if(AuthKey == "")
                request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath);
            else
                request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath + "?auth=" + AuthKey);

            request.Method = "GET";
            request.ContentType = "application/json";
            var response = request.GetResponse();
            string json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// returns the raw json file in the provided path
        /// </summary>
        /// <param name="jsonPath"></param>
        /// <returns></returns>
        public string GetData(string jsonPath)
        {

            HttpWebRequest request;
            if (AuthKey == "")
                request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath);
            else
                request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath + "?auth=" + AuthKey);


            request.Method = "GET";
            request.ContentType = "application/json";
            var response = request.GetResponse();
            string json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
            return json;
        }

        /// <summary>
        /// add the data with a auto generated title to the given path
        /// </summary>
        /// <param name="jsonPath"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool PushData(string jsonPath, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            HttpWebRequest request;
            if(AuthKey == "")
                request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath);
            else
                request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath + "?auth=" + AuthKey);

            request.Method = "POST";
            request.ContentType = "application/json";
            var buffer = Encoding.UTF8.GetBytes(json);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            var response = request.GetResponse();
            json = (new StreamReader(response.GetResponseStream())).ReadToEnd();

            return true;
        }

        /// <summary>
        /// deletes all the data from the given path
        /// </summary>
        /// <param name="jsonPath"></param>
        /// <returns></returns>
        public bool DeleteData(string jsonPath)
        {
            HttpWebRequest request;
            if(AuthKey == "")
                request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath);
            else
                request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath + "?auth=" + AuthKey);

            request.Method = "DELETE";
            request.ContentType = "application/json";
            var response = request.GetResponse();
            string json = (new StreamReader(response.GetResponseStream())).ReadToEnd();

            return true;
        }

        public List<T> GetListOf<T>(string jsonPath)
        {
            string data = GetData(jsonPath);
            JObject jo = JObject.Parse(data);
            

            List<T> answer = new List<T>();

            foreach (var obj in jo)
            {

                T item = JsonConvert.DeserializeObject<T>(obj.Value.ToString());
                answer.Add(item);
            }

            return answer;
        }

    }
}
