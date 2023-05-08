using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace InstagramEngagementRate
{
    class Program
    {
        static void Main(string[] args)
        {
            // input your Instagram API access token here
            string accessToken = "your-access-token";

            // Enter the username of the Instagram profile you want to check here
            string username = "example_username";

            // make a REST API request to fetch Instagram profile data
            var client = new RestClient($"https://api.instagram.com/v1/users/self/media/recent/?access_token={accessToken}");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            // convert JSON data into a JObject object
            JObject instagramData = JObject.Parse(response.Content);

            // look for the data object with the appropriate username
            JObject userData = null;
            foreach (var user in instagramData["data"])
            {
                if (user["user"]["username"].ToString() == username)
                {
                    userData = (JObject)user;
                    break;
                }
            }

            // check if Instagram profile found
            if (userData != null)
            {
                // calculate engagement rate
                int likes = int.Parse(userData["likes"]["count"].ToString());
                int comments = int.Parse(userData["comments"]["count"].ToString());
                int followers = int.Parse(userData["user"]["followed_by"]["count"].ToString());
                double engagementRate = (likes + comments) / (double)followers * 100;

                // tampilkan hasil
                Console.WriteLine($"Engagement rate for : {username} is {engagementRate}%");
            }
            else
            {
                Console.WriteLine($"Profile : {username} not found");
            }
        }
    }
}
