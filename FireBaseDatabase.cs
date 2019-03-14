using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace FireBase_Http
{
    public class FireBaseDatabase
    {
        /// <summary>
        /// the Base URL of the Firebase Project (without the '/' at the end)
        /// </summary>
        public string BaseURL { get; }


        public FireBaseDatabase(string dataBaseUrl)
        {
            BaseURL = dataBaseUrl;
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
            
            var request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath);
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
            var request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath);
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
            var request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath);
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

            var request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath);
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
            var request = WebRequest.CreateHttp(BaseURL + "/" + jsonPath);
            request.Method = "DELETE";
            request.ContentType = "application/json";
            var response = request.GetResponse();
            string json = (new StreamReader(response.GetResponseStream())).ReadToEnd();

            return true;
        }

    }
}
