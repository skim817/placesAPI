using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using planyourdate.Model;

namespace planyourdate.Transfer
{
    public class Map
    {
        public static void TestProgram()
        {
            Console.WriteLine(GetPlaceFromId("ChIJ7-AWnvpHDW0RXGbnD3-1Zsc"));
            Console.WriteLine(GetPhotosFromID("ChIJ7-AWnvpHDW0RXGbnD3-1Zsc"));
            Console.ReadLine();
        }

        public static Place GetPlaceFromId(String PlaceID)
        {
            String APIKEY = "AIzaSyDxnLbITe46r20XEo51dgFm8yHeHL4nzT0";
            String LinkforMap = "https://maps.googleapis.com/maps/api/place/details/json?placeid=" + PlaceID + "&key=" + APIKEY;

            String PlacesInfoJSON = new WebClient().DownloadString(LinkforMap);

            dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(PlacesInfoJSON);

            String Name = jsonObj["result"]["name"];

            int rank = 100;
            if (jsonObj["result"]["rating"] != null)
            {
                rank = jsonObj["result"]["rating"];
            }

            String Address = jsonObj["result"]["formatted_address"];

            String PhoneNo = "Not Avaliable";
            if (jsonObj["result"]["formatted_phone_number"] != null)
            {
                 PhoneNo = jsonObj["result"]["formatted_phone_number"];
            }
      
            Boolean OpenNow = false;
            if (jsonObj["result"]["opening_hours"] != null)
            {
                OpenNow = jsonObj["result"]["opening_hours"]["open_now"];
            }

            String lat = jsonObj["result"]["geometry"]["location"]["lat"];
            String lng = jsonObj["result"]["geometry"]["location"]["lng"];

            String photo = "https://shenandoahcountyva.us/bos/wp-content/uploads/sites/4/2018/01/picture-not-available-clipart-12.jpg";
            if (jsonObj["result"]["photos"] != null)
            {
                photo = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=" + jsonObj["result"]["photos"][0]["photo_reference"] + "&key=" + APIKEY;
            }

            Place place = new Place
            {
                PlaceName = Name,
                RankBy = rank,
                PlaceAddress = Address,
                Comment = "No Comments Shown.. Please Comment Me!!",
                PhoneNumber = PhoneNo,
                IsFavourite = false,
                IsOpenNow = OpenNow,
                PlaceGeolat =lat,
                PlaceGeolng =lng,
                PhotoRef = photo
               
            };
            return place;
        }

        public static List<Photo> GetPhotosFromID(String PlaceID)
        {
            String APIKEY = "AIzaSyDxnLbITe46r20XEo51dgFm8yHeHL4nzT0";
            String LinkforMap = "https://maps.googleapis.com/maps/api/place/details/json?placeid=" + PlaceID + "&key=" + APIKEY;

            String PlacesInfoJSON = new WebClient().DownloadString(LinkforMap);

            dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(PlacesInfoJSON);
            List<Photo> photoFinal = new List<Photo>();
            if (jsonObj["result"]["photos"] != null)
           {
                for (int i = 0; i < 10; i++)
                {
                    String Name = jsonObj["result"]["name"];   
                    String Photoaaa = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=" + jsonObj["result"]["photos"][i]["photo_reference"] + "&key=" + APIKEY;
                    bool ava = true;
                    Photo ph = new Photo
                    {
                        PhotoName = Name,
                        IsAvaliable = ava,
                        Photo1 = Photoaaa
                    };

                    photoFinal.Add(ph);
               }
            }

            return photoFinal;
        }



    }
}
