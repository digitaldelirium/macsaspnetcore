create table Activities
(
    Id                  int auto_increment
        primary key,
    ActivityDescription longtext    null,
    ActivityTitle       longtext    null,
    CalendarId          int         null,
    EndTime             datetime(6) not null,
    StartTime           datetime(6) not null,
    constraint FK_Activities_Calendars_CalendarId
        foreign key (CalendarId) references Calendars (Id)
);

create index IX_Activities_CalendarId
    on Activities (CalendarId);

INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (3, 'at the docks before the Fishing Derby', 'Complimentary Continental Breakfast', 5, '2017-06-03 08:30:00.000000', '2017-06-03 07:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (4, 'Come try your luck catching the biggest fish (One trophy awarded & several prizes). You may use row boats, kayaks, or your canoe for this catch and release event. <em>No motorized Boats!</em></br>Let''s Keep the girl power going!', 'Fishing Derby! <h5 style=''text-align: center; background-color: green;''>Free Fishing Day!!</h5>', 5, '2017-06-03 11:00:00.000000', '2017-06-03 07:30:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (5, 'See Teri Gorham for details!', 'Cornhole Game Tournament', 5, '2017-06-03 17:00:00.000000', '2017-06-03 13:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (6, 'Bring your favorite dish to share with your camp family/friends', 'Potluck Dinner', 5, '2017-06-03 20:00:00.000000', '2017-06-03 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (7, 'Choose from scrambled eggs, fried eggs, or French toast, home fries, choice of bacon or sausage, juice & coffee. <h5 style=''text-align: center; background-color: green;''>Adults $5.00 children 12 & under $4.00</h5>', 'Mac''s Traditional Breakfast', 5, '2017-06-03 10:00:00.000000', '2017-06-03 08:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (8, 'Either the 1st or 2nd. <em>See Doug for details!</em>Hopefully, there will be enough guys around to play!', 'Horseshoe Tournament', 5, '2017-07-02 20:30:00.000000', '2017-07-01 10:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (10, '', 'Fireworks at dark!', 5, '2017-07-07 23:00:00.000000', '2017-07-07 21:30:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (11, 'We will be spending the afternoon with the kids playing ridiculously fun, awesome and easy games! From Cookie Face to making a pyramid out of thirty-six cups, and much more! So calling all our kids, grand kids and friends.', 'Minute to Win It', 5, '2017-07-08 17:00:00.000000', '2017-07-08 14:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (12, 'This is always a great hit with most campers. Please sign up for a variety of breakfast foods that you will prepare and share with your camp friends. Your creativity will make for a yummy breakfast.', 'Breakfast Potluck', 5, '2017-07-16 11:00:00.000000', '2017-07-16 09:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (13, 'Fresh haddock, homemade French fries, Doug''s famous coleslaw, roll, butter & dessert. There will be boneless fried chicken fingers for those who don''t like fish. <h5 style=''text-align: center; background-color: green;''>Adults $12.00 Children 12 & under $8.00</h5>< br/><p class=''text-success''><em>To get a proper headcount, we will need you to sign up & pay for this meal in advance!</em></p>', 'Fish Fry', 5, '2017-07-22 20:00:00.000000', '2017-07-22 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (14, 'anyone who would like to join Mac''s Camping Area on this cruise can do so by pre buying your tickets on their web site songoriverqueen.com under the tab special theme cruises. There is a Disc jockey featuring 50''s and 60''s music!<h5 style=''text-align: center; background-color: green;''>Tickets are $25.00 per person</h5>There is a cash bar available.  We could also go out to dinner in Naples if you''d like.', 'Songo River Queen "Golden Oldies Cruise"', 5, '2017-07-29 21:30:00.000000', '2017-07-29 19:30:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (15, 'Don''t forget your country western attire because we have hired a band for your entertainment after dinner by country band "Cold Blue Steel" (formerly "Emerald Sky"). More information will follow.', 'Country Western Hoedown Potluck', 5, '2017-08-05 22:00:00.000000', '2017-08-05 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (16, 'This a replica of the Mississippi River Paddle Boat. If, enough campers would like to go on this two hour cruise through Long Lake. We can get a little bit of a discount if 25 or more people are interested.<h5 style=''text-align:center; background-color:green;''>Adults $25.00 child 12 & under $13.00 and under 4 is free.</h5>', 'Songo River Queen Day Cruise', 5, '2017-08-12 14:00:00.000000', '2017-08-12 12:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (17, 'A Mac''s Tradition!', 'Traditional Breakfast', 5, '2017-08-13 10:00:00.000000', '2017-08-13 08:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (18, '1/4 rack of ribs, chicken leg, potato, cole slaw, corn on the cob & dessert. <br /><p class=''text-success''><em>We will need you to sign up and pay for this dinner in advance to get a proper head count.</em></p>', 'Fallin'' Off the Rib & Chicken Barbecue Dinner', 5, '2017-08-19 21:00:00.000000', '2017-08-19 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (19, 'Please make sure you sign the potluck sheet. It will help her out. She will have the campers pool and surprises throughout the day. Come and join the fun! More details will follow.', 'Liz & Lynn''s Camper''s Pool Potluck', 5, '2017-09-02 20:00:00.000000', '2017-09-02 17:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (20, 'The docks are getting heavier and heavier every year. Please try and be here to help get them out of the water.', 'Docks Come Out of the Water', 5, '2017-09-09 23:59:59.000000', '2017-09-09 00:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (21, null, 'Last Full Weekend of Camping', 5, '2017-10-09 23:59:59.000000', '2017-10-06 00:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (22, null, 'Closing Down Camp', 5, '2017-10-15 19:00:00.000000', '2017-10-14 09:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (23, 'Food will be provided for your hard work. Until it''s done!  Rain date is October 31st.', 'Fall Cleanup', 5, '2017-10-28 23:59:59.000000', '2017-10-28 09:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (24, 'Food will be provided for your hard work. Until it''s done!  Rain date is October 31st.', 'Fall Cleanup Rain Date (if necessary)', 5, '2017-10-31 23:59:59.000000', '2017-10-31 09:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (26, '<em>Welcome Back!</em> Come meet your old and new camp friends. You can take a chance on raffles. There will be “Free raffles”. You must be present to win. Let’s kick off our season!', '"Meet and Greet" Homemade Sausage Soup', 6, '2018-05-19 20:00:00.000000', '2018-05-19 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (27, 'We need your help, see Doug!', 'Docks in the Water', 6, '2018-05-20 14:00:00.000000', '2018-05-20 10:30:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (28, 'This has been a great hit in seasons past. Please sign up for a variety of breakfast foods that you will prepare and share with your camp friends. Your creativity will make for a yummy breakfast!', 'Breakfast Potluck', 6, '2018-05-27 11:00:00.000000', '2018-05-27 09:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (29, 'Breakfast at the docks before the fishing derby', 'Complimentary Continental Breakfast', 6, '2018-06-02 07:30:00.000000', '2018-06-02 07:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (30, 'Come try your luck catching the biggest fish (One trophy awarded & several prizes). You may use row boats, kayaks, or your canoe for this catch and release event. <em>No motorized Boats!</em>', 'Fishing Derby (Free Fishing Weekend)', 6, '2018-06-02 11:00:00.000000', '2018-06-02 07:30:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (31, 'Bring your favorite dish to share with your camp family/friends.', 'Potluck Dinner', 6, '2018-06-02 20:00:00.000000', '2018-06-02 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (32, 'Any camper who would like to gather all your garb that you have been wanting to get rid of or holding onto for some reason - now is your chance to make some cash!<br /> I think it should be good if we open it up to the public. Any camper interested, please sign up so that we can post it in the local news paper for all those yard sale goers.', 'Mac''s Yard Sale', 6, '2018-06-16 16:00:00.000000', '2018-06-16 08:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (33, 'Choose from scrambled eggs, fried eggs, or French toast, home fries, choice of bacon or sausage, or sausage & gravy, juice & coffee.<br /><h5 style=''text-align: center; background-color: green;''>Adults $5.00 children 12 & under $4.00</h5>', 'Mac''s Traditional Breakfast', 6, '2018-06-24 10:00:00.000000', '2018-06-24 08:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (35, 'See Doug for details', 'Annual Horseshoe Tournament', 6, '2018-07-04 15:00:00.000000', '2018-07-04 10:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (36, '', 'Fireworks at Dark', 6, '2018-07-08 00:00:00.000000', '2018-07-07 21:30:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (37, '', 'Our Bi-Annual Camp Around the World', 6, '2018-07-08 23:59:59.000000', '2018-07-07 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (38, 'Fresh Haddock, homemade French fries, Doug’s famous coleslaw, roll, butter & dessert. There will be boneless fries chicken fingers for those who don’t like fish. <h5 style=''text-align: center; background-color: green;''>Adults $12.00 Children 12 & under $8.00</h5> We will need you to sign up & pay for this meal in advance to get a proper head count.', 'Fish Fry', 6, '2018-07-21 20:00:00.000000', '2018-07-21 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (39, 'Choose from scrambled eggs, fried eggs, or French toast, served with home fries, bacon or sausage, toast, juice and coffee. <h5 style=''text-align: center; background-color: green;''>Adults $5.00 Children 12 & under $4.00</h5>', 'Mac''s Traditional Breakfast', 6, '2018-07-29 10:00:00.000000', '2018-07-29 08:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (40, 'Have a fun filled afternoon trying your luck at playing bingo for a meat package. More details will follow.', 'Meat Raffle Bingo', 6, '2018-08-04 16:00:00.000000', '2018-08-04 14:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (41, '', 'Breakfast Potluck', 6, '2018-08-12 11:00:00.000000', '2018-08-12 09:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (42, '¼ rack of ribs, chicken leg, potato, Doug’s famous homemade coleslaw, corn on the cob, & dessert.<h5 style=''text-align: center; background-color: green;''>Adults $12.00 Children 12 & under $8.00</h5><b>We will need you to sign up & pay for this in advance to get a proper head count.</b>', 'Fallin'' off the Rib Barbeque Dinner', 6, '2018-08-18 20:00:00.000000', '2018-08-18 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (43, 'See Doug for details.  The docks are getting heavier and heavier every year.  Please try and be here to help get them out of the water.', 'Docks Come Out of the Water', 6, '2018-09-15 23:59:59.000000', '2018-09-15 00:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (44, '', 'Last Full Weekend of Camping', 6, '2018-10-07 17:00:00.000000', '2018-10-05 00:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (45, '', 'Closing Down Camp', 6, '2018-10-13 23:59:59.000000', '2018-10-13 00:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (46, 'Until it''s done!  <b>Food will be provided for your hard work</b>', 'Fall Cleanup', 6, '2018-10-27 23:59:59.000000', '2018-10-27 09:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (47, '<em>Welcome Back!</em> Come meet your old and new camp friends. You can take a chance on raffles. There will be “Free Raffles”. You must be present to win. Let’s kick off our season!', '“Meet & Greet” Homemade White Chili', 7, '2019-05-18 21:00:00.000000', '2019-05-18 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (48, 'We need your help! Please see Doug', 'Docks in the Water', 7, '2019-05-25 13:30:00.000000', '2019-05-25 10:30:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (49, 'at the docks before the Fishing Derby', 'Complimentary Continental Breakfast', 7, '2019-06-01 07:30:00.000000', '2019-06-01 07:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (50, 'Come try your luck catching the biggest fish (Trophies awarded & several prizes). You may use row boats, kayaks, or your canoe for this catch and release event. <em>No motorized Boats</em>', 'Annual Fishing Derby', 7, '2019-06-01 11:00:00.000000', '2019-06-01 07:30:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (51, 'See Teri Gorham in site #9', 'Cornhole Tournament', 7, '2019-06-01 17:00:00.000000', '2019-06-01 13:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (52, 'Bring your favorite dish to share with your camp family/friends.', 'Potluck Dinner', 7, '2019-06-01 20:00:00.000000', '2019-06-01 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (53, 'Choose from scrambled eggs, fried eggs, or French toast, home fries, choice of bacon or sausage, or sausage & gravy, juice & coffee.  Please see Janice or Diane for details', 'Macs Traditional Breakfast', 7, '2019-06-01 20:00:00.000000', '2019-06-09 08:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (54, 'Fresh Haddock, homemade French fries, Doug''s famous coleslaw, roll , butter, & dessert. There will be boneless chicken fingers for those who don’t like fish.  Please sign up in advance to get a proper headcount', 'Fish Fry', 7, '2019-06-22 20:00:00.000000', '2019-06-22 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (55, 'Hopefully, there will be enough guys around to play! This may be done on the 6th instead.  Doug will have details on proper start time as well', 'Horseshoe Tournament', 7, '2019-07-04 17:00:00.000000', '2019-07-04 10:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (56, 'This may be either the fourth or the sixth and will be dependent on weather and which campers are around', 'Fireworks at Dark', 7, '2019-07-04 22:00:00.000000', '2019-07-04 20:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (57, 'Hopefully, there will be enough guys around to play! This may be done on the 6th instead.  Doug will have details on proper start time as well', 'Horseshoe Tournament Alternate', 7, '2019-07-06 17:00:00.000000', '2019-07-06 10:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (58, 'This may be either the fourth or the sixth and will be dependent on weather and which campers are around', 'Fireworks at Dark Alternate', 7, '2019-07-06 22:00:00.000000', '2019-07-06 20:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (59, 'Gingerbread house decorating', 'Christmas in July', 7, '2019-07-13 15:30:00.000000', '2019-07-13 13:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (60, 'Santa will pass out gifts, details will follow on what you will need to do for any child that will attend', 'Santa Arrives with Mrs. Claus', 7, '2019-07-13 18:00:00.000000', '2019-07-13 16:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (61, '', 'Christmas in July Potluck Dinner', 7, '2019-07-13 19:00:00.000000', '2019-07-13 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (62, '$20 limit, details to follow', 'Adult Yankee Swap', 7, '2019-07-13 21:30:00.000000', '2019-07-13 19:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (63, 'Some campers like this, some don’t. It mostly is a great hit. Please sign up for a variety of breakfast foods that you will prepare and share with your camp friends. Your creativity will make this a very yummy breakfast.', 'Breakfast Potluck', 7, '2019-07-21 11:00:00.000000', '2019-07-21 09:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (64, 'Teri & Mike are going on the Songo and have invited us to go along with them. If, you''d like to join us. You can do so by purchasing your tickets on www.songoriverqueen.net Click on the ENTER (red) and scroll over to the “Special Theme Cruises”, down at the bottom you''ll see “Cover Tones 7pm-9pm and order your tickets. Should be a fun night! They play 70''s to today.', 'Songo River Queen “Cover Tones Cruise Band”', 7, '2019-07-20 23:59:59.000000', '2019-07-20 19:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (65, '¼ rack of ribs, chicken leg, baked potato, coleslaw, corn on the cob, & dessert. <b>We will need you to sign up in advance to get a proper headcount</b>', 'Fallin'' off the Bone Rib Barbeque Dinner', 7, '2019-07-27 21:30:00.000000', '2019-07-27 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (66, 'Come join us for birthday cake and ice cream.  Share in some birthday games.', 'Everyone''s Birthday', 7, '2019-08-03 21:00:00.000000', '2019-08-03 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (67, '', 'Mac''s Traditional Breakfast', 7, '2019-08-04 10:00:00.000000', '2019-08-04 08:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (68, 'Come join us for your “Campers Pool”. We will have raffles to purchase, 50/50, many free give away prizes, and of course the “Pool”(The more campers play the bigger the prizes will be).', 'End of Season Camper''s Potluck Party', 7, '2019-08-31 17:30:00.000000', '2019-08-31 15:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (69, 'Please sign up for your favorite dish to prepare/share with your camper friends.', 'Camper''s Pool Potluck', 7, '2019-08-31 20:00:00.000000', '2019-08-31 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (70, 'Decorate your campsite for a chance to win $100.00 off your site for 2020.', 'Halloween', 7, '2019-09-14 23:59:59.000000', '2019-09-14 10:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (71, 'come dress in your favorite costume and win a prize. Voting will be determined by you.', 'Halloween Potluck Dinner', 7, '2019-09-14 19:00:00.000000', '2019-09-14 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (72, 'Trick or Treating at each camp site. Make sure you remember to purchase candy for our little ghost & goblins to hand out.', 'Trick or Treating', 7, '2019-09-14 21:00:00.000000', '2019-09-14 19:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (73, 'See Doug for details. The docks are getting heavier and heavier every year. Please try and be here to help get them out of the water.', 'Docks come out of the Water', 7, '2019-09-21 14:00:00.000000', '2019-09-21 10:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (74, 'Do you have a chili recipe that you think could be the winner of our “Chili Contest”, then enter to win. Voting will be decided by you. <b>1st Prize $100.00, 2nd prize $50.00 and 3rd prize $25.00.</b>', 'Chili Contest', 7, '2019-09-28 20:00:00.000000', '2019-09-28 18:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (75, 'Come back your trucks/cars at the garage. Bring your coolers with what you’d like to drink. We will provide burgers/dogs while we watch week #5 NFL Patriots vs. Redskins on the big screen.', 'NFL Patriots vs Redskins Tailgate Party', 7, '2019-10-06 16:30:00.000000', '2019-10-06 13:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (76, '', 'Closing Down Camp', 7, '2019-10-19 19:00:00.000000', '2019-10-19 08:00:00.000000');
INSERT INTO activity.Activities (Id, ActivityDescription, ActivityTitle, CalendarId, EndTime, StartTime) VALUES (77, '9am until it’s done Fall Cleanup Food will be provided for your hard work.', 'Fall Cleanup', 7, '2019-10-26 23:59:59.000000', '2019-10-26 09:00:00.000000');