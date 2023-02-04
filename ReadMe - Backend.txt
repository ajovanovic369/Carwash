			:::::::::::::::::::::
			::::###:::::::::::##:
			::'## ##::::::::: ##:
			:'##:. ##:::::::: ##:
			:##:::. ##::::::: ##:
			:#########::##::: ##:
			:##.....##::##::: ##:
			:##:::::##:::#######:
			:::::::::::::::::::::
ÚÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ¿
³ PROJECT  : Carwash                                                          ³
³ DURATION : 2 weeks                                                          ³
ÀÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ
³ BEFORE YOU START UPDATE-DATABASE, BE SURE YOU ADD A NEW MIGRATION           ³
³ (FOR ENCRYPTION TO WORK)                                                    ³
ÀÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ

Greetings and salutations,


Task assigned to me was to make an application that will simulate carwash 
online services.During production of project I was using online course
"Building RESTful Web APIs with ASP.NET Core 3.1" by Felipe Gavilán.
My goal was to take all knowledge from Felipe's course and successfully 
implement it in my project.

I will now get into a few things covered by project (which wasn't covered in 
official request):

- carwash name is unique, no matter if different owner try to make a carwash 
  with a same name, it won't be possible, that was a design choice made with 
  a purpose to protect trade-marks and possible copy-cats
- regarding carwash working hours, I took under presumption that every
  carwash was open every day except Sunday
- regarding carwash address I used basic regex just to check if there is 
  no special characters in the address name and if there is a white space
  between a name and a number
- carwash can't have two services with the same name
- carwash open true/false check is handled by automated service
- user have to post an image of carwash logo when making a new carwash
- service price can't be 0 or free
- user reservation, when submitted, will be set as "pending" if carwash
  owner doesn't set user reservation to "accepted" in 15 minutes after 
  reservation was made it will be "rejected" automatically by automated 
  service, that design choice was made due carwash not being able to 
  accept new customers due unforeseen circumstances
- user reservation, with status "accepted" after expiration will 
  be deleted from the list of reservations by an automated service 
  and information will be inserted into "earnings" table


How I could improve my application in future
(and still be in framework of official requirements):

- instead of having lot of code in the controllers, make an repository
- greater use of logger services and writting logs into a file
- carwash working hours, I could implement that owner can set up his 
  own working hours for every day of the week and decided when carwash 
  is closed
- carwash can have several car wash slots per hour
- before sending information to the "earnings" table, I could set up 
  a check, if customer really showed up at the carwash shop and paid, 
  that checked would be ticked by the carshop staff


According to team meeting discussion, due usage of "IDataProtectionProvider" 
and "IDataProtector" in main project, I made a separate branch on GitLab, 
named "UnitTesting" in wich I didn't used said interfaces 
and branch is used for UnitTesting of my application.


ÚÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ¿
³                               GREETINGS FROM                                ³
³                                 ALEKSANDAR                                  ³
ÀÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÙ
