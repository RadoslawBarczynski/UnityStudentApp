using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supabase.Functions.Responses;
using Supabase.Storage;
using Supabase.Gotrue.Responses;
using Supabase.Realtime.Converters;
using System;
using Supabase.Realtime;
using static Supabase.Client;
using static UnityEditor.Progress;
using System.Threading.Tasks;

public class LoginScript : MonoBehaviour
{
    string url;
    string key;
    public Supabase.Client client;
    [SerializeField]
    public List<Student> students = new List<Student>();


    // Start is called before the first frame update
    void Start()
    {
        Main();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public async void Main()
    {
        url = "https://melfibfnkmadpskpvist.supabase.co";
        key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1lbGZpYmZua21hZHBza3B2aXN0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NzgxOTIxMTcsImV4cCI6MTk5Mzc2ODExN30.Kd7kp1eiKHzcTsg7noH02E_smiAJR_Y-9kR45wg6UlE";

        client = await Supabase.Client.InitializeAsync(url, key, new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true
        });


        var result = await client.From<Student>().Get();

        students = result.Models;

        foreach (var student in students)
        {
            Debug.Log(student.StudentFirstName);
        }
    }
}
