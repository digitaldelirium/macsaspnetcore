BEGIN TRANSACTION;
CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId" NVARCHAR(50) NOT NULL CONSTRAINT "PK_HistoryRow" PRIMARY KEY,
    "ProductVersion" NVARCHAR(50) NOT NULL
);
INSERT INTO [__EFMigrationsHistory] (MigrationId,ProductVersion) VALUES ('20160424033250_InitializeActivities','7.0.0-rc1-16348');
INSERT INTO [__EFMigrationsHistory] (MigrationId,ProductVersion) VALUES ('20160424155214_FixedActivityDataAnnotations','7.0.0-rc1-16348');
CREATE TABLE "Calendar" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Calendar" PRIMARY KEY IDENTITY,
    "Year" DATE NOT NULL
);
INSERT INTO [Calendar] (Id,YEAR ) VALUES (1,2016);
INSERT INTO [Calendar] (Id,YEAR ) VALUES (1,2017);
CREATE TABLE "Activity" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Activity" PRIMARY KEY IDENTITY,
    "ActivityDescription" TEXT,
    "ActivityTitle" TEXT,
    "CalendarId" INTEGER,
    "EndTime" DATE NOT NULL,
    "StartTime" DATE NOT NULL,
    CONSTRAINT "FK_Activity_Calendar_CalendarId" FOREIGN KEY ("CalendarId") REFERENCES "Calendar" ("Id") ON DELETE CASCADE
);
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (1,'Welcome Back! Come meet your camp friends. Enjoy homemade chili, and enter in a chance to win a “Free Visitor’s Pass” along with other raffles. You must be present to win. Let’s kick off our season!','"Meet & Greet"',1,'2016-05-14 20:00:00','2016-05-14 18:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (2,'It''s been another great year at Mac''s. We can''t wait to see you all next year!','Last Full Weekend of Camping',1,'2016-10-10 00:00:00','2016-10-07 00:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (3,'See Doug for details. <br />The docks are getting heavier and heavier every year. <br />Please try and be here to help get them out of the water.','Docks Come Out of the Water',1,'2016-09-11 18:00:00','2016-09-11 09:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (4,'Please make sure you sign the potluck sheet. It will help her out.<br />She will have the campers pool and surprises throughout the day. <br />Come and join the fun! More details will follow.','Liz and Lynn''s Campers Pool Potluck',1,'2016-09-03 17:00:00','2016-09-03 12:30:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (5,'¼ rack of ribs, chicken leg, potato, cole slaw, corn on the cob & dessert. <br /><p class=''text-success''><em>We will need you to sign up for this dinner in advance to get a proper head count.</em></p>','Fallin’ Off the Bone Ribs & Chicken Barbeque Dinner',1,'2016-08-20 20:00:00','2016-08-20 18:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (6,'Choose from scrambled eggs, fried eggs, or French toast, served with home fries, bacon or sausage, toast, juice and coffee.','Mac’s Traditional Breakfast',1,'2016-08-14 10:00:00','2016-08-14 08:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (7,'Bingo in the Rec Hall!<h5 style=''text-align: center; background-color: green;''>Cards will be 1 for $1.00 or 6 for $5.00</h5>','Bingo',1,'2016-08-13 22:00:00','2016-08-13 19:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (8,'We would love to hire a company to roast a pig for us. At very least we will provide the pork. So come dressed in your Hawaiian shirts and your grass skirts garb. You will be showing off your hula hoop talent, show us how you can still limbo. More details later.','Hawaiian Luau Potluck',1,'2016-08-06 22:00:00','2016-08-06 18:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (9,'You will want to plan to bring your kids/grandkids for this fun filled day! This will be mostly water related fun! Games, relays and a water slide/jumpy house! We are very excited to try something new. <br />This will be held at the swimming area. More details will follow.','Water Works',1,'2016-07-23 18:00:00','2016-07-23 14:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (10,'This is always a great hit with most campers. Please sign up for a variety of breakfast foods that you will prepare and share with your camp friends. Your creativity will make for a yummy breakfast.','Breakfast Potluck',1,'2016-07-09 11:00:00','2016-07-17 09:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (11,'Our bi-annual Camp Around the World is a popular event! <br />Each camper that wants to participate will choose a country theme (such as Mexico, China, Italy, etc.) You can play games, play music, make appetizer, drinks or dessert, from that country. Each group will change stations(campsites) so everyone will get a chance to taste or experience the whole world. See Janice to sign up for your country.','Camp Around the World',1,'2016-07-09 21:00:00','2016-07-09 18:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (12,'Winterize the campground, blow out pipes, etc.','Closing Down Camp',1,'2016-10-16 23:59:59','2016-10-16 00:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (13,'Fireworks display at dark!','3rd of July Fireworks',1,'2016-07-03 23:59:59','2016-07-03 20:30:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (14,'See Doug for details.<br />Hopefully, there will be enough guys around to play!','Horseshoe Tournament',1,'2016-07-03 20:00:00','2016-07-02 09:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (15,'Back by popular demand! We will have Doug as our auctioneer. We are looking for any donated items: anything new, handmade, antique, items for camp, anything in good working condition. We will also have a couple of mystery boxes to take a chance on. This has been a lot of fun in the past. All proceeds will go right back into the activities fund! Scramble burgers served.','Auction',1,'2016-06-25 22:00:00','2016-06-25 18:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (16,'Choose from scrambled eggs, fried eggs, or French toast, served with home fries, bacon or sausage, toast, juice and coffee.','Mac''s Traditional Breakfast',1,'2016-06-12 10:00:00','2016-06-12 08:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (17,'Bingo in the Rec Hall!<h5 style=''text-align: center; background-color: green;''>Cards will be 1 for $1.00 or 6 for $5.00</h5>','Bingo',1,'2016-06-11 22:00:00','2016-06-11 19:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (18,'Bring your favorite dish to share with your camp family/friends.','Italian Potluck Dinner',1,'2016-06-04 20:00:00','2016-06-04 18:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (19,'Come try your luck catching the biggest fish (one trophy awarded & several prizes). You may use row boats, kayaks, or canoes for this catch and release event--no motorized boats!','Fishing Derby',1,'2016-06-04 11:00:00','2016-06-04 07:30:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (20,'at the docks before the Fishing Derby','Complimentary Continental Breakfast',1,'2016-06-04 07:30:00','2016-06-04 07:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (21,'This has been a great hit in seasons past. Please sign up for a variety of breakfast foods that you will prepare and share with your camp friends. Your creativity will make for a yummy breakfast.','Breakfast Potluck',1,'2016-05-29 11:00:00','2016-05-29 09:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (22,'Fresh haddock, homemade French fries, Doug’s famous coleslaw, roll, butter & dessert. There will be boneless fried chicken fingers for those who don’t like fish. <br /><p class=''text-success''><em>We will need you to sign up for this dinner in advance to get a proper head count.</em></p>','Fish Fry',1,'2016-05-28 22:00:00','2016-05-28 18:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (23,'We need your help! See Doug!','Docks in the Water',1,'2016-05-21 14:00:00','2016-05-21 10:30:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (24,'If there are enough campers that want to play.<h5 style=''text-align: center; background-color: green;''>Cards will be 1 for $1.00 or 6 for $5.00</h5>','Bingo',1,'2016-07-03 21:00:00','2016-07-03 19:00:00');
INSERT INTO [Activity] (Id,ActivityDescription,ActivityTitle,CalendarId,EndTime,StartTime) VALUES (25,'Food will be provided for your hard work. <br />Rain date: Oct 29th','Fall Cleanup',1,'2016-10-22 23:59:59','2016-10-22 09:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES (26, '2', 'Complimentary Continental Breakfast', 'at the docks before the Fishing Derby', '2017-06-03T07:00:00',
   '2017-06-03T08:30:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (27,'2', 'Fishing Derby! <h5 style=''text-align: center; background-color: green;''>Free Fishing Day!!</h5>',
   'Come try your luck catching the biggest fish (One trophy awarded & several prizes). You may use row boats, kayaks, or your canoe for this catch and release event. <em>No motorized Boats!</em></br>Let''s Keep the girl power going!',
   '2017-06-03T07:30:00', '2017-06-03T11:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime)
VALUES (28,'2', 'Cornhole Game Tournament', 'See Teri Gorham for details!', '2017-06-03T13:00:00', '2017-06-03T17:00:00');
INSERT INTO [Activity] (CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  ('2', 'Potluck Dinner', 'Bring your favorite dish to share with your camp family/friends', '2017-06-03T18:00:00',
   '2017-06-03T20:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (29, '2', 'Mac''s Traditional Breakfast',
   'Choose from scrambled eggs, fried eggs, or French toast, home fries, choice of bacon or sausage, juice & coffee. <h5 style=''text-align: center; background-color: green;''>Adults $5.00 children 12 & under $4.00</h5>',
   '2017-06-03T08:00:00', '2017-06-03T10:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (30, '2', 'Horseshoe Tournament',
   'Either the 1st or 2nd. <em>See Doug for details!</em>Hopefully, there will be enough guys around to play!',
   '2017-07-01T10:00:00', '2017-07-02T20:30:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (31,'2', 'Fireworks at dark!', 'This will depend on who''s around!,  It could be the 1st or 4th', '2017-07-01T21:00:00',
   '2017-07-01T23:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (32,'2', 'Fireworks at dark!', 'This will depend on who''s around!,  It could be the 1st or 4th', '2017-07-04T21:00:00',
   '2017-07-04T23:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (33,'2', 'Minute to Win It',
   'We will be spending the afternoon with the kids playing ridiculously fun, awesome and easy games! From Cookie Face to making a pyramid out of thirty-six cups, and much more! So calling all our kids, grand kids and friends.',
   '2017-07-08T14:00:00', '2017-07-08T17:00:00');
INSERT INTO [Activity] (CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  ('2', 'Breakfast Potluck',
   'This is always a great hit with most campers. Please sign up for a variety of breakfast foods that you will prepare and share with your camp friends. Your creativity will make for a yummy breakfast.',
   '2017-07-16T09:00:00', '2017-07-16T11:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES (34,'2', 'Fish Fry',
                                                                                                    'Fresh haddock, homemade French fries, Doug''s famous coleslaw, roll, butter & dessert. There will be boneless fried chicken fingers for those who don''t like fish. <h5 style=''text-align: center; background-color: green;''>Adults $12.00 Children 12 & under $8.00</h5>< br/><p class=''text-success''><em>To get a proper headcount, we will need you to sign up & pay for this meal in advance!</em></p>',
                                                                                                    '2017-07-22T18:00:00',
                                                                                                    '2017-07-22T20:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (35, '2', 'Songo River Queen "Golden Oldies Cruise"',
   'anyone who would like to join Mac''s Camping Area on this cruise can do so by pre buying your tickets on their web site songoriverqueen.com under the tab special theme cruises. There is a Disc jockey featuring 50''s and 60''s music!<h5 style=''text-align: center; background-color: green;''>Tickets are $25.00 per person</h5>There is a cash bar available.  We could also go out to dinner in Naples if you''d like.',
   '2017-07-29T19:30:00', '2017-07-29T21:30:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (36, '2', 'Country Western Hoedown Potluck',
   'Don''t forget your country western attire because we have hired a band for your entertainment after dinner by country band "Cold Blue Steel" (formerly "Emerald Sky"). More information will follow.',
   '2017-08-05T18:00:00', '2017-08-05T22:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (37, '2', 'Songo River Queen Day Cruise',
   'This a replica of the Mississippi River Paddle Boat. If, enough campers would like to go on this two hour cruise through Long Lake. We can get a little bit of a discount if 25 or more people are interested.<h5 style=''text-align:center; background-color:green;''>Adults $25.00 child 12 & under $13.00 and under 4 is free.</h5>',
   '2017-08-12T12:00:00', '2017-08-12T14:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime)
VALUES (38, '2', 'Traditional Breakfast', 'A Mac''s Tradition!', '2017-08-13T08:00:00', '2017-08-13T10:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (39, '2', 'Fallin'' Off the Rib & Chicken Barbecue Dinner',
   '1/4 rack of ribs, chicken leg, potato, cole slaw, corn on the cob & dessert. <br /><p class=''text-success''><em>We will need you to sign up and pay for this dinner in advance to get a proper head count.</em></p>',
   '2017-08-19T18:00:00', '2017-08-19T21:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (40,'2', 'Liz & Lynn''s Camper''s Pool Potluck',
   'Please make sure you sign the potluck sheet. It will help her out. She will have the campers pool and surprises throughout the day. Come and join the fun! More details will follow.',
   '2017-09-02T17:00:00', '2017-09-02T20:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (41,'2', 'Docks Come Out of the Water',
   'The docks are getting heavier and heavier every year. Please try and be here to help get them out of the water.',
   '2017-09-09T00:00:00', '2017-09-09T23:59:59');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime)
VALUES (42,'2', 'Last Full Weekend of Camping', NULL, '2017-10-06T00:00:00', '2017-10-09T23:59:59');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime)
VALUES (43, '2', 'Closing Down Camp', NULL, '2017-10-14T09:00:00', '2017-10-15T19:00:00');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (44,'2', 'Fall Cleanup', 'Food will be provided for your hard work. Until it''s done!  Rain date is October 31st.',
   '2017-10-28T09:00:00', '2017-10-28T23:59:59');
INSERT INTO [Activity] (Id, CalendarId, ActivityTitle, ActivityDescription, StartTime, EndTime) VALUES
  (45,'2', 'Fall Cleanup Rain Date (if necessary)',
   'Food will be provided for your hard work. Until it''s done!  Rain date is October 31st.', '2017-10-31T09:00:00',
   '2017-10-31T23:59:59');
COMMIT;
