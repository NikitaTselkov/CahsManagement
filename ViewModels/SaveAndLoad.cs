using GalaSoft.MvvmLight;
using System.IO;
using System.Runtime.Serialization.Json;

namespace ViewModels
{
    public abstract class SaveAndLoad : ViewModelBase
    {

        /// <summary>
        /// Сохранение данных.
        /// </summary>
        /// <typeparam name="T"> Тип сохраняемого значения. </typeparam>
        /// <param name="path"> Путь сохранения. </param>
        /// <param name="result"> Сохраняемое значение. </param>
        public static void Save<T>(string path, T result)
        {
            var formatter = new DataContractJsonSerializer(typeof(T));

            using (var fs = new FileStream(path, FileMode.Create))
            {
                formatter.WriteObject(fs, result);
            }
        }

        /// <summary>
        /// Получение данных.
        /// </summary>
        /// <typeparam name="T"> Тип получаемых данных. </typeparam>
        /// <param name="path"> Путь получаемых данных. </param>
        /// <returns> Данные. </returns>
        public static T Load<T>(string path)
        {
            var formatter = new DataContractJsonSerializer(typeof(T));

            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                {

                    if (formatter.ReadObject(fs) is T users)
                    {
                        return users;
                    }
                    else
                    {
                        return default;
                    }
                }
                return default;
            }
        }
    }


}
