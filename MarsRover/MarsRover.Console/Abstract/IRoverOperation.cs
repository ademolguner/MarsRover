using MarsRover.Console.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Console.Abstract
{
   public  interface IRoverOperation
    {
        /// <summary>
        /// Hareket için girilen verilerin doğruluğunu yapacağız
        /// 2 adet int bir adet char
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        ReturnDataResult IsPositionValuesCorrect(List<string> values);


        /// <summary>
        /// girilen karakteri kontrol ediyoruz. Sayı veya metin kontrolü icin
        /// durum bilgisi karsılastımanda kullanacağız
        /// </summary>
        /// <param name="charValue"></param>
        /// <param name="controlStatus"></param>
        /// <returns></returns>
        bool IsCharacterCorrect(char charValue, bool controlStatus);


        /// <summary>
        /// Geziciden gelen islem  koda göre  hareket edilecektir,
        /// hareket için yapılacak işlem imzası
        /// </summary>
        Rover Motion(Rover rover, List<int> maxBorderValues, string orientationValues);


        /// <summary>
        /// Hareketler yapılırken rover a gönderilen kod , rover in son konumu ile devam etmesi istenen konumunun
        /// hareket etmeden önce  gridi aşıp aşmadığı kontrolü yapalım.
        /// </summary>
        /// <param name="maxBorderValues"></param>
        /// <returns></returns>
        ReturnDataResult GridPointControl(List<int> maxBorderValues);
    }
}
