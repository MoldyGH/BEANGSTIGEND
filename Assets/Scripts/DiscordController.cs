using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public Discord.Discord discord;

    void Start()
    {
        discord = new Discord.Discord(832428756114210836, (System.UInt64)Discord.CreateFlags.Default);
    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }
    public void UpdatePresence(string details, string state)
    {
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            Details = details,
            State = state
        };
        activityManager.UpdateActivity(activity, (res) => { 
            if(res == Discord.Result.Ok)
            {
                Debug.Log("DISCORD UPDATED LMAO");
            }
            else
            {
                Debug.LogError("THERE WAS AN ERROR THATS YOUR PROBLEM NOT MINE");
            }
        });
    }
    private void OnApplicationQuit()
    {
        ResetPresence();
    }
    public void ResetPresence()
    {
        discord.Dispose();
        Debug.Log("DISCORD RESET LMAO");
    }
}
