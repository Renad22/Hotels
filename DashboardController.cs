using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;


namespace Hotels.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

       

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;

        }


        public IActionResult Delete(int id)
        {
            var hotelDel = _context.hotel.SingleOrDefault(x => x.Id == id);
            if(hotelDel != null)
            {
                _context.hotel.Remove(hotelDel);
                _context.SaveChanges();
                TempData["Del"] = "OK";
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Index(string cities)
        {
            var hotel = _context.hotel.Where(x => x.City.Contains(cities));
            return View(hotel);
        }

        //MyPassApp Gmail: jynnzndberqvudoi

        //[Authorize]
        public IActionResult Index()
        {
           // var currentUser = HttpContent.User.Identity.Name;
           // ViewBag.currentUser = currentUser;
            CookieOptions option = new CookieOptions(); // Create Cookies
              // option.Expires = DateTime.Now.AddMinutes(20); //
           // Response.Cookies.Append("UserName", currentUser, option); // To Store info inside Cookies

           
          //  HttpContext.Session.SetString("UserName" , currenUser); // Create Session

            var hotel = _context.hotel.ToList();
            return View(hotel);
        }

        public async Task<string> SendEmail()
        {
            var Message = new MimeMessage();
            Message.From.Add(new MailboxAddress("Test msg", "Ralhindi0011@stu.kau.edu.sa"));
            Message.To.Add(MailboxAddress.Parse("Renadwh22@hotmail.com"));
            Message.Subject="Test Email from my ASP.net core MVC";
            Message.Body = new TextPart("Plain")
            {
                Text = "Welcome In my App"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587);
                    client.Authenticate("Ralhindi0011@stu.kau.edu.sa","jynnzndberqvudoi");
                    await client.SendAsync(Message);
                    client.Disconnect(true);
                }
                catch  (Exception e)
                {
                    return e.Message.ToString();
                }
                
            }
            return "Ok";
        } 

        public IActionResult Rooms()
        {
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;

            //ViewBag.currentUser = Request.Cookies["currentUser"]; // Calling User's info from Cookies currentUser استرجاع البيانات

            ViewBag.currentUser = HttpContext.Session.GetString("UserName"); // Calling User's info from session  استرجاع / استدعاء البيانات


            var rooms = _context.rooms.ToList();
            return View(rooms);

        }

        public async Task<IActionResult> RoomDetails()
        {
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;

            var rooms = _context.rooms.ToList();
            ViewBag.rooms = rooms;

            ViewBag.currentuser = HttpContext.Session.GetString("UserName");

            var roomDetails = _context.roomDetails.ToList();
            return View(roomDetails);
        }

        public IActionResult AddRoomDetails(RoomDetails roomDetails)
        {
            _context.roomDetails.Add(roomDetails);
            _context.SaveChanges();
            return RedirectToAction("RoomDetails");
        }

        public IActionResult CreateNewRoom(Rooms rooms)
        {
            _context.rooms.Add(rooms);
            _context.SaveChanges();
            return RedirectToAction("Rooms");
        }

        public IActionResult CreateNewHotel(Hotel hotels)
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Add(hotels);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            var hotel = _context.hotel.ToList();
            return View("Index", hotel);
        }

        public IActionResult Edit(int id)
        {
            var HotelEdit = _context.hotel.SingleOrDefault(x => x.Id == id); //Search

            return View(HotelEdit);
        }

        public IActionResult Update(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Update(hotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Edit");
        }


        public IActionResult EditRooms(int id)
        {
            var RoomEdit = _context.rooms.SingleOrDefault(x => x.Id == id); //Search

            return View(RoomEdit);
        }

        public IActionResult UpdateRooms(Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                _context.rooms.Update(rooms);
                _context.SaveChanges();
                return RedirectToAction("Rooms");
            }

            return View("EditRooms");
        }

        public IActionResult DeleteRooms(int id)
        {
            var RoomDel = _context.rooms.SingleOrDefault(x => x.Id == id);
            if (RoomDel != null)
            {
                _context.rooms.Remove(RoomDel);
                _context.SaveChanges();
                TempData["Del"] = "OK";
            }
            return RedirectToAction("Rooms");
        }


        public IActionResult EditRoomDetails(int id)
        {
            var RoomDetailsEdit = _context.roomDetails.SingleOrDefault(x => x.Id == id); //Search

            return View(RoomDetailsEdit);
        }

        public IActionResult UpdateRoomDetails(RoomDetails roomDetails)
        {
            if (ModelState.IsValid)
            {
                _context.roomDetails.Update(roomDetails);
                _context.SaveChanges();
                return RedirectToAction("RoomDetails");
            }

            return View("EditRoomDetails");
        }

        public IActionResult DeleteRoomDetails(int id)
        {
            var RoomDetailDel = _context.roomDetails.SingleOrDefault(x => x.Id == id);
            if (RoomDetailDel != null)
            {
                _context.roomDetails.Remove(RoomDetailDel);
                _context.SaveChanges();
                TempData["Del"] = "OK";
            }
            return RedirectToAction("RoomDetails");
        }

    }
}

