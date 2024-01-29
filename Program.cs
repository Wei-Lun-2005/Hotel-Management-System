//=============================================================================
// Student Number : S10242883
// Student Name : Ho Wei Lun
// 
// Feature : 2, 4, 6, Advance Feature A
//
//
//=============================================================================
using Assignment;

var room_List = new List<Room> { };
var guests_List = new List<Guest> { };

//==========================================================
// search room from room list
// Made by: Wei Lun, Javier
// 
//==========================================================
Room SearchRoom(int roomNumber, List<Room> room_List)
{
    foreach(Room r in room_List)
    {
        if(r.RoomNumber == roomNumber)
        {
            return r;
        }
    }
    return null;
}

//==========================================================
// Search guest from guest list
// Made by: Wei Lun, Javier
// 
//==========================================================
Guest SearchGuest(string passportNum, List<Guest> guests_List)
Guest SearchGuest(string passportNum, List<Guest> guests_List)
{
    foreach(Guest g in guests_List)
    {
        if(g.PassportNum == passportNum)
        {
            return g;
        }
    }
    return null;
}


//==========================================================
// Initialize room_List and guests_List
// Made by: Wei Lun, Javier
// 
//==========================================================
void InitializeList(List<Room> room_List, List<Guest> guests_List)
{
    string[] room_data = File.ReadAllLines("Rooms.csv");
    for (int i = 1; i < room_data.Length; i++)
    {
        string[] line = room_data[i].Split(",");
        int roomNumber = Convert.ToInt32(line[1]);
        string bedConfig = line[2];
        double rate = Convert.ToDouble(line[3]);
        bool avail = true;

        if (line[0] == "Standard")
        {
            StandardRoom room = new StandardRoom(roomNumber, bedConfig, rate, avail);
            room_List.Add(room);
        }
        else
        {
            DeluxeRoom room = new DeluxeRoom(roomNumber, bedConfig, rate, avail);
            room_List.Add(room);
        }
    }

    string[] guest_data = File.ReadAllLines("Guests.csv");
    for (int i = 1; i < guest_data.Length; i++)
    {
        string[] line = guest_data[i].Split(",");
        string status = line[2];
        int points = Convert.ToInt32(line[3]);
        Membership membership = new Membership(status, points);

        string name = line[0];
        string passportNum = line[1];
        Stay hotel = new Stay();
        bool ischeckedIn = false;
        Guest guest = new Guest(name, passportNum, hotel, membership, ischeckedIn);

        guests_List.Add(guest);
    }
}

//==========================================================
// Initialize Stays.csv
// Made by: Wei Lun, Javier
// 
//==========================================================
void InitializeStayData(List<Room> room_List, List<Guest> guests_List)
{
    string[] stay_data = File.ReadAllLines("Stays.csv");
    for(int i = 1; i < stay_data.Length; i++)
    {
        string[] line = stay_data[i].Split(',');
        DateTime CheckinDate = Convert.ToDateTime(line[3]);
        DateTime CheckoutDate = Convert.ToDateTime(line[4]);
        Stay stay = new Stay(CheckinDate, CheckoutDate);

        Room room = (Room)SearchRoom(Convert.ToInt32(line[5]), room_List).Clone();
        if (room is null)
        {
            continue;
        }
        else if(room is StandardRoom)
        {
            StandardRoom s_room = (StandardRoom)room;
            bool wifi = Convert.ToBoolean(line[6].ToLower());
            bool breakfast = Convert.ToBoolean(line[7].ToLower());
            s_room.RequireWifi = wifi;
            s_room.RequireBreakfast = breakfast;
            stay.RoomList.Add(s_room);
        }
        else if(room is DeluxeRoom)
        {
            DeluxeRoom d_room = (DeluxeRoom)room;
            bool bed = Convert.ToBoolean(line[8].ToLower());
            d_room.AdditionalBed = bed;

            stay.RoomList.Add(d_room);
        }
        if (line[9] != "")
        {
            Room room1 = (Room)SearchRoom(Convert.ToInt32(line[9]), room_List).Clone();
            if (room1 is null)
            {
                continue;
            }
            else if (room1 is StandardRoom)
            {
                StandardRoom s_room = (StandardRoom)room1;
                bool wifi = Convert.ToBoolean(line[10].ToLower());
                bool breakfast = Convert.ToBoolean(line[11].ToLower());
                s_room.RequireWifi = wifi;
                s_room.RequireBreakfast = breakfast;

                stay.RoomList.Add(s_room);
            }
            else if (room1 is DeluxeRoom)
            {
                DeluxeRoom d_room = (DeluxeRoom)room1;
                bool bed = Convert.ToBoolean(line[12].ToLower());
                d_room.AdditionalBed = bed;

                stay.RoomList.Add(d_room);
            }
        }

        Guest guest = SearchGuest(line[1], guests_List);
        guest.HotelStay = stay;
        if (line[2] == "FALSE")
        {
            guest.IsCheckedin = true;
        }

    }
}
//==========================================================
// Feature 1
// 
//==========================================================
void ListGuest(List<Guest> guest_list)

{

    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 30)) + " Guest Information " + string.Concat(Enumerable.Repeat("-", 30)));

    Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}", "Name", "Passport_No", "Check_In", "Check_Out", "Status", "Points", "Check_In");

    Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}", "----", "-----------", "--------", "---------", "------", "------", "--------");

    foreach (Guest g in guest_list)

    {

        Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}", g.Name, g.PassportNum, g.HotelStay.CheckinDate.ToString("dd/MM/yyyy"), g.HotelStay.CheckoutDate.ToString("dd/MM/yyyy"), g.Member.Status, g.Member.Points, g.IsCheckedin);

    }

}
//==========================================================
// Feature 2 
// 
//==========================================================
void CheckAvailRoom(List<Room> room_List, List<Guest> guests_List)
{
    foreach(Guest g in guests_List)
    {
        if(g.IsCheckedin is true)
        {
           foreach(Room r in g.HotelStay.RoomList)
            {
                SearchRoom(r.RoomNumber, room_List).IsAvail = false;
            }
        }
        else
        {
            foreach (Room r in g.HotelStay.RoomList)
            {
                SearchRoom(r.RoomNumber, room_List).IsAvail = true;
            }
        }
    }
}
void ListAvailRoom(List<Room> room_List)
{
    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 32)) + " Available Room " + string.Concat(Enumerable.Repeat("-", 32)));
    Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}", "Room Number", "Bed Configuration", "Daily Rate($)", "Type");
    Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}", "-----------", "-----------------", "-------------", "----");
    foreach (Room r in room_List)
    {
        if (r.IsAvail is true)
        {
            Console.WriteLine("{0,-20}{1,-20}${2,-19}{3,-20}", r.RoomNumber, r.BedConfiguration, r.DailyRate, r.GetType().Name);
        }
    }
}

//==========================================================
// Feature 3 
// 
//==========================================================
void RegisterGuest(List<Guest> guests_List)
{
    Console.Write("Enter your name: ");
    string? name = Console.ReadLine();
    Console.Write("Enter your passport number: ");
    string? passport_no = Console.ReadLine();
    if (passport_no.Length != 9)
    {
        Console.WriteLine("Invalid passport number!\n");
        RegisterGuest(guests_List);
        return;
    }
    string temp = passport_no.Remove(0,1);
    temp  = temp.Remove(temp.Length - 1,1);
    if (Char.IsLetter(passport_no[0]) == false)
    {
        Console.WriteLine("Invalid passport number!\n");
        RegisterGuest(guests_List);
        return;
    }
    if (Char.IsLetter(passport_no[8]) == false)
    {
        Console.WriteLine("Invalid passport number!\n");
        RegisterGuest(guests_List);
        return;
    }
    try
    {
        Convert.ToInt32(temp);
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid passport number!\n");
        RegisterGuest(guests_List);
        return;
    }
    Membership newmember = new Membership("Ordinary", 0);
    Stay hotel = new Stay();
    bool ischeckedIn = false;
    Guest newguest = new Guest(name, passport_no, hotel, newmember, ischeckedIn);
    guests_List.Add(newguest);
    string addfile = name + "," + passport_no + "," + "Ordinary" + "," + "0";
    File.AppendAllText("Guests.csv", addfile);
    Console.WriteLine("You have successfully registered!");
}

//==========================================================
// Feature 4 
// 
//==========================================================
void CheckInGuest(List<Room> room_List, List<Guest> guests_List)
{
    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 32)) + " Check-In Guest " + string.Concat(Enumerable.Repeat("-", 32)));
    Console.WriteLine("Guest's Name   Passport Number");
    Console.WriteLine("------------   ---------------");

    foreach(Guest g in guests_List)
    {
        if(g.IsCheckedin is false)
        {
            Console.WriteLine("{0,-15}{1,-15}", g.Name, g.PassportNum);
        }
    }
    Console.WriteLine();
    Console.WriteLine("Exit [x]");
    Console.WriteLine();
    Console.Write("Enter Passport Number of guest to check in: ");
    string passport_num = Console.ReadLine();
    if(passport_num == "x")
    {
        return;
    }

    Guest guest = SearchGuest(passport_num, guests_List);
    if(guest == null)
    {
        Console.WriteLine("Guest not found.");
        CheckInGuest(room_List, guests_List);
        return;
    }
    if(guest.IsCheckedin == true)
    {
        Console.WriteLine("Guest Can only register for one stay");
        CheckInGuest(room_List, guests_List);
        return;
    }
    Console.Write("Enter check in Date (dd/mm/yyyy): ");
    string CheckinInput = Console.ReadLine();
    try
    {
        Convert.ToDateTime(CheckinInput);
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid Date.");
        Console.WriteLine();
        CheckInGuest(room_List, guests_List);
        return;
    }
    DateTime CheckinDate = Convert.ToDateTime(CheckinInput);
    if(CheckinDate.Date < DateTime.Now.Date)
    {
        Console.WriteLine("Invalid Date.");
        Console.WriteLine();
        CheckInGuest(room_List, guests_List);
        return;
    }
    Console.Write("Enter check out Date (dd/mm/yyyy): ");
    string CheckoutInput = Console.ReadLine();
    try
    {
        Convert.ToDateTime(CheckoutInput);
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid Date.");
        Console.WriteLine();
        CheckInGuest(room_List, guests_List);
        return;
    }
    DateTime CheckoutDate = Convert.ToDateTime(CheckoutInput);
    if (CheckoutDate <= CheckinDate)
    {
        Console.WriteLine("Invalid Date.");
        Console.WriteLine();
        CheckInGuest(room_List, guests_List);
        return;
    }
    Stay stay = new Stay(CheckinDate,CheckoutDate);
    Console.WriteLine();
    while (true)
    {
        ListAvailRoom(room_List);
        Console.WriteLine();
        Console.WriteLine("Exit [x]");
        Console.WriteLine();
        Console.Write("Enter room number: ");
        string roomNum = Console.ReadLine();
        if (roomNum == "x")
        {
            CheckInGuest(room_List, guests_List);
            return;
        }
        try
        {
            Convert.ToInt32(roomNum);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Room Number.");
            Console.WriteLine();
            CheckInGuest(room_List, guests_List);
            return;
        }
        Room room = SearchRoom(Convert.ToInt32(roomNum), room_List);
        if (room == null)
        {
            Console.WriteLine("Room does not exist.");
            Console.WriteLine();
            CheckInGuest(room_List, guests_List);
            return;
        }
        if (room.IsAvail is false)
        {
            Console.WriteLine("Room is not available.");
            Console.WriteLine();
            CheckInGuest(room_List, guests_List);
            return;
        }

        if (room is StandardRoom)
        {
            StandardRoom s_room = (StandardRoom)room.Clone();
            Console.Write("Requires Wifi [Y/N]");
            string wifioptions = Console.ReadLine();
            if(wifioptions.ToLower() == "y")
            {
                s_room.RequireWifi = true;
            }
            else if(wifioptions.ToLower() == "n")
            {
                s_room.RequireWifi = false;
            }
            else
            {
                Console.WriteLine("Invalid option.");
                continue;
            }
            Console.Write("Requires Breakfast [Y/N]");
            string brkoptions = Console.ReadLine();
            if (brkoptions.ToLower() == "y")
            {
                s_room.RequireBreakfast = true;
            }
            else if (brkoptions.ToLower() == "n")
            {
                s_room.RequireBreakfast = false;
            }
            else
            {
                Console.WriteLine("Invalid option.");
                continue;
            }
            room.IsAvail = false;
            s_room.IsAvail = false;
            stay.RoomList.Add(s_room);
        }
        else if (room is DeluxeRoom)
        {
            DeluxeRoom d_room = (DeluxeRoom)room.Clone();
            Console.Write("Requires additional bed [Y/N]: ");
            string bedoptions = Console.ReadLine();
            if (bedoptions.ToLower() == "y")
            {
                d_room.AdditionalBed = true;
            }
            else if (bedoptions.ToLower() == "n")
            {
                d_room.AdditionalBed = false;
            }
            else
            {
                Console.WriteLine("Invalid option.");
                continue;
            }
            room.IsAvail = false;
            d_room.IsAvail = false;
            stay.RoomList.Add(d_room);
        }
        Console.Write("Does guest want another room [Y/N]: ");
        string options = Console.ReadLine();
        if (options.ToLower() == "y")
        {
            continue;
        }
        else if (options.ToLower() == "n")
        {
            guest.HotelStay = stay;
            guest.IsCheckedin = true;
            Console.WriteLine("Check in Successfull");
            break;
        }
        else
        {
            Console.WriteLine("Invalid option.");
            continue;
        }

    }

}
//==========================================================
// Feature 5 
// 
//==========================================================
void StayDetails(List<Guest> guests_List)
{
    Console.WriteLine("Guest's Name   Passport Number   Checked In");
    Console.WriteLine("------------   ---------------   ----------");
    int i = 1;
    foreach (Guest g in guests_List)
    {
        Console.Write("[{0}] {1,-15}{2,-15}", i, g.Name, g.PassportNum);
        if (g.IsCheckedin == true)
        {
            Console.WriteLine("Yes");
        }
        else
        {
            Console.WriteLine("No");
        }
        i++;
    }
    Console.WriteLine("\nExit[x]\n");
    Console.Write("Select a guest number: ");
    string? temp = Console.ReadLine();

    try
    {
        Convert.ToInt32(temp);
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input! not number");
        StayDetails(guests_List);
        return;
    }
    if (Convert.ToInt32(temp) > guests_List.Count)
    {
        Console.WriteLine("Invalid input! too big");
        StayDetails(guests_List);
        return;
    }
    if (temp == "x")
    {
        return;
    }
    int no = Convert.ToInt32(temp) - 1;
    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)) + " Guest Information " + string.Concat(Enumerable.Repeat("-", 50)));
    foreach (Room r in guests_List[no].HotelStay.RoomList)
    {
        if (r is StandardRoom)
        {
            StandardRoom sr = (StandardRoom)r;
            Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}{7,-15}", "RoomNo", "BedConfig", "DailyRate", "Available", "Wifi", "Breakfast", "Check_In", "Check_Out");
            Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}{7,-15}", "------", "---------", "---------", "---------", "----", "---------", "--------", "---------");
            Console.Write("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}", sr.RoomNumber, sr.BedConfiguration, sr.DailyRate, sr.IsAvail, sr.RequireWifi, sr.RequireBreakfast);
        }
        if (r is DeluxeRoom)
        {
            DeluxeRoom dr = (DeluxeRoom)r;
            Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}", "RoomNo", "BedConfig", "DailyRate", "Available", "AddBed", "Check_In", "Check_Out");
            Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}", "------", "---------", "---------", "---------", "------", "--------", "---------");
            Console.Write("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}", dr.RoomNumber, dr.BedConfiguration, dr.DailyRate, dr.IsAvail, dr.AdditionalBed);
        }
        Console.WriteLine("{0,-15}{1,-15}", guests_List[no].HotelStay.CheckinDate.ToString("dd/MM/yyyy"), guests_List[no].HotelStay.CheckoutDate.ToString("dd/MM/yyyy"));
    }
}
//==========================================================
// Feature 6 
// 
//==========================================================
void ExtendDay(List<Guest> guests_List)
{
    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 32)) + " Extend Stay Day " + string.Concat(Enumerable.Repeat("-", 32)));
    Console.WriteLine("Guest's Name   Passport Number   Checked In");
    Console.WriteLine("------------   ---------------   ----------");
    foreach (Guest g in guests_List)
    {
        Console.Write("{0,-15}{1,-15}   ", g.Name, g.PassportNum);
        if(g.IsCheckedin == true)
        {
            Console.WriteLine("Yes");
        }
        else
        {
            Console.WriteLine("No");
        }
    }
    Console.WriteLine();
    Console.WriteLine("Exit [x]");
    Console.WriteLine();
    Console.Write("Enter Passport Number of guest to extend stay: ");
    string passport_num = Console.ReadLine();
    if (passport_num == "x")
    {
        return;
    }
    Guest guest = SearchGuest(passport_num, guests_List);
    if (guest == null)
    {
        Console.WriteLine("Guest not found.");
        ExtendDay(guests_List);
        return;
    }
    if (guest.IsCheckedin == false)
    {
        Console.WriteLine("Guest not checked in.");
        ExtendDay(guests_List);
        return;
    }
    Console.WriteLine("CheckInDate: {0}   CheckOutDate: {1}"
        ,guest.HotelStay.CheckinDate.ToString("dd/MM/yyyy"), guest.HotelStay.CheckoutDate.ToString("dd/MM/yyyy"));
    Console.Write("Enter number of day to extend: ");
    string extend_input = Console.ReadLine();
    try
    {
        Convert.ToInt32(extend_input);
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid Input.");
        Console.WriteLine();
        ExtendDay(guests_List);
        return;
    }
    int extend = Convert.ToInt32(extend_input);
    if (extend < 0)
    {
        Console.WriteLine("Invalid number.");
        ExtendDay(guests_List);
        return;
    }
    guest.HotelStay.CheckoutDate = guest.HotelStay.CheckoutDate.AddDays(extend);
    Console.WriteLine("CheckInDate: {0}   CheckOutDate: {1}"
       , guest.HotelStay.CheckinDate.ToString("dd/MM/yyyy"), guest.HotelStay.CheckoutDate.ToString("dd/MM/yyyy"));
    Console.WriteLine("Stay date extended.");

}
//==========================================================
// Advance Feature A
// Made By : Wei Lun
//
//==========================================================
void DisplayCharged(List<Guest> guests_List)
{
    var month = new Dictionary<string, double> { { "Jan", 0.0 }, { "Feb", 0 }, { "Mar", 0 }, { "Apr", 0 },
        { "May", 0 }, { "Jun", 0 }, { "Jul", 0 }, { "Aug", 0 }, 
        { "Sep", 0 }, { "Oct", 0 }, { "Nov", 0 }, { "Dec", 0 } };
    Console.WriteLine();
    Console.WriteLine("---------- Display Charges for year ----------");
    Console.Write("Enter the year: ");
    string year_input = Console.ReadLine();
    try
    {
        Convert.ToInt32(year_input);
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid Input.");
        Console.WriteLine();
        DisplayCharged(guests_List);
        return;
    }
    Console.WriteLine();
    int year = Convert.ToInt32(year_input); 
    foreach (Guest g in guests_List)
    {
        if(g.HotelStay.CheckoutDate.Year == year)
        {
            int month_value = g.HotelStay.CheckoutDate.Month - 1;
            string key = month.ElementAt(month_value).Key;

            month[key] += g.HotelStay.CalculateTotal();
            
        }
    }
    double total_charge = 0;
    foreach(KeyValuePair<string, double> m in month)
    {
        Console.WriteLine("{0} {1}:     ${2}", m.Key, year, m.Value);
        total_charge += m.Value;
    }
    Console.WriteLine();
    Console.WriteLine("Total   :     ${0}",total_charge);


}

//==========================================================
// Advance Feature B
// 
//==========================================================
void CheckOut(List<Guest> guests_List, List<Room> room_list)
{
    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 32)) + " Check-Out Guest " + string.Concat(Enumerable.Repeat("-", 32)));
    Console.WriteLine("Guest's Name   Passport Number");
    Console.WriteLine("------------   ---------------");

    foreach (Guest g in guests_List)
    {
        if (g.IsCheckedin is true)
        {
            Console.WriteLine("{0,-15}{1,-15}", g.Name, g.PassportNum);
        }
    }

    Console.WriteLine("\nExit [x]\n");
    Console.Write("Enter Passport Number of guest to check out: ");
    string passport_num = Console.ReadLine();
    if (passport_num == "x")
    {
        return;
    }

    Guest guest = SearchGuest(passport_num, guests_List);
    if (guest == null)
    {
        Console.WriteLine("Guest not found.");
        CheckOut(guests_List, room_List);
        return;
    }
    if (guest.IsCheckedin == false)
    {
        Console.WriteLine("This Guest is already checked out");
        CheckInGuest(room_List, guests_List);
        return;
    }

    double totalbill = guest.HotelStay.CalculateTotal();

    Console.WriteLine("\nTotal Bill: $" + totalbill);
    Console.WriteLine("\nStatus   Points");
    Console.WriteLine("------   ------");
    Console.WriteLine("{0,-9}{1,-10}", guest.Member.Status, guest.Member.Points);
    while (true)
    {
        if (guest.Member.Status == "Silver" || guest.Member.Status == "Gold")
        {
            Console.Write("Do you want to redeem your points? (Y/N): ");
            string? yn = Console.ReadLine();
            if (yn.ToLower() == "y")
            {
                Console.WriteLine("\nExit [x]\n");
                Console.Write("How much points to redeem: ");
                string? temp = Console.ReadLine();
                if (temp == "x")
                {
                    return;
                }
                try
                {
                    Convert.ToInt32(temp);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input.\n");
                    CheckOut(guests_List, room_List);
                    return;
                }

                int redeem = Convert.ToInt32(temp);

                if (guest.Member.RedeemPoints(redeem) == true)
                {
                    Console.WriteLine("{0} Points have been redeemed. Your Balance is {1}", redeem, guest.Member.Points);
                    totalbill -= redeem;
                }

                else
                {
                    Console.WriteLine("Not enough points!");
                    CheckOut(guests_List, room_List);
                    return;
                }
            }

            else if (yn.ToLower() == "n")
            {
                Console.WriteLine("No points have been redeemed.");
            }
            else
            {
                Console.WriteLine("Invalid Input.");
                continue;
            }
        }

        else if (guest.Member.Status == "Ordinary")
        {
            Console.WriteLine("Redeem points feature only available for Silver/Gold Members");
        }

        Console.WriteLine("Final Bill: $" + totalbill);
        Console.Write("Enter P to make payment (enter X to exit): ");
        string? p = Console.ReadLine();

        if (p.ToUpper() == "P")
        {
            guest.IsCheckedin = false;
            foreach(Room r in guest.HotelStay.RoomList)
            {
                r.IsAvail = true;
                SearchRoom(r.RoomNumber, room_List).IsAvail = true;
            }
            Console.WriteLine("Payment Successful! You have been checked out");
            guest.Member.EarnPoints(totalbill);

            if (guest.Member.Status == "Ordinary" & guest.Member.Points >= 100)
            {
                guest.Member.Status = "Silver";
                Console.WriteLine("Your Membership Status has upgraded from Ordinary to Silver!");
            }
            if (guest.Member.Status == "Silver" & guest.Member.Points >= 200)
            {
                guest.Member.Status = "Gold";
                Console.WriteLine("Your Membership Status has upgraded from Silver to Gold!");
            }
            return;
        }
        else if (p.ToUpper() == "X")
        {
            Console.WriteLine("Payment cancelled");
            return;
        }
        else
        {
            Console.WriteLine("Invalid Input,");
            continue;
        }
    }
}
//==========================================================
// Bonus Feature 1 
// 
//==========================================================
var staff_List = new List<Staff> { };
void Intializestaff(List<Staff> staff_List)
{
    string[] staff_data = File.ReadAllLines("Staff.csv");
    for (int i = 1; i < staff_data.Length; i++)
    {
        string[] line = staff_data[i].Split(',');
        string name = line[0];
        int id = Convert.ToInt32(line[1]);
        string password = line[2];
        string email = line[3];
        DateTime datejoined = Convert.ToDateTime(line[4]);

        Staff staff = new Staff(name,id,password,email,datejoined);
        staff_List.Add(staff);
    }
}

Staff SearchStaff(int id,List<Staff> staff_List)
{
    foreach(Staff s in staff_List)
    {
        if (s.Id == id)
        {
            return s;
        }
    }
    return null;
}

void Addstaff(List<Staff> staff_List)
{
    Console.WriteLine("---------- Add Staff ----------");
    Console.Write("Enter name: ");
    string name = Console.ReadLine();
    Console.Write("Enter email: ");
    string email = Console.ReadLine();
    int id = 0;
    while (true)
    {
        Random rnd = new Random();
        int random = rnd.Next(10000, 100000);
        if (SearchStaff(id, staff_List) == null)
        {
            id = random;
            break;
        }
        else
        {
            continue;
        }
    }
    Console.WriteLine(id + " is your generated Staff ID");
    Console.Write("Create password (must be more than 8 characters): ");
    string password = Console.ReadLine();
    if (password.Length < 8)
    {
        Console.WriteLine("Password must be 8 characters");
        Addstaff(staff_List);
        return;
    }

    Staff newstaff = new Staff(name, id, password, email, DateTime.Now);
    staff_List.Add(newstaff);
    var staffinfo = new List<string> { "Name,StaffId,Password,Email,DateJoined" };
    foreach (Staff s in staff_List)
    {
        string info = string.Format("{0},{1},{2},{3},{4}", s.Name, Convert.ToString(s.Id), s.Password, s.Email, s.Datejoined.ToString("dd/MM/yyyy"));
        staffinfo.Add(info);
    }
    File.WriteAllLines("Staff.csv", staffinfo);
    Console.WriteLine("Staff successfully registered");
}

void Login(List<Staff> staff_List, List<Room> room_List, List<Guest> guests_List)
{
    Console.WriteLine("---------- Staff's Login ----------");
    Console.Write("StaffID: ");
    string id_input = Console.ReadLine();
    try
    {
        Convert.ToInt32(id_input);
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid ID.");
        Console.WriteLine();
        Login(staff_List, room_List, guests_List);
        return;
    }
    int id = Convert.ToInt32(id_input);
    Staff staff = SearchStaff(id, staff_List);
    if (staff == null)
    {
        Console.WriteLine("Staff ID Not Found.");
        Console.WriteLine();
        Login(staff_List, room_List, guests_List);
        return;
    }
    Console.Write("Password: ");
    string password = Console.ReadLine();
    if (staff.Password != password)
    {
        Console.WriteLine("Incorrect Password.");
        Console.WriteLine();
        Login(staff_List, room_List, guests_List);
        return;
    }
    Console.WriteLine("Hi {0}, you are logged in.", staff.Name);
    Console.WriteLine();
    Console.WriteLine("---------- Hotel Management System ----------");
    Console.WriteLine("[1] Guest Management System");
    Console.WriteLine("[2] Register Staff");
    Console.WriteLine();
    Console.WriteLine("[x] Exit");
    Console.Write("Enter option: ");
    string option = Console.ReadLine();
    if(option == "1")
    {
        Menu(room_List, guests_List);
        return;
    }
    else if (option == "2")
    {
        Addstaff(staff_List);
        Login(staff_List, room_List, guests_List);
        return;
    }
    else if (option == "x")
    {
        Console.WriteLine("Logged out.");
        return;
    }
    else
    {
        Console.WriteLine("Invalid options.");
        Login(staff_List, room_List, guests_List);
        return;
    }

}

//==========================================================
// Menu
// 
//==========================================================
void Menu(List<Room> room_List, List<Guest> guests_List)
{
    Console.WriteLine();
    Console.WriteLine("-------------------- Menu --------------------");
    Console.WriteLine("[1] List all guests");
    Console.WriteLine("[2] List all available rooms");
    Console.WriteLine("[3] Register guest");
    Console.WriteLine("[4] Check in guest");
    Console.WriteLine("[5] Display stay detail of guest");
    Console.WriteLine("[6] Extend stay of guest");
    Console.WriteLine("[7] Display charges");
    Console.WriteLine("[8] Check out guest");
    Console.WriteLine();
    Console.WriteLine("[x] Exit");
    Console.WriteLine();
    Console.Write("Enter option: ");
    string option = Console.ReadLine();
    if (option == "1")
    {
        ListGuest(guests_List);
        Menu(room_List, guests_List);
        return;
    }
    else if (option == "2")
    {
        ListAvailRoom(room_List);
        Menu(room_List, guests_List);
        return;
    }
    else if (option == "3")
    {
        RegisterGuest(guests_List);
        Menu(room_List, guests_List);
        return;
    }
    else if (option == "4")
    {
        CheckInGuest(room_List, guests_List);
        Menu(room_List, guests_List);
        return;
    }
    else if (option == "5")
    {
        StayDetails(guests_List);
        Menu(room_List, guests_List);
        return;
    }
    else if (option == "6")
    {
        ExtendDay(guests_List);
        Menu(room_List, guests_List);
        return;
    }
    else if (option == "7")
    {
        DisplayCharged(guests_List);
        Menu(room_List, guests_List);
        return;
    }
    else if (option == "8")
    {
        CheckOut(guests_List, room_List);
        Menu(room_List, guests_List);

    }
    else if (option == "x")
    {
        return;
    }
    else
    {
        Console.WriteLine("Invalid options.");
        Menu(room_List, guests_List);
        return;
    }
}

Intializestaff(staff_List);
InitializeList(room_List, guests_List);
InitializeStayData(room_List, guests_List);
CheckAvailRoom(room_List, guests_List);
Login(staff_List, room_List, guests_List);

