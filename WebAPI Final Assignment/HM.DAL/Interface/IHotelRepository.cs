using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HM.Models;
namespace HM.DAL.Interface
{
    public interface IHotelRepository
    {
        List<Hotel> GetHotels();//done
        List<Room> GetRooms(string city, decimal? pincode, decimal? price, string category);//done
        bool IsAvailable(int id,DateTime date);//Not got question
        Hotel AddHotel(Hotel hotel);//done
        bool Book(Booking booking);//done
        bool UpdateBookDate(int id, Booking booking);//done
        bool UpdateStatus(int id, Booking booking);//done
        bool DeleteBook(int id);//done
    }
}
