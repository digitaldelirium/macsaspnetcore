using System;
using System.Collections.Generic;
using System.Linq;

namespace macsaspnetcore.Models
{
    public static class ActivityDbContextSeedData
    {

        public static void Initialize(ActivityDbContext context)
        {
            if (!context.Calendars.Any())
            {
                // Add new data
                var campCalendar = new Calendar()
                {
                    Year = DateTime.UtcNow.Year,
                    Activities = new Activity[] {
                            new Activity() {
                                StartTime = new DateTime(2016, 5, 14, 18, 0, 0), EndTime = new DateTime(2016, 5, 14, 20, 00, 00), ActivityTitle = "\"Meet & Greet\"", ActivityDescription = "Welcome Back! Come meet your camp friends. Enjoy homemade chili, and enter in a chance to win a “Free Visitor’s Pass” along with other raffles. You must be present to win. Let’s kick off our season!"
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 5, 21, 10, 30, 0), EndTime = new DateTime(2016, 5, 21, 14, 0, 0), ActivityTitle = "Docks in the Water", ActivityDescription = "We need your help! See Doug!"
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 5, 28, 18, 0, 0), EndTime = new DateTime(2016, 5, 28, 22, 0, 0), ActivityTitle = "Fish Fry", ActivityDescription = "Fresh haddock, homemade French fries, Doug’s famous coleslaw, roll, butter & dessert. There will be boneless fried chicken fingers for those who don’t like fish. <br /><p class='text-success'><em>We will need you to sign up for this dinner in advance to get a proper head count.</em></p>"
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 5, 29, 9, 0, 0), EndTime = new DateTime(2016, 5, 29, 11, 0, 0), ActivityTitle = "Breakfast Potluck", ActivityDescription = "This has been a great hit in seasons past. Please sign up for a variety of breakfast foods that you will prepare and share with your camp friends. Your creativity will make for a yummy breakfast."
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 6, 4, 7, 00, 0), EndTime = new DateTime(2016, 6, 4, 7, 30, 0), ActivityTitle = "Complimentary Continental Breakfast", ActivityDescription = "at the docks before the Fishing Derby"
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 6, 4, 7, 30, 0), EndTime = new DateTime(2016, 6, 4, 11, 0, 0), ActivityTitle = "Fishing Derby", ActivityDescription = "Come try your luck catching the biggest fish (one trophy awarded & several prizes). You may use row boats, kayaks, or canoes for this catch and release event--no motorized boats!"
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 6, 4, 18, 0, 0), EndTime = new DateTime(2016, 6, 4, 20, 0, 0), ActivityTitle = "Italian Potluck Dinner", ActivityDescription = "Bring your favorite dish to share with your camp family/friends."
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 6, 11, 19, 0, 0), EndTime = new DateTime(2016, 6, 11, 22, 0, 0), ActivityTitle = "Bingo", ActivityDescription = "Bingo in the Rec Hall!<h5 style='text-align: center; background-color: green;'>Cards will be 1 for $1.00 or 6 for $5.00</h5>"
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 6, 12, 8, 0, 0), EndTime = new DateTime(2016, 6, 12, 10, 0, 0), ActivityTitle = "Mac's Traditional Breakfast", ActivityDescription = "Choose from scrambled eggs, fried eggs, or French toast, served with home fries, bacon or sausage, toast, juice and coffee."
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 6, 25, 18, 0, 0), EndTime = new DateTime(2016, 6, 25, 22, 0, 0), ActivityTitle = "Auction", ActivityDescription = "Back by popular demand! We will have Doug as our auctioneer. We are looking for any donated items: anything new, handmade, antique, items for camp, anything in good working condition. We will also have a couple of mystery boxes to take a chance on. This has been a lot of fun in the past. All proceeds will go right back into the activities fund! Scramble burgers served."
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 7, 2, 9, 0, 0), EndTime = new DateTime(2016, 7, 3, 20, 0, 0), ActivityTitle = "Horseshoe Tournament", ActivityDescription = "See Doug for details.<br />Hopefully, there will be enough guys around to play!"
                            },
                            new Activity {
                                StartTime = new DateTime(2016, 7, 3, 19, 0, 0), EndTime = new DateTime(2016, 7, 3, 21, 0, 0), ActivityTitle = "Bingo", ActivityDescription = "If there are enough campers that want to play.<h5 style='text-align: center; background-color: green;'>Cards will be 1 for $1.00 or 6 for $5.00</h5>"
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 7, 3, 20, 30, 0), EndTime = new DateTime(2016, 7, 3, 23, 59, 59), ActivityTitle = "3rd of July Fireworks", ActivityDescription = "Fireworks display at dark!"
                            },
                            new Activity(){
                                StartTime = new DateTime(2016, 7, 9, 18, 0, 0), EndTime = new DateTime(2016, 7, 9, 21, 0, 0), ActivityTitle = "Camp Around the World", ActivityDescription = "Our bi-annual Camp Around the World is a popular event! <br />Each camper that wants to participate will choose a country theme (such as Mexico, China, Italy, etc.) You can play games, play music, make appetizer, drinks or dessert, from that country. Each group will change stations(campsites) so everyone will get a chance to taste or experience the whole world. See Janice to sign up for your country."
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 7, 17, 9, 0, 0), EndTime = new DateTime(2016, 7, 9, 11, 0, 0), ActivityTitle = "Breakfast Potluck", ActivityDescription = "This is always a great hit with most campers. Please sign up for a variety of breakfast foods that you will prepare and share with your camp friends. Your creativity will make for a yummy breakfast."
                            },
                            new Activity(){
                                StartTime = new DateTime(2016, 7, 23, 14, 0, 0), EndTime = new DateTime(2016, 7, 23, 18, 0, 0), ActivityTitle = "Water Works", ActivityDescription = "You will want to plan to bring your kids/grandkids for this fun filled day! This will be mostly water related fun! Games, relays and a water slide/jumpy house! We are very excited to try something new. <br />This will be held at the swimming area. More details will follow."
                            },
                            new Activity(){
                                StartTime = new DateTime(2016, 8, 6, 18, 0, 0), EndTime = new DateTime(2016, 8, 6, 22, 0, 0), ActivityTitle = "Hawaiian Luau Potluck", ActivityDescription = "We would love to hire a company to roast a pig for us. At very least we will provide the pork. So come dressed in your Hawaiian shirts and your grass skirts garb. You will be showing off your hula hoop talent, show us how you can still limbo. More details later."
                            },
                            new Activity {
                                StartTime = new DateTime(2016, 8, 13, 19, 0, 0), EndTime = new DateTime(2016, 8, 13, 22, 0, 0), ActivityTitle = "Bingo", ActivityDescription = "Bingo in the Rec Hall!<h5 style='text-align: center; background-color: green;'>Cards will be 1 for $1.00 or 6 for $5.00</h5>"
                            },
                            new Activity() {
                                StartTime = new DateTime(2016, 8, 14, 8, 0, 0), EndTime = new DateTime(2016, 8, 14, 10, 0, 0), ActivityTitle = "Mac’s Traditional Breakfast", ActivityDescription =  "Choose from scrambled eggs, fried eggs, or French toast, served with home fries, bacon or sausage, toast, juice and coffee."
                            },
                            new Activity(){
                                StartTime = new DateTime(2016, 8, 20, 18, 0, 0), EndTime = new DateTime(2016, 8, 20, 20, 0, 0), ActivityTitle = "Fallin’ Off the Bone Ribs & Chicken Barbeque Dinner", ActivityDescription = "1/4 rack of ribs, chicken leg, potato, cole slaw, corn on the cob & dessert. <br /><p class='text-success'><em>We will need you to sign up for this dinner in advance to get a proper head count.</em></p>"
                            },
                            new Activity(){
                                StartTime = new DateTime(2016, 9, 3, 12, 30, 0), EndTime = new DateTime(2016, 9, 3, 17, 0, 0), ActivityTitle = "Liz and Lynn's Campers Pool Potluck", ActivityDescription = "Please make sure you sign the potluck sheet. It will help her out.<br />She will have the campers pool and surprises throughout the day. <br />Come and join the fun! More details will follow."
                            },
                            new Activity(){
                               StartTime = new DateTime(2016, 9, 11, 9, 0, 0), EndTime = new DateTime(2016, 9, 11, 18, 0, 0), ActivityTitle = "Docks Come Out of the Water", ActivityDescription = "See Doug for details. <br />The docks are getting heavier and heavier every year. <br />Please try and be here to help get them out of the water."
                            },
                            new Activity(){
                                StartTime = new DateTime(2016, 10, 7), EndTime = new DateTime(2016, 10, 10), ActivityTitle = "Last Full Weekend of Camping", ActivityDescription = "It's been another great year at Mac's. We can't wait to see you all next year!"
                            },
                            new Activity(){
                                StartTime = new DateTime(2016, 10, 16), EndTime = new DateTime(2016, 10, 16, 23, 59, 59), ActivityTitle = "Closing Down Camp", ActivityDescription = "Winterize the campground, blow out pipes, etc."
                            },
                            new Activity(){
                                StartTime = new DateTime(2016, 10, 22, 9, 0, 0), EndTime = new DateTime(2016, 10, 22, 23, 59, 59), ActivityTitle = "Fall Cleanup", ActivityDescription = "Food will be provided for your hard work. <br />Rain date: Oct 29th"
                            }
                        }
                };

                context.Calendars.Add(campCalendar);
                context.Activities.AddRange(campCalendar.Activities);

                context.SaveChanges();
            }
        }
    }
}